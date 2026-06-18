using Microsoft.Playwright;
using NUnit.Framework;
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
        private IPage _page;

        public LoginFeatureStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _page = _scenarioContext["Page"] as IPage;
        }


        /// <summary>
        /// Navigates to the login page of the application.
        /// </summary>
        /// <returns></returns>
        [Given("I have navigated to the login page")]
        public async Task GivenIHaveNavigatedToTheLoginPage()
        {

            await _page.GotoAsync(ConstantStrings.BaseUrl);
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

            await _page.FillAsync("#user-name", username);
            await _page.FillAsync("#password", password);
        }

        /// <summary>
        /// Login button click action. 
        /// This method simulates a user clicking the login button on the login page.
        /// </summary>
        /// <returns></returns>
        [When("I tap Login")]
        public async Task WhenITapLogin()
        {
            await _page.ClickAsync("#login-button");    
            
        }

        /// <summary>
        /// Home page verification. 
        /// This method checks if the user has successfully logged in by waiting for a specific element on the home page to be visible.
        /// </summary>
        /// <returns></returns>
        [Then("I should see home page")]
        public async Task ThenIShouldSeeHomePage()
        {
            Assert.That(await _page.IsVisibleAsync(".inventory_list"), Is.True);
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
            var errorContainer = await _page.Locator(".error-message-container").InnerHTMLAsync();

            Assert.That(errorContainer.Contains(errorMessage));
        }
    }
}
