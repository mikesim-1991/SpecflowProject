using Microsoft.Playwright;
using SpecflowProject.Utilities;

namespace SpecflowProject.Pages
{
    public class LoginPage 
    {
        private readonly IPage _page;

        private ILocator UsernameInput => _page.GetByPlaceholder("Username");
        private ILocator PasswordInput => _page.GetByPlaceholder("Password");
        private ILocator LoginButton => _page.GetByRole(AriaRole.Button, new() { Name = "Login" });
        private ILocator InventoryList => _page.Locator(".inventory_list");
        private ILocator ErrorMessageContainer => _page.Locator(".error-message-container");

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
            LoggerManager.LogInfo($"Entering credentials");

            await UsernameInput.FillAsync(username);
            await PasswordInput.FillAsync(password);
        }

        /// <summary>
        /// Clicks the login button on the login page to submit the entered credentials.
        /// </summary>
        /// <returns></returns>
        public async Task ClickLoginButton()
        {
            LoggerManager.LogInfo("Clicking the login button");

            await LoginButton.ClickAsync();
        }

        /// <summary>
        /// Home page verification after login. 
        /// This method checks if the home page is displayed by verifying the visibility of the inventory list.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> IsHomePageDisplayed()
        {
            LoggerManager.LogInfo("Verifying that the home page is displayed after login");

            return await InventoryList.IsVisibleAsync();
        }

        /// <summary>
        /// Error message verification after failed login attempt. 
        /// This method checks if the specified error message is displayed on the login page.
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public async Task<string> IsErrorMessageDisplayed()
        {
            LoggerManager.LogInfo($"Verifying that the error message  is displayed after failed login attempt");

            return await ErrorMessageContainer.TextContentAsync() ?? string.Empty; ;
        }
    }
}
