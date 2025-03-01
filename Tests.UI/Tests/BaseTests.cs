using Tests.UI.Helpers;

namespace Tests.UI.Tests;

[TestClass]
public abstract class BaseTests<T> where T : BasePage, new()
{
    public TestContext TestContext { get; set; }

    protected T Page { get; set; }

    protected BaseTests() => Page = new T();

    public static void Initialize(TestContext context)
    {
        try
        {
            Browser.LaunchApplication();
        }
        catch (Exception ex)
        {
            TestLogger.LogMessage($"Error occurred while launching the application: {ex.Message}");
        }
    }

    public static void Cleanup()
    {
        try
        {
            Browser.CloseBrowser();
        }
        catch (Exception ex)
        {
            TestLogger.LogMessage($"Error occurred during cleanup: {ex.Message}");
        }
    }

    public void TestCleanup()
    {
        if (TestContext.CurrentTestOutcome != UnitTestOutcome.Passed)
        {
            var testName = TestContext.TestName;
            var fileName = $"{testName}.png";
            Browser.TakeScreenshot(fileName);
            TestLogger.LogMessage($"Screenshot has been taken: {fileName}");
        }
        else
        {
            TestLogger.LogMessage("Test has been passed successfully.");
        }
    }
}
