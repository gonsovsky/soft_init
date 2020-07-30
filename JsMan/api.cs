using Jurassic.Library;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JsMan
{
    public abstract class JsApi : ObjectInstance
    {
        public JsApi(ObjectInstance prototype) : base(prototype)
        {
            TypesToInjectJS
                .ToList()
                .ForEach(p =>
                    Engine.SetGlobalValue(p.Name, new JsConstructor(Engine, p)));
            this.PopulateFunctions();
        }

        protected IEnumerable<Type> TypesToInjectJS
        {
            get
            {
                IEnumerable<Type> traverse(Type t)
                {
                    if (t.IsSubclassOf(typeof(ObjectInstance)))
                        yield return t;

                    foreach (var x in t.GetNestedTypes())
                        foreach (var a in traverse(x))
                            yield return a;
                }

                foreach (var m in GetType().GetMethods())
                {
                    foreach (var x in traverse(m.ReturnType))
                        yield return x;

                    foreach (var a in m.CustomAttributes)
                        if (a.AttributeType == typeof(JSFunctionAttribute) || a.AttributeType.IsSubclassOf(typeof(JSFunctionAttribute)))
                            foreach (ParameterInfo p in m.GetParameters())
                                foreach (var x in traverse(p.ParameterType))
                                    yield return x;
                }
            }
        }
    }
}
