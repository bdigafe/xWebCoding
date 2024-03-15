using System;
using System.Reflection;
using log4net;
 
namespace xWebCodingLab
{
    public static class Logger
    {
        // Instantiate logger
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void Info(string message)
        {
            LogInfo(message, false);
        }
        public static void Info(string message, bool logToConsole)
        {
            LogInfo(message, logToConsole);
        }

        public static void Error(string message)
        {
            LogError(message, null, false);
        }
        public static void Error(string message, bool logToConsole)
        {
            LogError(message, null, logToConsole);
        }
        public static void Error(string message, Exception ex) 
        {
            LogError(message, ex, false);
        }
        public static void Error(string message, Exception ex, bool logToConsole)
        {
            LogError(message, ex, logToConsole);
        }

        private static void LogInfo(string message, bool logToConsole)
        {
            log.Info(message);

            if (logToConsole)
            {
                Console.WriteLine(message);
            }
        }
        private static void LogError(string message, Exception ex, bool logToConsole)
        {
            log.Error(message, ex);

            if (logToConsole)
            {
                Console.WriteLine(message);
            }
        }
    }
}
