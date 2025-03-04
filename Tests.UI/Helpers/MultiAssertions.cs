using System.Text;

namespace Tests.UI.Helpers;

public static class MultiAssertions
{
    public static void AssertAll(params Action[] assertions)
    {
        var failedMessages = new List<string>();
        TestLogger.LogMessage("Starting assertions...");

        foreach (var assertion in assertions)
        {
            try
            {
                assertion();
                TestLogger.LogMessage("✅ Assertion passed.");
            }
            catch (Exception ex)
            {
                TestLogger.LogMessage($"❌ Assertion failed: {ex.Message}");
                failedMessages.Add(ex.Message);
            }
        }

        // 🚨 Ensure that a failed assertion causes the test to fail!
        if (failedMessages.Any())
        {
            TestLogger.LogMessage($"❌ One or more assertions failed: {failedMessages.Count} failures.");
            throw new AssertFailedException($"One or more assertions failed:\n{string.Join("\n", failedMessages)}");
        }
    }



    public static void AreEqual(string expected, string actual)
    {
        if (expected != actual)
            throw new AssertFailedException($"Expected: <{expected}>. Actual: <{actual}>");
    }

    public static void IsFalse(bool condition, string message = "Condition was expected to be false but was true.")
    {
        if (condition)
            throw new AssertFailedException(message);
    }

    public static void IsTrue(bool condition, string message = "Condition was expected to be true but was false.")
    {
        if (!condition)
            throw new AssertFailedException(message);
    }

    public static void IsModuleDisplayed(bool isDisplayed, string moduleName)
    {
        if (!isDisplayed)
            throw new AssertFailedException($"{moduleName} module was not displayed.");
    }

    private static string FormatFailedAssertions(List<string> failedMessages)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < failedMessages.Count; i++)
        {
            sb.AppendLine($"Issue #{i + 1}: {failedMessages[i]}");
        }
        return sb.ToString();
    }
}