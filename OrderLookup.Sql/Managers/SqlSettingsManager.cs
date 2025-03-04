using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OrderLookup.Sql.Managers
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
            var assembly = Assembly.GetExecutingAssembly();

            var resourceName = assembly.GetManifestResourceNames()
                .FirstOrDefault("appsettings.json");

            using Stream stream = assembly.GetManifestResourceStream(resourceName)!;

            using StreamReader streamReader = new StreamReader(stream);

            string json = streamReader.ReadToEnd();

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
