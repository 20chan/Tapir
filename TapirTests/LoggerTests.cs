using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

using static Tapir.Logger;

namespace Tapir.Tests
{
    [TestClass()]
    public class LoggerTests
    {
        [TestMethod]
        public void TestLog()
        {
            var builder = new StringBuilder();
            var logger = new Logger(
                LogLevel.Warning,
                (s) => builder.Append(s)
            );
            logger.Header = "[[{Level}]] ";

            logger.LogInfo("No not this one");
            logger.LogWarning("Warning");
            logger.LogError("Error!");

            Assert.AreEqual("[[Warning]] Warning\n[[Error]] Error!\n", builder.ToString());
        }

        [TestMethod]
        public void TestBaseLog()
        {
            var builder1 = new StringBuilder();
            var builder2 = new StringBuilder();

            var logger1 = new Logger(
                LogLevel.Error,
                (s) => builder1.Append(s)
            );
            var logger2 = new Logger(
                LogLevel.Warning,
                (s) => builder2.Append(s),
                baselog: logger1
            );

            logger1.Header = "{Level}";
            logger2.Header = "!{Level} ";

            logger2.LogWarning("Warning");
            logger2.LogError("Error");
            logger1.LogError("What");
            logger2.LogInfo("heh");

            Assert.AreEqual("ErrorError\nErrorWhat\n", builder1.ToString());
            Assert.AreEqual("!Warning Warning\n!Error Error\n", builder2.ToString());
        }

        [TestMethod]
        public void TestDateLog()
        {
            var builder = new StringBuilder();
            var logger = new Logger(LogLevel.Warning, (s) => builder.Append(s));
            DefaultLogger = logger;

            Warning("warning");
        }
    }
}