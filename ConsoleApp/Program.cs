using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jurassic;
using System.IO;
using System.Data.SqlClient;
using prototypeExpander;

namespace ConsoleApp
{
    class Program
    {
        const string defscript = "../../def.js";

        static void Main(string[] args)
        {
            var file = args.Length >= 1 ? args[0] : defscript;
            Console.WriteLine($"C# started script: {file}");
            var src = File.ReadAllText(file);
            Run(src);
            Console.ReadKey();
        }

        public static void Run(string script)
        {
            try
            {
                var result = Engine.Evaluate(script);
                Console.WriteLine($"C# finished and said: {result}");
            }
            catch(Exception e)
            {
                Console.WriteLine($"C# Failure:{e.Message}\r\n{e.StackTrace}");
            }
        }

        private static ScriptEngine engine = null;
        private static readonly object threadlock = new object();
        public static ScriptEngine Engine
        {
            get
            {
                lock (threadlock)
                {
                    if (engine == null)
                    {
                        engine = new Jurassic.ScriptEngine();
                        Engine.SetGlobalValue("console", new Jurassic.Library.FirebugConsole(Engine));
                        engine.SetGlobalValue("ExpandedPrototype", new PrototypeConstructor(engine));
                    }
                    return engine;
                }
            }
        }
    }
}
