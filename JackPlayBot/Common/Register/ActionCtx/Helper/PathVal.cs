using Newtonsoft.Json.Linq;
using System;
using System.Dynamic;
using System.Reflection;

public static class PathVal
{
    public static T Get<T>(dynamic obj, params string[] keys)
    {
        if (obj == null) return default(T);
        dynamic currentObject = obj;

        foreach (string key in keys)
        {
            try
            {
                if (currentObject is System.Collections.IEnumerable && int.TryParse(key, out int index))
                {
                    // If currentObject is an array-like structure, convert the key to int
                    currentObject = currentObject[index];
                }
                else
                {
                    currentObject = currentObject[key];
                }

                if (currentObject == null) return default(T);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        return (T)currentObject;
    }
    public static List<string> GetDynamicKeys(dynamic obj)
    {
        List<string> keys = new List<string>();

        if (obj is ExpandoObject expando)
        {
            foreach (var property in expando)
            {
                keys.Add(property.Key);
            }
        }
        else
        {
            Type type = obj.GetType();

            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                keys.Add(property.Name);
            }
        }

        return keys;
    }

}
