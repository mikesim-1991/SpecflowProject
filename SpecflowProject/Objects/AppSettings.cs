namespace SpecflowProject.Objects
{
    /// <summary>
    /// AppSettings class represents the configuration settings for the application, including the base URL, browser type, and headless mode.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// BaseUrl property represents the base URL of the application under test. 
        /// It is used to navigate to the application during automated tests.
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// BrowserType property represents the type of browser to be used for automated tests. 
        /// Default is "chromium".
        /// </summary>
        public string BrowserType { get; set; } = "chromium";
        /// <summary>
        /// Headless property indicates whether the browser should run in headless mode. Default is true.
        /// </summary>
        /// 
        public bool Headless { get; set; } = true;
    }
}
