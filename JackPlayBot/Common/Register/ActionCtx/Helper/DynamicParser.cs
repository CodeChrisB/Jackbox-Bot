using JackPlayBot.Common.Register.ActionCtx.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace JackPlayBot.Common.Register.ActionCtx.Helper
{
    internal class DynamicParser
    {

            public static T Convert<T>(dynamic source)
            {
                T result = Activator.CreateInstance<T>();

                PropertyInfo[] properties = typeof(T).GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    string propertyName = property.Name;
                    dynamic propertyValue = source.GetType().GetProperty(propertyName).GetValue(source, null);

                    if (propertyValue != null)
                    {
                        property.SetValue(result, propertyValue.ToString());
                    }
                }

                return result;
            }
    }
}
