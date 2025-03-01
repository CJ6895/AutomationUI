

namespace Tests.UI;

public abstract class BasePage
{
    public TPage NavigateViaDirectLink<TPage>(string url) where TPage : BasePage, new()
    {
        Browser.Visit(url);
        return new TPage();
    }
}