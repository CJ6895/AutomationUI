using System.Text;

namespace Tests.UI.Helpers;

public static class FailTest
{
    private const string GenericMessage = "Forcefully failing test due to unsuccessful action.";

    /// <summary>
    /// Forces the current running test to fail and terminate.
    /// </summary>
    /// <param name="message">Explanation of why the test should fail and be terminated</param>
    public static void Fail(string message)
    {
        TestLogger.LogMessage(message);
        CallAssertFail(message);
    }

    /// <summary>
    /// Forces the current running test to fail and terminate.
    /// </summary>
    /// <param name="ex">Exception that was caught which determined that the test should fail</param>
    /// <param name="message">Explanation of why the test should fail and be terminated</param>
    public static void Fail(string customMessage, Exception ex)
    {
        TestLogger.LogException(ex);
        CallAssertFail(customMessage, $"{ex?.GetType().Name}::{ex?.Message}");
    }

    private static void CallAssertFail(string message)
    {
        CallAssertFail(message, null);
    }

    private static void CallAssertFail(string customMessage, string exceptionMessage = null)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(
        $"{GenericMessage} \n " +
        $"Custom Message: {customMessage}");

        if (exceptionMessage != null)
            builder.Append($"\n Exception Message: {exceptionMessage}");

        Assert.Fail(builder.ToString());
    }
}
