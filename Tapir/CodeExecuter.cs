using System;
using System.CodeDom.Compiler;
using System.Linq;
using System.Text;

namespace Tapir
{
    public class CodeExecuter
    {
        public static string Execute(string code, string type="A", string method="B")
        {
            try
            {
                var provider = CodeDomProvider.CreateProvider("CSharp");
                var options = new CompilerParameters();
                options.ReferencedAssemblies.Add("System.dll");
                options.GenerateExecutable = false;
                options.GenerateInMemory = true;
                var result = provider.CompileAssemblyFromSource(options, code);

                if (result.Errors.HasErrors)
                {
                    var sb = new StringBuilder();
                    foreach (CompilerError err in result.Errors)
                        sb.AppendLine(err.ToString());
                }

                return result.CompiledAssembly.GetType(type).GetMethod(method).Invoke(null, null).ToString();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
