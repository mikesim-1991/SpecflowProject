using Microsoft.Playwright;
using NUnit.Framework;
using SpecflowProject.Utilities;

namespace SpecflowProject.Pages
{
    public class LoginPage 
    {
        private IPage _page;

        public LoginPage(IPage page)
        {
            _page = page;
        }

        /// <summary>
        /// Navigates to the login page using the provided URL.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task NavigateToLoginPage(string url)
        {
            LoggerManager.LogInfo($"Navigating to the login page: {url}");

            await _page.GotoAsync(url);
        }

        /// <summary>
        /// Enters the provided username and password into the respective input fields on the login page.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task EnterCredentials(string username, string password)
        {
            LoggerManager.LogInfo($"Entering username: {username} and password: {password}");

            await _page.GetByPlaceholder("Username").FillAsync(username);
            await _page.GetByPlaceholder("Password").FillAsync(password);
        }

        /// <summary>
        /// Clicks the login button on the login page to submit the entered credentials.
        /// </summary>
        /// <returns></returns>
        public async Task ClickLoginButton()
        {
            LoggerManager.LogInfo("Clicking the login button");

            await _page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();
        }

        /// <summary>
        /// Home page verification after login. 
        /// This method checks if the home page is displayed by verifying the page title and the visibility of the inventory list.
        /// </summary>
        /// <returns></returns>
        public async Task IsHomePageDisplayed()
        {
            LoggerManager.LogInfo("Verifying that the home page is displayed after login");

            var pageTitle = await _page.GetByText("Swag Labs").InnerHTMLAsync();
            var inventoryListVisible = await _page.IsVisibleAsync(".inventory_list");

            Assert.That(pageTitle, Is.EqualTo("Swag Labs"));
            Assert.That(inventoryListVisible, Is.True);
        }

        /// <summary>
        /// Error message verification after failed login attempt. 
        /// This method checks if the specified error message is displayed on the login page.
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public async Task IsErrorMessageDisplayed(string errorMessage)
        {
            LoggerManager.LogInfo($"Verifying that the error message '{errorMessage}' is displayed after failed login attempt");

            var errorContainer = await _page.Locator(".error-message-container").InnerHTMLAsync();

            Assert.That(errorContainer.Contains(errorMessage));
        }
    }
}
