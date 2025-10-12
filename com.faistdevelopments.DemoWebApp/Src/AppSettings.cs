using com.faistdevelopments.AbstractEfCore;

namespace com.faistdevelopments.DemoWebApp
{
    /// <summary>
    /// Class to read config from appsettings.json
    /// </summary>
    public class AppSettings
    {
        protected static readonly string CONFIG_FILE = "appsettings.json";
        protected static Dictionary<string, object?> CACHE_SETTINGS = new Dictionary<string, object?>();

        /// <summary>
        /// Init the config file
        /// </summary>
        /// <returns></returns>
        protected static IConfigurationRoot GetConfig()
        {
            return new ConfigurationBuilder()
                .AddJsonFile(CONFIG_FILE)
                .Build();
        }

        protected static T? GetValueFromCache<T>(string key, bool disableLogs = false)
        {
            if (!CACHE_SETTINGS.ContainsKey(key))
            {
                if (!disableLogs)
                {
                    //_log.Debug("Value for key '" + key + "' was not found in Cache");
                }

                return default(T);
            }

            if (!disableLogs)
            {
                // _log.Debug("Found Value for key '" + key + "' in Cache");
            }
            return (T?)CACHE_SETTINGS[key];
        }

        protected static void AddOrUpdateValueFromCache(string key, object? value)
        {
            if (CACHE_SETTINGS.ContainsKey(key))
            {
                CACHE_SETTINGS[key] = value;
            }
            else
            {
                CACHE_SETTINGS.Add(key, value);
            }
        }

        /// <summary>
        /// This is retrieved from the Settings Server, so only there the config needs to exist
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static string GetDbConnectionString(string dbName)
        {
            string identifier = "filebasedConnString_" + dbName;
            string? connectionString = GetValueFromCache<string>(identifier);
            if (connectionString == null)
            {
                string hostKey = "hostname";
                string userKey = "user";
                string passwordKey = "password";
                string databaseKey = "database";

                IConfigurationSection section = GetConfig().GetSection("Databases").GetSection(dbName);

                connectionString = MySqlConnectionStringBuilder.Create() //
                    .SetHostname(section[hostKey]!) //
                    .SetUsername(section[userKey]!) //
                    .SetPassword(section[passwordKey]!) //
                    .SetDatabaseName(section[databaseKey]!) //
                    .Build();

                AddOrUpdateValueFromCache(identifier, connectionString);
            }

            Console.WriteLine("Conn: " + connectionString);

            return connectionString;
        }
    }
}
