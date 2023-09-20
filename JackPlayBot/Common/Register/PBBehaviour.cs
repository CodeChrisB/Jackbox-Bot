using JackPlayBot.Common.Data;
using JackPlayBot.Common.Data.Annotation;
using JackPlayBot.Common.Register.ActionCtx.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JackPlayBot.Common.Register
{
    public class PBBehaviour
    {

        public static void Register<T>(Games game)
        {
            var type = typeof(T);
            var methods = type.GetMethods();

            foreach (var method in methods)
            {
                var attributes = method.GetCustomAttributes(true);

                foreach (Attribute attr in attributes)
                {
                    if (attr.TypeId == typeof(BotAction))
                    {
                        var action = (BotAction)attr;
                        ActionContext context = new ActionContext(); // Instantiate or get an ActionContext
                        Action<ActionContext> actionDelegate = (ActionContext context) =>
                        {
                            method.Invoke(null, new object[] { context });
                        };
                        ContextDistributer.AddBehaviour(game, action.State, actionDelegate);
                    }
                }


            }
        }

        public static Func<ActionContext> CreateFuncFromMethodInfo<T>(MethodInfo methodInfo)
        {
            return (Func<ActionContext>)Delegate.CreateDelegate(typeof(Func<ActionContext>), methodInfo);
        }
    }
}
