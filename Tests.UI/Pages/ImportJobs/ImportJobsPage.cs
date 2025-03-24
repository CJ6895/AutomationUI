using System.Threading.Tasks;
using Tests.UI.Helpers;

namespace Tests.UI.Pages.ImportJobs;
public class ImportJobsPage : BasePage
{
    #region Locators
    FindBy PageLinks => FindBy.CssSelector("body div[class='page-wrapper'] main[id='anchor-content'] div[class='fire_menu'] div a");
    FindBy AddJobButton => FindBy.Id("add");
    #endregion

    public ImportJobsPage ClickLinkFromGroup(string correctLinkText)
    {
        Browser.FindAll(PageLinks)
        .FirstOrDefault(link => link.Text.Contains(correctLinkText))
        ?.Click();

        return this;
    }

    public bool AreLinksDisplayed()
    {
        var links = Browser.FindAll(PageLinks).ToList();
        links.All(link => ImportJobsLinks.LinksList.Contains(link.Text));

        return true;
    }

    public bool IsUserTakenToLink(string expectedLink)
    {
        Browser.SwitchToLastTab();
        if (Browser.Instance.Url.ToString().Contains(expectedLink))
        {
            Browser.CloseTab();
            Browser.SwitchToFirstTab();
            BaseWait.WaitForElementToBePresent(AddJobButton, (int)WaitTime.Long);
            return true;
        }

        return false;
    }
}
