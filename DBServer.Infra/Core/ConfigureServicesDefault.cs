using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DBServer.Infra
{
    public static class ConfigureServicesDefault
    {
        public static readonly string AppSettingsJsonFile = GetAppSettingsFileLocation();

        public static IConfigurationBuilder AddDefaultAppSettings(IConfigurationBuilder config)
        {
            if (File.Exists(AppSettingsJsonFile))
            {
                config.AddJsonFile(AppSettingsJsonFile);
            }
            return config;
        }

        public static string GetAppSettingsFileLocation()
        {
            return (Environment.OSVersion.Platform == PlatformID.Unix)
                    ? @"/default_settings.json"
                    : @"C:\data\dbserver\appsettings\default.docker.json";
        }
    }
}
