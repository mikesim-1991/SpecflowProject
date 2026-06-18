using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using SpecflowProject.Utilities;

namespace SpecflowProject.Hooks
{
    /// <summary>
    /// ExtentReportHooks class for SpecFlow. 
    /// This class is responsible for integrating ExtentReports with SpecFlow to generate detailed test execution reports. 
    /// It contains methods that run before and after the test run, as well as before and after each feature and scenario. 
    /// The report is generated in HTML format and provides insights into the test execution, including passed and failed steps.
    /// </summary>
    [Binding]
    internal class ExtentReportHooks
    {
        private static ExtentReports _extent;
        private static ExtentTest _feature;
        private static ExtentTest _scenario;


        private static readonly string _reportPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "ExtentReports");
        private static readonly string _reportFile = Path.Combine(_reportPath, "ExtentReports.html");

        /// <summary>
        /// Initializes the ExtentReports instance before the test run starts. 
        /// This method is decorated with the [BeforeTestRun] attribute, which means it will be executed once before any tests are run. 
        /// You can use this method to set up the report configuration, such as specifying the report file path, adding system information, and configuring other report settings.
        /// </summary>
        [BeforeTestRun]
        public static void InitializeReport()
        {
            LoggerManager.LogInfo("Initializing ExtentReports...");

            if (!Directory.Exists(_reportPath))
            {
                LoggerManager.LogInfo($"Creating report directory at: {_reportPath}");
                Directory.CreateDirectory(_reportPath);
            }

            var htmlReporter = new ExtentSparkReporter(_reportFile);

            _extent = new ExtentReports();
            _extent.AttachReporter(htmlReporter);
        }

        /// <summary>
        /// AfterTestRun hook method that runs after the entire test run. 
        /// You can use this method to perform any cleanup or finalization required after executing all tests. 
        /// In this case, it flushes the ExtentReports instance to ensure that all report data is written to the report file.
        /// </summary>
        [AfterTestRun]
        public static void FlushReport()
        {
            LoggerManager.LogInfo("Flushing ExtentReports...");
            _extent.Flush();
        }

        /// <summary>
        /// BeforeFeature hook method that runs before each feature. 
        /// You can use this method to perform any setup or initialization required before executing a feature. 
        /// In this case, it creates a new ExtentTest instance for the feature and assigns it to the _feature variable.
        /// </summary>
        /// <param name="featureContext"></param>
        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            LoggerManager.LogInfo($"Starting feature: {featureContext.FeatureInfo.Title}");

            _feature = _extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        }

        /// <summary>
        /// BeforeScenario hook method that runs before each scenario. 
        /// You can use this method to perform any setup or initialization required before executing a scenario. 
        /// In this case, it creates a new ExtentTest instance for the scenario and assigns it to the _scenario variable.
        /// </summary>
        /// <param name="scenarioContext"></param>
        [BeforeScenario]
        public static void BeforeScenario(ScenarioContext scenarioContext)
        {
            LoggerManager.LogInfo($"Starting scenario: {scenarioContext.ScenarioInfo.Title}");

            _scenario = _feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
        }

        /// <summary>
        /// AfterStep hook method that runs after each step in a scenario. 
        /// You can use this method to perform any cleanup or finalization required after executing a step. 
        /// In this case, it creates a new node in the ExtentTest instance for the scenario, representing the executed step.
        /// </summary>
        /// <param name="scenarioContext"></param>
        [AfterStep]
        public void AfterStep(ScenarioContext scenarioContext)
        {
            LoggerManager.LogInfo($"AfterStep hook executed for step: {ScenarioStepContext.Current.StepInfo.Text}");

            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            var stepInfo = ScenarioStepContext.Current.StepInfo.Text;

            if (scenarioContext.TestError == null)
            {
                LoggerManager.LogInfo($"Step passed: {stepType} {stepInfo}");

                if (stepType == "Given")
                    _scenario.CreateNode<Given>(stepInfo);
                else if(stepType == "When")
                    _scenario.CreateNode<When>(stepInfo);
                else if(stepType == "Then")
                    _scenario.CreateNode<Then>(stepInfo);
                else if(stepType == "And")
                    _scenario.CreateNode<And>(stepInfo);
            }
            else
            {
                LoggerManager.LogInfo($"Step failed: {stepType} {stepInfo}. Error: {scenarioContext.TestError.Message}");

                if (stepType == "Given")
                    _scenario.CreateNode<Given>(stepInfo).Fail(scenarioContext.TestError.Message);
                else if(stepType == "When")
                    _scenario.CreateNode<When>(stepInfo).Fail(scenarioContext.TestError.Message);
                else if(stepType == "Then")
                    _scenario.CreateNode<Then>(stepInfo).Fail(scenarioContext.TestError.Message);
                else if(stepType == "And")
                    _scenario.CreateNode<And>(stepInfo).Fail(scenarioContext.TestError.Message);
            }
        }
    }
}
