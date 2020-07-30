using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jurassic;
using System.IO;
using System.Data.SqlClient;
using Jurassic.Library;
using JsMan;
using JsdvApi32;
using System.Diagnostics;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new Jurassic.ScriptEngine();

            new Dictionary<string, ObjectInstance>
            {
                {"console", new Jurassic.Library.FirebugConsole(engine) },
                {"advapi32", new JsConstructor<AdvApi32>(engine) }
            }
            .ToList()
            .ForEach(x => engine.SetGlobalValue(x.Key, x.Value));

            var file = "";
            #if DEBUG
                file = args.Length >= 1 ? args[0] : "../../def.js";
            #else
                if (args.Length < 0){
                    Console.WriteLine("error");
                    return;
                }
                file = args[0];
            #endif

            try
            {
                var result = engine.Evaluate<CreateServiceOut>(File.ReadAllText(file));
                var x = result.Message;
                Console.WriteLine($"C# Result: {result.Result}");
                Console.WriteLine($"C# ErrorCode: {result.ErrorCode}");
                Console.WriteLine($"C# Message: {result.Message}");
            }
            catch(JavaScriptException e)
            {
                Console.WriteLine("C# JavaScriptException: " + e.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("C# Exception: " + e.Message);
            }

            Console.ReadKey();
        }
    }
}
