

using OpenQA.Selenium;

namespace  Tests.UI;

public class FindBy
{
public By? By { get; private set; }

private static FindBy CreateSeleniumBy(By by)
{
    FindBy findBy = new FindBy
    {
        By = by
    };
    return findBy;
}

// SELENIUM'S BY METHODS
public static FindBy ClassName(string className)
{
    return CreateSeleniumBy(By.ClassName(className));
}

public static FindBy CssSelector(string cssSelector)
{
    return CreateSeleniumBy(By.CssSelector(cssSelector));
}

public static FindBy Id(string id)
{
    return CreateSeleniumBy(By.Id(id));
}

public static FindBy LinkText(string linkText)
{
    return CreateSeleniumBy(By.LinkText(linkText));
}

public static FindBy Name(string name)
{
    return CreateSeleniumBy(By.Name(name));
}

public static FindBy PartialLinkText(string partialLinkText)
{
    return CreateSeleniumBy(By.PartialLinkText(partialLinkText));
}

public static FindBy TagName(string tagName)
{
    return CreateSeleniumBy(By.TagName(tagName));
}

public static FindBy XPath(string xPath)
{
    return CreateSeleniumBy(By.XPath(xPath));
}

// HELPFUL FIND BY METHODS
public static FindBy WhereHtmlAttributeEndsWith(string attrName, string partialId)
{
    return CreateSeleniumBy(By.CssSelector($"[{attrName}$={partialId}]"));
}

public static FindBy WhereHtmlAttributeContains(string attrName, string partialId)
{
    return CreateSeleniumBy(By.CssSelector($"[{attrName}*={partialId}]"));
}

public static FindBy WhereHtmlAttributeStartsWith(string attrName, string partialId)
{
    return CreateSeleniumBy(By.CssSelector($"[{attrName}^={partialId}]"));
}

public static FindBy WhereIdEndsWith(string partialId)
{
    return CreateSeleniumBy(By.CssSelector($"[id$={partialId}]"));
}

public static FindBy WhereIdContains(string partialId)
{
    return CreateSeleniumBy(By.CssSelector($"[id*={partialId}]"));
}

public static FindBy WhereIdStartsWith(string partialId)
{
    return CreateSeleniumBy(By.CssSelector($"[id^={partialId}]"));
}

public static FindBy WhereClassNameEndsWith(string partialClassName)
{
    return CreateSeleniumBy(By.CssSelector($"[class$={partialClassName}]"));
}

public static FindBy WhereNameEndsWith(string partialName)
{
    return CreateSeleniumBy(By.CssSelector($"[name$={partialName}]"));
}

public static FindBy WhereClassNameContains(string partialClassName)
{
    return CreateSeleniumBy(By.CssSelector($"[class*={partialClassName}]"));
}

public static FindBy WhereClassNameStartsWith(string partialClassName)
{
    return CreateSeleniumBy(By.CssSelector($"[class^={partialClassName}]"));
}

public static FindBy HtmlAttribute(string attrName, string attrValue)
{
    return CreateSeleniumBy(By.XPath($"//*[@{attrName} = '{attrValue}']"));
}

public static FindBy Href(string hrefValue)
{
    return CreateSeleniumBy(By.CssSelector($"[href='{hrefValue}']"));
}

public static FindBy FollowingSibling(string siblingXPath, string followingSiblingXPath)
{
    return CreateSeleniumBy(By.XPath($"//{siblingXPath}//following-sibling::{followingSiblingXPath}"));
}
}