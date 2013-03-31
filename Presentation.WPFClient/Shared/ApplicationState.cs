using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsGateway.Presentation.WPFClient.Shared
{
    public static class ApplicationState
    {
        private static readonly Dictionary<string, object> Values =
                   new Dictionary<string, object>();
        public static void SetValue(string key, object value)
        {
            if (Values.ContainsKey(key))
            {
                Values.Remove(key);
            }
            Values.Add(key, value);
        }
        public static T GetValue<T>(string key)
        {
            if (Values.ContainsKey(key))
            {
                return (T)Values[key];
            }
            else
            {
                return default(T);
            }
        }
    }
}
