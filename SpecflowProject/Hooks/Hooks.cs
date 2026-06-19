using Microsoft.Playwright;
using SpecflowProject.Config;
using SpecflowProject.Utilities;

namespace SpecflowProject.Hooks
{
    /// <summary>
    /// Hooks class for SpecFlow. This class can be used to define setup and teardown methods that run before and after each scenario or feature. 
    /// You can use attributes like [BeforeScenario], [AfterScenario], [BeforeFeature], and [AfterFeature] to specify the methods that should be executed at different points in the test execution lifecycle.
    /// </summary>
    [Binding]
    internal class Hooks
    {
        private readonly ScenarioContext _scenarioContext;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        /// <summary>
        /// BeforeScenario hook method that runs before each scenario. You can use this method to perform any setup or initialization required before executing a scenario.
        /// </summary>
        [BeforeScenario]
        public async Task BeforeScenario()
        {
            LoggerManager.LogInfo("Creating Playwright, Browser and Page instance...");

            IPlaywright playwright = await Playwright.CreateAsync();
            var browser = await CreateBrowser(playwright);
            var page = await browser.NewPageAsync();

            await page.SetViewportSizeAsync(1920, 1080);

            _scenarioContext["Browser"] = browser;
            _scenarioContext["Page"] = page;
            _scenarioContext["Playwright"] = playwright;
        }

        /// <summary>
        /// BeforeScenario hook method that runs after each scenario. You can use this method to perform any cleanup or finalization required after executing a scenario.
        /// </summary>
        [AfterScenario]
        public async Task AfterScenario()
        {
            LoggerManager.LogInfo("Closing Playwright, Browser and Page instance...");

            var page = _scenarioContext["Page"] as IPage;
            var browser = _scenarioContext["Browser"] as IBrowser;

            await page.CloseAsync();
            await browser.CloseAsync();
        }

        /// <summary>
        /// BeforeTestRun hook method that runs before the entire test run. 
        /// You can use this method to perform any setup or initialization required before executing all tests.
        /// </summary>
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            try
            {
                ConfigurationLoader.Load();
            }
            catch (Exception ex)
            {
                LoggerManager.LogInfo("An error occurred while initializing configuration settings.", LogTypeEnum.Error);

                throw new Exception("An error occurred while initializing configuration settings.", ex);
            }
        }

        /// <summary>
        /// Creates a browser instance based on the specified browser type. 
        /// This method uses the Playwright library to launch the appropriate browser (Chrome, Firefox, or Chromium in headless mode) based on the configuration settings. 
        /// If an unsupported browser type is specified, it throws an ArgumentException.
        /// </summary>
        /// <param name="playwright"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private async Task<IBrowser> CreateBrowser(IPlaywright playwright, BrowserTypeLaunchOptions options = null)
        {

            LoggerManager.LogInfo("Creating browser instance.");
            IBrowser browser;

            if (playwright == null)
            {
                LoggerManager.LogInfo("Playwright instance is null.", LogTypeEnum.Error);

                throw new ArgumentNullException(nameof(playwright), "Playwright instance cannot be null.");
            }

            if(string.IsNullOrEmpty(ConfigurationLoader.AppSettings.BrowserType))
            {
                LoggerManager.LogInfo("Browser type is not specified in the configuration.", LogTypeEnum.Error);

                throw new ArgumentException("Browser type is not specified in the configuration.");
            }

            if (ConfigurationLoader.AppSettings.BrowserType.Equals("chrome", StringComparison.OrdinalIgnoreCase))
            {
                LoggerManager.LogInfo("Launching Chrome browser.");

                browser = await playwright.Chromium.LaunchAsync(options ?? new BrowserTypeLaunchOptions { Headless = false });
            }
            else if (ConfigurationLoader.AppSettings.BrowserType.Equals("firefox", StringComparison.OrdinalIgnoreCase))
            {
                LoggerManager.LogInfo("Launching Firefox browser.");

                browser = await playwright.Firefox.LaunchAsync(options ?? new BrowserTypeLaunchOptions { Headless = false });
            }
            else
            {
                LoggerManager.LogInfo($"Unsupported browser type: {ConfigurationLoader.AppSettings.BrowserType}", LogTypeEnum.Error);

                throw new ArgumentException($"Unsupported browser type: {ConfigurationLoader.AppSettings.BrowserType}");
            }

            return browser;
        }
    }
}
