using Tests.UI.Pages.Login;
using Tests.UI.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.UI.Tests.Login;

[TestClass]
public class LoginTests : BaseTests<LoginPage>
{
    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        LoginClassInitialize(context);
    }

    [TestInitialize]
    public void TestInitialize()
    {
        if (TestContext.TestName == "VerifyLogin_GivenInvalidCredentials_DisplaysErrorMessage")
        {
            return;
        }

        Page.Login(ProjectSetup.Instance.UserName, ProjectSetup.Instance.Password);
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
    public void VerifyLogin_GivenValidCredentials_SystemModuleDisplayed()
    {
        // Arrange
        const string PageTitleText = "Import Jobs";

        // Assert
        MultiAssertions.AssertAll(
            () => Assert.IsTrue(Page.IsUserLoggedIn(), "User is not logged in"),
            () => Assert.AreEqual(PageTitleText, PageHelpers.GetPageTitleText(), "User was not redirected to default page")
        );
    }

    [TestMethod]
    public void VerifyLogOut_GivenUserDropdownSelection_DisplaysSuccessMessage()
    {
        // Arrange
        const string successMessage = "You have logged out.";

        // Act
        Page.LogOut();

        // Assert
        MultiAssertions.AssertAll(
            () => Assert.IsTrue(Page.IsUserLoggedOut(), "Login button is not displayed"),
            () => Assert.AreEqual(successMessage, Page.GetLoggedOutMessage().Text, "Logout message is different than expected")
        );
    }

    [TestMethod]
    public void VerifyLogin_GivenInvalidCredentials_DisplaysErrorMessage()
    {
        // Arrange
        const string errorMessage = "The account sign-in was incorrect";

        // Act
        Page.Login(ProjectSetup.Instance.InvalidUserName, ProjectSetup.Instance.InvalidPassword);

        // Assert
        MultiAssertions.AssertAll(
            () => Assert.IsTrue(Page.IsUserLoggedOut(), "User is logged into the application"),
            () => Assert.IsTrue(Page.IsLoginErrorMessageDisplayed(), "Error Message was not displayed"),
            () => Assert.IsTrue(Page.GetLoginErrorMessage().Text.Contains(errorMessage), "Error message changed")
        );
    }
}
