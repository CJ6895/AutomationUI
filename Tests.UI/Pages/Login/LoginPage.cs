using OpenQA.Selenium;
using Tests.UI.Helpers;

namespace Tests.UI.Pages.Login;

    public class LoginPage : BasePage
    {
        #region Locators
        FindBy UserNameField => FindBy.Id("username");
        FindBy PasswordField => FindBy.Id("login");
        FindBy LoginButton => FindBy.CssSelector(".action-login.action-primary");
        FindBy UserDropdown => FindBy.CssSelector(".admin-user.admin__action-dropdown-wrap");
        FindBy LoggedOutMessage => FindBy.CssSelector("div[data-ui-id='messages-message-success']");
        FindBy LoginErrorMessage => FindBy.CssSelector("div[data-ui-id='messages-message-error']");
        #endregion

        public IWebElement GetLoggedOutMessage() => LoggedOutMessage.Find();
        public IWebElement GetLoginErrorMessage() => LoginErrorMessage.Find();

        public void Login(string userName, string password)
        {
            try
            {
                if (Browser.IsElementDisplayed(UserNameField))
                {
                    UserNameField.TypeText(userName);
                    PasswordField.TypeText(password);
                    Browser.Find(LoginButton).Click();
                    BaseWait.WaitForElementToBeVisible(UserDropdown, (int)WaitTime.Medium);
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                TestLogger.LogMessage($"LoginPage is not displayed" + ex.Message);
            }
        }

        public void LogOut()
        {
            try
            {
                if(Browser.IsElementDisplayed(UserDropdown))
            {
                PageHelpers.SelectInDropdown(UserDropdown, ProfileDropdownSelections.SignOut);
                BaseWait.WaitForUrlToBe("https://magento2demo.firebearstudio.com/admin_k1b5n8/admin/", (int)WaitTime.Medium);
            }
            else
            {
                return;
            }
            }
            catch (Exception ex)
            {
                TestLogger.LogMessage("User Dropdown is not displayed or User is not logged in" + ex.Message);
            }
        }

        public bool IsUserLoggedIn()
        {
            return Browser.Find(Browser.GetPageTitle()).Displayed;
        }

        public bool IsUserLoggedOut()
        {
            return Browser.IsElementDisplayed(LoginButton);
        }

        public bool IsLoginErrorMessageDisplayed()
        {
            return Browser.IsElementDisplayed(LoginErrorMessage);
        }
    }
