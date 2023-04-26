using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration.Memory;

namespace GetKnownMAUI.Services
{
    public class AppConfiguration : ConfigurationBuilder
    {
        private readonly static Dictionary<string, string> source = new()
        {
            ["USERNAME"] = "wy",
            ["PASSWORD"] = "aaaaaa",
            //["HOST"] = "http://192.168.10.224:5157",
            ["HOST"] = "https://xamarin.ccmeta.com:9501",
        };

        public static IConfiguration GetInstence()
        {
            var appConfiguration = new AppConfiguration();
            MemoryConfigurationSource m_config = new() { InitialData = source };
            appConfiguration.Add(m_config);
            return appConfiguration.Build();
        }
    }
}
