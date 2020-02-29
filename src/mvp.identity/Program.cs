// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using mvp.identity.Data;
using mvp.identity.Extensions;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace mvp.identity
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.File(
                    formatter: new RenderedCompactJsonFormatter(),
                    path: Configuration.LogsPath(),
                    restrictedToMinimumLevel: Configuration.LogsMinLevel() == "Info" ? LogEventLevel.Information : LogEventLevel.Debug,
                    fileSizeLimitBytes: 1_000_000,
                    shared: true,
                    flushToDiskInterval: TimeSpan.FromSeconds(10),
                    rollOnFileSizeLimit: true
                )
                .CreateLogger();

            try
            {
                var seed = args.Contains("/seed");
                if (seed)
                {
                    args = args.Except(new[] { "/seed" }).ToArray();
                }

                var host = CreateHostBuilder(args).Build();

                var connectionString = Configuration.GetConnectionString("DefaultConnection");
                SeedData.EnsureSeedData(connectionString);

                Log.Information("Starting host...");
                host.Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly.");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseSerilog();
                });
    }
}
