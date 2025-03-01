
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Tests.UI.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using OpenQA.Selenium.Internal;

namespace Tests.UI;

public static class Browser
{
    static FindBy PageTitle => FindBy.CssSelector(".page-title");

    private static IWebDriver _instance;
    private static DriverService _driverService;
    private static string _mainWindow;

    public static IWebDriver Instance
    {
        get
        {
            if (_instance == null)
            {
                _driverService = ChromeDriverService.CreateDefaultService();
                _driverService.HideCommandPromptWindow = true;
                var chromeOptions = new ChromeOptions();
                chromeOptions.PageLoadStrategy = PageLoadStrategy.Normal;
                _instance = new ChromeDriver((ChromeDriverService)_driverService, chromeOptions);
                _mainWindow = _instance.CurrentWindowHandle;
            }
            return _instance;
        }
    }

    public static void Visit(string url)
    {
        _instance.Navigate().GoToUrl(url);
    }

    public static void CloseBrowser()
    {
        try { _instance.Quit(); }
        catch { }

        try { _instance.Close(); }
        catch { }

        try { _driverService.Dispose(); }
        catch { }
    }

    public static IWebElement GetPageTitle() => FindElement(PageTitle);

    public static IWebElement GetElement(FindBy element) => _instance.FindElement(element.By);

    public static string GetElementText(FindBy element) => FindElement(element).Text;
    public static bool IsElementDisplayed(FindBy element)
    {
        return FindElement(element).Displayed;
    }

    public static bool IsPageTitleDisplayed()
    {
        return IsElementDisplayed(PageTitle);
    }

    public static void LaunchApplication()
    {
        TestLogger.LogMessage("Launching browser has been started.");
        Instance.Manage().Window.Maximize();
        string url = ProjectSetup.Instance.LaunchUrl;
        Instance.Navigate().GoToUrl(url);


        if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
        {
            TestLogger.LogMessage("Launching browser has been completed.");
        }
        else
        {
            throw new ValidationException($"URL was not entered successfully. Actual URL: {UriComponents.AbsoluteUri}");
        }
    }

    public static void TakeScreenshot(string fileName)
    {
        Screenshot screenshot = ((ITakesScreenshot)_instance).GetScreenshot();
        screenshot.SaveAsFile(fileName);
    }

    public static void SwitchToMainWindow()
    {
        _instance.SwitchTo().Window(_mainWindow);
    }

    public static void SwitchToTab(int tabIndex)
    {
        _instance.SwitchTo().Window(_instance.WindowHandles[tabIndex]);
    }

    public static IWebElement FindElement(FindBy findBy)
    {
        return _instance.FindElement(findBy.By);
    }

    public static IWebElement FindElementWithShortWait(FindBy findBy)
    {
        BaseWait.WaitForElementToBeVisible(findBy, (int)WaitTime.Medium);
        return _instance.FindElement(findBy.By);
    }

    public static ReadOnlyCollection<IWebElement> FindElements(FindBy findBy)
    {
        return _instance.FindElements(findBy.By);
    }

    public static List<IWebElement> FindChildElementsBy(this IWebElement parentElement, FindBy findBy, int timeOut = (int)WaitInSeconds.Thirty)
    {
        List<IWebElement> children = null;
        bool childrenFound = false;
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();
        while (!childrenFound)
        {
            children = parentElement.FindElements(findBy.By).ToList();
            if (children?.Count > 0)
                break;

            if (stopWatch.ElapsedMilliseconds > (int)timeOut * 1000)
            {
                throw new Exception($"Unable to find child elements with locator: {findBy.By}");
            }
        }
        stopWatch.Stop();

        return children;
    }

    public static void Click(FindBy findBy)
    {
        FindElement(findBy).Click();
    }

    public static void ClickWithJavaScript(FindBy findBy)
    {
        var element = FindElement(findBy);
        ((IJavaScriptExecutor)_instance).ExecuteScript("arguments[0].click();", element);
    }

    public static void ClickWithShortWait(FindBy findBy)
    {
        BaseWait.WaitForElementToBeClickable(findBy.By, (int)WaitTime.Medium);
        Click(findBy);
    }

    public static void TypeText(FindBy findBy, string text)
    {
        FindElement(findBy).SendKeys(text);
    }
}
