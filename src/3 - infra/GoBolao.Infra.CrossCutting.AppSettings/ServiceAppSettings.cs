using GoBolao.Infra.Data.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GoBolao.Infra.CrossCutting.AppSettings
{
    public class ServiceAppSettings : IServiceAppSettings
    {
        private string ApplicationExeDirectory()
        {
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var appRoot = Path.GetDirectoryName(location);
            return appRoot;
        }

        private IConfigurationRoot GetAppSettings()
        {
            string applicationExeDirectory = ApplicationExeDirectory();

            var builder = new ConfigurationBuilder()
            .SetBasePath(applicationExeDirectory)
            .AddJsonFile("appsettings.json");
            return builder.Build();
        }

        public string ConexaoMSSQL()
        {
            var appSettingsJson = GetAppSettings();
            return appSettingsJson["ConexaoMSSQL"];
        }
    }
}
