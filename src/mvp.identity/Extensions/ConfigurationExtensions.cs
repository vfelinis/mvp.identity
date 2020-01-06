using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvp.identity.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string LoggingPath(this IConfiguration config)
        {
            return config.GetValue<string>("Logging:Path");
        }

        public static string GoogleClientId(this IConfiguration config)
        {
            return config.GetValue<string>("Google:ClientId");
        }

        public static string GoogleClientSecret(this IConfiguration config)
        {
            return config.GetValue<string>("Google:ClientSecret");
        }
    }
}
