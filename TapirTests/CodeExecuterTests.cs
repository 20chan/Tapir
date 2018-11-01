using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tapir.Tests
{
    [TestClass()]
    public class CodeExecuterTests
    {
        [TestMethod()]
        public void ExecuteTest()
        {
            var code = "public class A{public static string B() { return \"Hello\"; }}";
            var res = CodeExecuter.Execute(code);
            Assert.AreEqual("Hello", res);
        }

        [TestMethod()]
        public void ExecuteFailTest()
        {
            var code = "public class A { public static string  }";
            var res = CodeExecuter.Execute(code);
            Assert.IsTrue(res.Contains("Exception"));
        }
    }
}