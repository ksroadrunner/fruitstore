using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;

namespace FruitStore.WebApp.Extensions
{
    public static class SessionExtension
    {
        public static T Get<T>(this ISession session, T def = null) where T : class
        {
            if (session == null)
                return def;

            var json = session.GetString(typeof(T).Name);
            if (string.IsNullOrEmpty(json))
                return def;

            T result = def;
            try
            {
                result = JsonConvert.DeserializeObject<T>(json);
            }
            catch { }
            return result;
        }

        public static void Set<T>(this ISession session, T arg) where T : class
        {
            if (session == null)
                return;

            var name = typeof(T).Name;
            if(arg == null)
            {
                session.Remove(name);
                return;
            }

            var json = JsonConvert.SerializeObject(arg, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            session.SetString(name, json);
        }
    }
}