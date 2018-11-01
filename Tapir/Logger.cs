using System;
using System.Collections.Generic;
using System.Text;

namespace Tapir
{
    public class Logger
    {
        public Action<string> logFunc;
        public LogLevel Level;
        public Logger BaseLogger;
        public string Name;

        public string Header = "[{Level}] {Date}";
        public string Tail = "\n";
        public string DateFormat = "MM/dd HH:mm:ss";

        public static Logger DefaultLogger;

        public Logger(LogLevel level, Action<string> func, string name = "", Logger baselog = null)
        {
            Level = level;
            logFunc = func;
            Name = name;
            BaseLogger = baselog;
        }

        public void Log(LogLevel level, string msg)
        {
            BaseLogger?.Log(level, msg);

            if (level > Level)
                return;

            var formatted = new StringBuilder(Header);
            formatted = formatted.Replace("{Level}", level.ToString());
            formatted = formatted.Replace("{Date}", DateTime.Now.ToString(DateFormat));
            logFunc($"{formatted.ToString()}{msg}{Tail}");
        }

        public void LogInfo(string msg = "")
            => Log(LogLevel.Info, msg);

        public void LogWarning(string msg = "")
            => Log(LogLevel.Warning, msg);

        public void LogError(string msg = "")
            => Log(LogLevel.Error, msg);

        public static void Info(string msg = "")
            => DefaultLogger.LogInfo(msg);

        public static void Warning(string msg = "")
            => DefaultLogger.LogWarning(msg);

        public static void Error(string msg = "")
            => DefaultLogger.LogError(msg);
    }
}
