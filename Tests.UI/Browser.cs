using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Tests.UI.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

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
        Instance.Navigate().GoToUrl(url);
    }

    public static void CloseBrowser()
    {
        try { Instance.Quit(); }
        catch { }

        try { Instance.Close(); }
        catch { }

        try { _driverService.Dispose(); }
        catch { }
    }

    public static FindBy GetPageTitle() => PageTitle;

    public static IWebElement GetElement(FindBy element) => Instance.FindElement(element.By);

    public static string GetElementText(FindBy element) => Find(element).Text;
    public static bool IsElementDisplayed(FindBy element)
    {
        BaseWait.WaitForElementToBeVisible(element, (int)WaitTime.Medium);
        return Find(element).Displayed;
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
        if (Instance.WindowHandles.Count > 1)
            Instance.SwitchTo().Window(_mainWindow);
    }

    public static void SwitchToFirstTab()
    {
        Instance.SwitchTo().Window(Instance.WindowHandles.First());
    }

    public static void SwitchToLastTab()
    {
        Instance.SwitchTo().Window(Instance.WindowHandles.Last());
    }

    public static void CloseTab()
    {
        Instance.Close();
    }

    public static IWebElement Find(this FindBy findBy)
    {
        return Instance.FindElement(findBy.By);
    }

    public static IWebElement FindElementWithShortWait(FindBy findBy)
    {
        BaseWait.WaitForElementToBeVisible(findBy, (int)WaitTime.Short);
        return Instance.FindElement(findBy.By);
    }

    public static List<IWebElement> FindAll(FindBy findBy)
    {
        return Instance.FindElements(findBy.By).ToList();
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

    public static void ClickElement(this IWebElement element)
    {
        element.Click();
    }
    
        public static void ClickElementWithJS(this IWebElement element)
    {
        try
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)_instance;
            executor.ExecuteScript("arguments[0].click();", element);
        }
        catch (Exception ex)
        {
            FailTest.Fail($"Unable to successfully click element via JavaScript with locator", ex);
        }
    }

    public static void ClickWithShortWait(FindBy findBy)
    {
        BaseWait.WaitForElementToBeClickable(findBy, (int)WaitTime.Medium);
        findBy.Find().ClickElement();
    }

    public static void TypeText(this FindBy findBy, string text)
    {
        findBy.Find().SendKeys(text);
    }
}
