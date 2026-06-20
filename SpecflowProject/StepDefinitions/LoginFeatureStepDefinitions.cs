using Microsoft.Playwright;
using NUnit.Framework;
using SpecflowProject.Config;
using SpecflowProject.Pages;
using SpecflowProject.Utilities;

namespace SpecflowProject.StepDefinitions
{
    /// <summary>
    /// LoginFeatureStepDefinitions class contains step definitions for the login feature of the application.
    /// </summary>
    [Binding]
    public class LoginFeatureStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly LoginPage _loginPage;

        public LoginFeatureStepDefinitions(
            ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            var page =
                _scenarioContext["Page"] as IPage;

            _loginPage = new LoginPage(page);
        }

        /// <summary>
        /// Navigates to the login page of the application.
        /// </summary>
        /// <returns></returns>
        [Given("I have navigated to the login page")]
        public async Task GivenIHaveNavigatedToTheLoginPage()
        {
            await _loginPage.NavigateToLoginPage(ConfigurationLoader.AppSettings.BaseUrl);
        }

        /// <summary>
        /// Enters the provided username and password into the login form fields.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [When(@"I enter a username ""(.*)"" and password ""(.*)""")]
        public async Task WhenIEnterValidUserAndPass(string username, string password)
        {
            await _loginPage.EnterCredentials(username, password);
        }

        /// <summary>
        /// Login button click action. 
        /// This method simulates a user clicking the login button on the login page.
        /// </summary>
        /// <returns></returns>
        [When("I tap Login")]
        public async Task WhenITapLogin()
        {
            await _loginPage.ClickLoginButton();
        }

        /// <summary>
        /// Home page verification. 
        /// This method checks if the user has successfully logged in by waiting for a specific element on the home page to be visible.
        /// </summary>
        /// <returns></returns>
        [Then("I should see home page")]
        public async Task ThenIShouldSeeHomePage()
        {
            LoggerManager.LogInfo("Asserting that the home page is displayed after login");

            Assert.That(await _loginPage.IsHomePageDisplayed(), Is.True);
        }

        /// <summary>
        /// Error message verification. 
        /// This method checks if an error message indicating invalid credentials is displayed on the login page after a failed login attempt.
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        [Then(@"I should see an error message ""(.*)"" indicating invalid credentials")]
        public async Task ThenIShouldSeeAnErrorMessageIndicatingInvalidCredentials(string errorMessage)
        {
            LoggerManager.LogInfo($"Asserting that the error message '{errorMessage}' is displayed after failed login attempt");

            var errorContainer = await _loginPage.IsErrorMessageDisplayed();

            Assert.That(errorContainer.Contains(errorMessage));
        }
    }
}
