using Microsoft.Extensions.Configuration;
using System.IO;

namespace ProMassSpammer.Data
{
    /// <summary>
    /// This class is purely for localized testing of this project.
    /// DotNet core's idea of configuration is odd so localizing what
    /// is needed to accomplish functionality in order to make it
    /// usable.
    /// </summary>
    public class Config
    {
        private readonly string _configFileName;
        private const string DefaultConfigName = "appsettings.json";

        public Config()
            : this(null)
        {
            
        }

        public Config(string configFileName)
        {
            _configFileName = string.IsNullOrWhiteSpace(configFileName) ? DefaultConfigName : configFileName;
        }

        public IConfigurationRoot BuildConfigs()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(_configFileName, optional: false, reloadOnChange: true);

            var configuration = builder.Build();

            return configuration;
        }
    }
}
