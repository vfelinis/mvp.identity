using Microsoft.Extensions.Configuration;
using mvp.identity.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;

namespace mvp.identity.Helpers
{
    public static class CertificateHelper
    {
        public static X509Certificate2 CreateCertificate(IConfiguration configuration)
        {
            if (configuration.IsDevelopment())
            {
                return new X509Certificate2(configuration.CertificateDevFile(), configuration.CertificateDevPass());
            }
            else
            {
                var jsonBytes = File.ReadAllBytes(configuration.CertificateAcmeFile());
                using var jsonDoc = JsonDocument.Parse(jsonBytes);
                var root = jsonDoc.RootElement;
                var item = root.GetProperty("leresolver").GetProperty("Certificates").EnumerateArray()
                    .First(s => s.GetProperty("domain").GetProperty("main").GetString() == configuration.CertificateAcmeDomain());
                var certBase64 = item.GetProperty("certificate").GetString();
                var keyBase64 = item.GetProperty("key").GetString();
                using var publicKey = new X509Certificate2(Convert.FromBase64String(certBase64));
                var privateKey = UTF8Encoding.UTF8.GetString(Convert.FromBase64String(keyBase64));
                var privateKeyBlocks = privateKey.Split("-", StringSplitOptions.RemoveEmptyEntries);
                var privateKeyBytes = Convert.FromBase64String(privateKeyBlocks[1]);
                using var rsa = RSA.Create();
                rsa.ImportRSAPrivateKey(privateKeyBytes, out _);
                var keyPair = publicKey.CopyWithPrivateKey(rsa);
                return new X509Certificate2(keyPair.Export(X509ContentType.Pfx));
            }
        }
    }
}
