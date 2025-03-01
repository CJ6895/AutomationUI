using System.Text;

namespace Tests.UI.Helpers;

public static class MultiAssertions
{
    private static readonly List<string> _failedMessages;

    public static string FailedAssertions => FormatFailedAssertions();

    public static bool HasAssertions => _failedMessages.Count > 0;

    static MultiAssertions()
    {
        _failedMessages = new List<string>();
    }

    public static void AssertAll(params Action[] assertions)
    {
        TestLogger.LogMessage("Starting assertions...");
        _failedMessages.Clear();

        foreach (var assertion in assertions)
        {
            try
            {
                assertion();
                TestLogger.LogMessage("Assertion passed.");
            }
            catch (Exception ex)
            {
                TestLogger.LogMessage($"Assertion failed: {ex.Message}");
                _failedMessages.Add(ex.Message);  
            }
        }

        if (_failedMessages.Any())
        {
            TestLogger.LogMessage($"One or more assertions failed: {_failedMessages.Count} failures.");
            throw new AssertFailedException($"One or more assertions failed:\n{string.Join("\n", _failedMessages)}");
        }
    }

    public static string AreEqual(string expectedResult, string actualResult)
    {
        if (expectedResult != actualResult)
            _failedMessages.Add($"Expected: <{expectedResult}>. Actual: <{actualResult}>");
        return FailedAssertions;
    }

    public static string IsModuleDisplayed(bool isModuleDisplayed, string moduleName)
    {
        if (!isModuleDisplayed)
            _failedMessages.Add($"{moduleName} module was not displayed.");
        return FailedAssertions;
    }

    public static void AddFailedMessage(string message)
    {
        _failedMessages.Add(message);
    }

    private static string FormatFailedAssertions()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < _failedMessages.Count; i++)
        {
            sb.Append($"{Environment.NewLine}Issue #{i + 1}:{_failedMessages[i]}{Environment.NewLine}");
        }
        return sb.ToString();
    }
}
