using Jurassic;
using Jurassic.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JsMan
{
    public class JsConstructor<T> : ClrFunction where T: ObjectInstance
    {
        public JsConstructor(ScriptEngine engine)
            : base(engine.Function.InstancePrototype, typeof(T).Name,
                  (T)Activator.CreateInstance(typeof(T), new object[] { engine.Object.InstancePrototype }))
        {
        }

        [JSConstructorFunction]
        public T Construct()
        {
            return (T)Activator.CreateInstance(typeof(T), new object[] { this.InstancePrototype });
        }
    }

    public class JsConstructor : ClrFunction
    {
        private Type _type;
        public JsConstructor(ScriptEngine engine, Type type)
            : base(engine.Function.InstancePrototype, type.Name,
                  (ObjectInstance)Activator.CreateInstance(type, new object[] { engine.Object.InstancePrototype }))
        {
            _type = type;
        }

        [JSConstructorFunction]
        public ObjectInstance Construct()
        {
            return (ObjectInstance)Activator.CreateInstance(_type, new object[] { this.InstancePrototype });
        }
    }
}