using System.Reflection;

namespace MarketAnalysisWebApi.Extensions
{
    public static class ConfigurationExtensions
    {
        public static T? GetSectionAs<T>(this IConfiguration configuration, string key)
        {
            var targetSection = configuration.GetSection(key);

            var objType = typeof(T);
            var obj = Activator.CreateInstance<T>();

            var props = objType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
            {
                var propSection = targetSection.GetSection(prop.Name);
                if (propSection.Value == null) continue;

                if (int.TryParse(propSection.Value, out var intVal))
                {
                    SetValue(obj, prop.Name, intVal);
                    continue;
                }

                if (bool.TryParse(propSection.Value, out var boolVal))
                {
                    SetValue(obj, prop.Name, boolVal);
                    continue;
                }

                SetValue(obj, prop.Name, propSection.Value);
            }

            return obj;
        }
        private static void SetValue<T>(T obj, string propName, object value)
        {
            typeof(T).InvokeMember(propName,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty,
                Type.DefaultBinder, obj, [value]);
        }
    }
}
