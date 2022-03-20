using System.Text.Json;

namespace FhnwAdSynchronizer.Configuration;

public interface IConfigurationService<T> where T : class, new()
{
    T ConfigurationObject { get; }

    Task UpdateConfigurationObject(T configurationObject);
}

public class ConfigurationService<T> : IConfigurationService<T> where T : class, new()
{
    private const string ConfigFileName = "fhnwAdSynchronizer.json";
    private readonly string ConfigFilePath;
    public T ConfigurationObject { get; private set; }

    public ConfigurationService()
    {
        ConfigFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ConfigFileName);
        if (!File.Exists(ConfigFilePath))
        {
            File.Create(ConfigFilePath).Dispose();
        }

        var configString = File.ReadAllText(ConfigFilePath);
        ConfigurationObject = string.IsNullOrWhiteSpace(configString)
            ? new T()
            : JsonSerializer.Deserialize<T>(configString)
            ?? new T();
    }

    public async Task UpdateConfigurationObject(T configurationObject)
    {
        ConfigurationObject = configurationObject;
        var configString = JsonSerializer.Serialize<T>(configurationObject);
        await File.WriteAllTextAsync(ConfigFilePath, configString);
    }
}
