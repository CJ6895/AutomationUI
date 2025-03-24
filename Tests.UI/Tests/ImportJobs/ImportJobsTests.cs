using Tests.UI.Helpers;
using Tests.UI.Pages.ImportJobs;

namespace Tests.UI.Tests.ImportJobs;

[TestClass]
public class ImportJobsTests : BaseTests<ImportJobsPage>
{
    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        Initialize(context);
    }

    [TestInitialize]
    public void TestInitialize()
    {
        TestLogger.LogMessage($"{TestContext.TestName} started successfully");
    }

    [TestCleanup]
    public void CleanupTest()
    {
        TestCleanup();
    }

    [ClassCleanup]
    public static void ClassCleanup() => Cleanup();

    [TestMethod]
    public void CheckLinks_GivenLandingOnImportJobsPage_DisplaysCorrectLinks()
    {
        // Assert
        Assert.IsTrue(Page.AreLinksDisplayed(), "Links are not displayed");
    }

    [TestMethod]
    [DataRow(ImportJobsLinks.FAQ, "firebear-import-export-extension-faq")]
    [DataRow(ImportJobsLinks.FireBear, "firebear-improved-import-export-for-magento-2-extension-change-log")]
    [DataRow(ImportJobsLinks.GuideList, "guide-list")]
    [DataRow(ImportJobsLinks.SampleFiles, "sample-files")]
    [DataRow(ImportJobsLinks.UserManual, "manual")]
    [DataRow(ImportJobsLinks.YourAccount, "https://firebearstudio.com/customer/account/login/")]
    public void ClickUserManualLink_GivenLandingOnImportJobsPage_DisplaysSuccessfully(string linkText, string expectedLink)
    {
        // Act
        Page.ClickLinkFromGroup(linkText);

        // Assert
        Assert.IsTrue(Page.IsUserTakenToLink(expectedLink), "Link content is not displayed");
    }
}
