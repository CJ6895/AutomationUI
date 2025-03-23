using System.Diagnostics;
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

    public static void WaitForElementToBeClickable(FindBy findBy, int timeoutInSeconds)
    {
        var wait = new WebDriverWait(Browser.Instance, TimeSpan.FromSeconds(timeoutInSeconds));
        wait.Until(ExpectedConditions.ElementToBeClickable(findBy.By));
    }

    public static void WaitForElementToBeInvisible(FindBy findBy, int timeoutInSeconds)
    {
        var wait = new WebDriverWait(Browser.Instance, TimeSpan.FromSeconds(timeoutInSeconds));
        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(findBy.By));
    }

    public static void WaitForElementToBePresent(FindBy findBy, int timeoutInSeconds)
    {
        var wait = new WebDriverWait(Browser.Instance, TimeSpan.FromSeconds(timeoutInSeconds));
        wait.Until(ExpectedConditions.ElementExists(findBy.By));
    }

    public static void WaitForElementToBeSelected(FindBy findBy, int timeoutInSeconds)
    {
        var wait = new WebDriverWait(Browser.Instance, TimeSpan.FromSeconds(timeoutInSeconds));
        wait.Until(ExpectedConditions.ElementToBeSelected(findBy.By));
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

    public static void WaitForTabToOpen(int timeoutInSeconds)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        while (Browser.Instance.WindowHandles.Count < 2)
        {
            if (stopwatch.Elapsed.TotalSeconds >= timeoutInSeconds)
            {
                throw new TimeoutException($"Timed out after {timeoutInSeconds} seconds waiting for a new tab to open.");
            }
        }
    }
    
    public static void WaitForPageToLoad(int timeoutInSeconds)
    {
        WebDriverWait wait = new WebDriverWait(Browser.Instance, TimeSpan.FromSeconds(timeoutInSeconds));
        wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
    }
}
