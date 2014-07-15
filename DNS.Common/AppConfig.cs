using System;
using System.Configuration;

namespace DNS.Common
{
    public static class AppConfig
    {
        public static T GetAppSetting<T>(string key)
        {
            var rawValue = ConfigurationManager.AppSettings[key];

            if (rawValue == null)
                throw new ConfigurationErrorsException(
                    string.Format
                        ("Please configure '{0}' in the AppSettings section of the application .config file.", key));

            if (typeof(T) == typeof(Uri))
                return (T)(object)new Uri(rawValue);

            var value = (T)Convert.ChangeType(rawValue, typeof(T));
            return value;
        }
    }
}