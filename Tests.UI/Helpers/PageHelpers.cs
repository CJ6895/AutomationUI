using OpenQA.Selenium;

namespace Tests.UI.Helpers;
    public static class PageHelpers
    {
        public static string GetPageTitleText()
        {
            return Browser.GetPageTitle().Text;
        }

        public static void SelectInDropdown(FindBy dropdownMenu, string option)
        {
            // Find the parent element (dropdown) and click on it
            Browser.FindElement(dropdownMenu).Click();
            var dropdownOptions = Browser.FindElements(FindBy.TagName(Tag.li));
            
            // Loop through each dropdown option and click the one that matches the provided option
            foreach (var item in dropdownOptions)
            {
                if (item.Text.Equals(option, StringComparison.InvariantCultureIgnoreCase))
                {
                    item.Click();
                    break;
                }
            }
        }
    }
