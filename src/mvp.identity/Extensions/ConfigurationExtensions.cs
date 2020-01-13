using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvp.identity.Extensions
{
    public static class ConfigurationExtensions
    {
        public static int KestrelHttpPort(this IConfiguration config)
        {
            return config.GetValue<int>("KestrelHttpPort");
        }

        public static int KestrelHttpsPort(this IConfiguration config)
        {
            return config.GetValue<int>("KestrelHttpsPort");
        }

        public static string CertificatePath(this IConfiguration config)
        {
            return config.GetValue<string>("Certificate:Path");
        }

        public static string CertificatePassword(this IConfiguration config)
        {
            return config.GetValue<string>("Certificate:Password");
        }

        public static string LogsMinLevel(this IConfiguration config)
        {
            return config.GetValue<string>("Logs:MinLevel");
        }

        public static string LogsPath(this IConfiguration config)
        {
            return config.GetValue<string>("Logs:Path");
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
