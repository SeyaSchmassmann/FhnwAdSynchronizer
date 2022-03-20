using FhnwAdSynchronizer;
using FhnwAdSynchronizer.Configuration;
using FhnwAdSynchronizer.Core;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddSingleton(typeof(IConfigurationService<>), typeof(ConfigurationService<>));
services.AddTransient<HandlerSelector>();
services.AddSingleton<Application>();

services.Scan(selector => selector.FromAssemblyOf<Program>()
                                      .AddClasses(f => f.AssignableTo<IHandler>())
                                          .AsSelf()
                                          .WithTransientLifetime());


var serviceProvider = services.BuildServiceProvider();

var application = serviceProvider.GetRequiredService<Application>();

await application.RunAsync(args);