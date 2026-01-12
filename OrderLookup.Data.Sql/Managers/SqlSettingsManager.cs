using System.Text.Json;

namespace OrderLookup.Data.Sql.Managers
{
    public class SqlSettingsManager
    {
        private readonly Dictionary<string, object> _config;
        public SqlSettingsManager()
        {
            _config = GetConfig();

        }

        private Dictionary<string, object> GetConfig()
        {
            var assembly = typeof(OrderLookupSqlAssemblyMarker).Assembly;

            var names = assembly.GetManifestResourceNames();

            var resourceName = names.FirstOrDefault(n =>
                n.EndsWith("appsettings.json", StringComparison.OrdinalIgnoreCase));

            if (resourceName is null)
                throw new InvalidOperationException(
                    "Embedded appsettings.json not found. Resources in this assembly:\n" +
                    string.Join("\n", names));

            using var stream = assembly.GetManifestResourceStream(resourceName)
                ?? throw new InvalidOperationException($"Resource stream null for: {resourceName}");

            using var reader = new StreamReader(stream);
            var json = reader.ReadToEnd();

            return JsonSerializer.Deserialize<Dictionary<string, object>>(json)!;
        }


        public T GetValue<T>(string key)
        {
            if (_config.TryGetValue(key, out var value))
            {
                if (value is JsonElement jsonElement) // If value is from JSON deserialization
                {
                    value = jsonElement.ToString();
                }

                return (T)Convert.ChangeType(value, typeof(T));
            }
            throw new KeyNotFoundException($"Key '{key}' not found in configuration.");
        }
    }
}
