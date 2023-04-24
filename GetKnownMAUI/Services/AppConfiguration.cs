using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Xamarin_Forms_demo.Services
{
    public class AppConfiguration : ConfigurationBuilder
    {

        public static IConfiguration GetInstence()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resName = assembly.GetManifestResourceNames()?.
                FirstOrDefault(r => r.EndsWith("AppSettings.json", StringComparison.OrdinalIgnoreCase));
            return new AppConfiguration().AddJsonFile("AppSettings.json", false, true).Build();
        }
    }
}
