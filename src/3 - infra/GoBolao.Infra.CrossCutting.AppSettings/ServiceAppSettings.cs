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
        private string ConexaoMSSQL;
        private IConfiguration Configuration;

        public ServiceAppSettings(IConfiguration configuration)
        {
            Configuration = configuration;
            ConexaoMSSQL = Configuration["ConexaoMSSQL"];
        }

        public string PegarConexaoMSSQL()
        {
            return ConexaoMSSQL;
        }
    }
}
