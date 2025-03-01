
namespace Tests.UI.Helpers;

    public static class TestLogger
    {
        public static void LogMessage(string message)
        {
            Console.WriteLine($"{DateTime.Now}: {message}");
        }
    }
