using Microsoft.Extensions.Configuration;
using System.IO;

namespace ApiPatrimonio.Data
{
    public static class ConfigurationManager
    {
        public static IConfiguration AppSetting { get; private set; }
        
        static ConfigurationManager()
        {
            AppSetting = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
        }
    }
}
