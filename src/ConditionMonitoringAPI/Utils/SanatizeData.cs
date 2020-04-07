using System;
using System.Text.RegularExpressions;

namespace ConditionMonitoringAPI.Utils
{
    public static class SanatizeData
    {
        public static TEntity SanitizeStrings<TEntity>(TEntity entity)
        {
            _ = entity ?? throw new ArgumentNullException();

            var t = entity.GetType();
            var properties = t.GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                var p = t.GetProperty(properties[i].Name);

                if (p.PropertyType == typeof(string))
                {
                    var value = p.GetValue(entity).ToString().Trim();
                    if (Regex.IsMatch(value, @"^[a-zA-Z0-9]+$"))
                        p.SetValue(entity, value);
                    else
                        throw new Exception($"Property '{p.Name}' can must only contain letters and numbers. '{value}' is not a valid value.");
                }
            }
            return entity;
        }
    }
}
