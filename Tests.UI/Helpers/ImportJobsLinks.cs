using System;

namespace Tests.UI.Helpers;

public static class ImportJobsLinks
{
    public const string FireBear = "Firebear Improved Import / Export v. 3.8.6";
    public const string UserManual = "User manual";
    public const string GuideList = "Guide list";
    public const string SampleFiles = "Sample files";
    public const string FAQ = "FAQ";
    public const string YourAccount = "Your account";

    public static List<string> LinksList = new() 
    {
        FireBear,
        UserManual,
        GuideList,
        SampleFiles,
        FAQ,
        YourAccount
    };
}
