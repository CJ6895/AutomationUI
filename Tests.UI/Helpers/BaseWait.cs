using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Tests.UI.Helpers;

    public static class BaseWait
    {
        public static void WaitForElementToBeVisible(FindBy findBy, int timeoutInSeconds)
        {
            var wait = new WebDriverWait(Browser.Instance, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(ExpectedConditions.ElementIsVisible(findBy.By));
        }

        public static void WaitForElementToBeClickable(By by, int timeoutInSeconds)
        {
            var wait = new WebDriverWait(Browser.Instance, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(ExpectedConditions.ElementToBeClickable(by));
        }

        public static void WaitForElementToBeInvisible(By by, int timeoutInSeconds)
        {
            var wait = new WebDriverWait(Browser.Instance, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(by));
        }

        public static void WaitForElementToBePresent(By by, int timeoutInSeconds)
        {
            var wait = new WebDriverWait(Browser.Instance, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(ExpectedConditions.ElementExists(by));
        }

        public static void WaitForElementToBeSelected(By by, int timeoutInSeconds)
        {
            var wait = new WebDriverWait(Browser.Instance, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(ExpectedConditions.ElementToBeSelected(by));
        }

        public static void WaitForElementToBeEnabled(IWebElement element, int timeoutInSeconds)
        {
            var wait = new WebDriverWait(Browser.Instance, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(_instance => element.Enabled);
        }

        public static void WaitForElementToBeDisabled(IWebElement element, int timeoutInSeconds)
        {
            var wait = new WebDriverWait(Browser.Instance, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(_instance => !element.Enabled);
        }

        public static void WaitForAlert(int timeoutInSeconds)
        {
            var wait = new WebDriverWait(Browser.Instance, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(ExpectedConditions.AlertIsPresent());
        }

        public static void WaitForUrlToContain(string url, int timeoutInSeconds)
        {
            var wait = new WebDriverWait(Browser.Instance, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(ExpectedConditions.UrlContains(url));
        }

        public static void WaitForUrlToBe(string url, int timeoutInSeconds)
        {
            var wait = new WebDriverWait(Browser.Instance, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(ExpectedConditions.UrlToBe(url));
        }
    }
    