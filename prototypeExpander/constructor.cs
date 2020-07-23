using Jurassic;
using Jurassic.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prototypeExpander
{
    public class PrototypeConstructor : ClrFunction
    {
        public PrototypeConstructor(ScriptEngine engine)
            : base(engine.Function.InstancePrototype, "Prototype", new Prototype(engine.Object.InstancePrototype))
        {
        }

        [JSConstructorFunction]
        public Prototype Construct()
        {
            return new Prototype(this.InstancePrototype);
        }
    }
}
