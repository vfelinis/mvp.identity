using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvp.identity.Extensions
{
    public static class ConfigurationExtensions
    {
        public static bool IsDevelopment(this IConfiguration config)
        {
            return config.GetValue<bool>("IsDevelopment");
        }

        public static string CertificateDevFile(this IConfiguration config)
        {
            return config.GetValue<string>("Certificate:DevFile");
        }

        public static string CertificateDevPass(this IConfiguration config)
        {
            return config.GetValue<string>("Certificate:DevPass");
        }

        public static string CertificateAcmeFile(this IConfiguration config)
        {
            return config.GetValue<string>("Certificate:AcmeFile");
        }

        public static string CertificateAcmeDomain(this IConfiguration config)
        {
            return config.GetValue<string>("Certificate:AcmeDomain");
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

        public static List<string> IdentityServerSpaClientRedirectUris(this IConfiguration config)
        {
            return config.GetValue<string>("IdentityServer:SpaClient:RedirectUris")?.Split(',')
                .Select(s => s.Trim()).Where(s => !string.IsNullOrEmpty(s)).ToList() ?? new List<string>();
        }

        public static List<string> IdentityServerSpaClientPostLogoutRedirectUris(this IConfiguration config)
        {
            return config.GetValue<string>("IdentityServer:SpaClient:PostLogoutRedirectUris")?.Split(',')
                .Select(s => s.Trim()).Where(s => !string.IsNullOrEmpty(s)).ToList() ?? new List<string>();
        }

        public static List<string> IdentityServerSpaClientAllowedCorsOrigins(this IConfiguration config)
        {
            return config.GetValue<string>("IdentityServer:SpaClient:AllowedCorsOrigins")?.Split(',')
                .Select(s => s.Trim()).Where(s => !string.IsNullOrEmpty(s)).ToList() ?? new List<string>();
        }
    }
}
