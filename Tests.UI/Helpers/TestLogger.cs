using Serilog;

namespace Tests.UI.Helpers;

public static class TestLogger
{
    public static void LogMessage(string message)
    {
        Console.WriteLine($"{DateTime.Now}: {message}");
    }

    public static void LogException(Exception ex)
    {
        string messageTemplate = "Error Message: " + ex?.Message + ". \n Error Source: " + ex?.Source + ". \n Error ExceptionThrown: " + ex?.GetType().Name + ". \n Error StackTrace: " + ex?.StackTrace + ". \n ";
        Log.Debug(messageTemplate);
    }
}
