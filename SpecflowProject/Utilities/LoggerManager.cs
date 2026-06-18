using log4net;
using log4net.Config;

namespace SpecflowProject.Utilities
{
    /// <summary>
    /// Logger class for logging messages using log4net. 
    /// This class provides a centralized way to log messages throughout the application, making it easier to track and debug issues. 
    /// You can use this logger to log information, warnings, errors, and other relevant messages during the execution of your tests or application.
    /// </summary>
    internal class LoggerManager
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LoggerManager));

        /// <summary>
        /// Logs a message with the specified log type. The default log type is Info.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="logType">The type of log message (Info, Warning, Error, etc.).</param>
        public static void LogInfo(string message, LogTypeEnum logType = LogTypeEnum.Info)
        {

            var logRepository = LogManager.GetRepository(System.Reflection.Assembly.GetExecutingAssembly());

            XmlConfigurator.Configure(logRepository, new FileInfo(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Resources\\log4net.config"));

            if (logType == LogTypeEnum.Warning)
            {
                log.Warn(message);
            }
            else if (logType == LogTypeEnum.Error)
            {
                log.Error(message);
            }
            else if (logType == LogTypeEnum.Fatal)
            {
                log.Fatal(message);
            }
            else
            {
                log.Info(message);
            }
        }

        //Todo: Add more logging methods for different log levels (Debug, Trace, etc.) if needed.
        //Todo: Clear the log file at the start of each test run to avoid cluttering the logs with old messages.
    }
}
