using Microsoft.Extensions.Configuration;
using SpecflowProject.Objects;
using SpecflowProject.Utilities;

namespace SpecflowProject.Config
{
    /// <summary>
    /// ConfigurationLoader is a static class responsible for loading application settings from the appsettings.json file.
    /// </summary>
    public static class ConfigurationLoader
    {
        public static AppSettings AppSettings { get; set; }

        /// <summary>
        /// Loads the application settings from the appsettings.json file and stores them in the AppSettings property.
        /// </summary>
        public static void Load()
        {
            LoggerManager.LogInfo("Loading configuration from appsettings.json");

            var configDirectory = AppContext.BaseDirectory;

            AppSettings = new ConfigurationBuilder()
                .SetBasePath(configDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build()
                .GetSection("AppSettings")
                .Get<AppSettings>()!;
        }
    }
}