using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Lucene.Net.Documents;

namespace Rss.Indexer
{
    public static class IndexerExtensions
    {
        private static readonly Dictionary<Type, PropertyInfo[]> PropertyCache = new Dictionary<Type, PropertyInfo[]>(8);

        public static void ForEach<T>(this IEnumerable<T> set, Action<T> action)
        {
            foreach (var item in set)
            {
                action(item);
            }
        }

        public static Lucene.Net.Documents.Document ToLuceneDocument<T>(this T document)
        {
            var luceneDocument = new Lucene.Net.Documents.Document();
            var properties = GetProperties(typeof(T));

            foreach (var propertyInfo in properties)
            {
                var field = propertyInfo.GetCustomAttribute<LuceneFieldAttribute>();

                if (field == null) continue;

                var value = GetStringValue(document, propertyInfo);

                luceneDocument.Add(new Field(field.Name, value,
                    field.Store, field.Index, field.TermVector));
            }

            return luceneDocument;
        }

        private static string GetStringValue<T>(T document, PropertyInfo propertyInfo)
        {
            return propertyInfo.PropertyType == typeof(Guid)
                  ? new Guid(propertyInfo.GetValue(document).ToString()).ToString("D")
                  : (string)propertyInfo.GetValue(document);
        }

        public static T ToResult<T>(this Lucene.Net.Documents.Document document) where T : new()
        {
            var result = new T();
            var properties = GetProperties(typeof(T));

            foreach (var propertyInfo in properties)
            {
                var field = propertyInfo.GetCustomAttribute<LuceneFieldAttribute>();

                if (field == null) continue;

                object value;

                if (TryConvertToType(document.GetField(field.Name).StringValue, propertyInfo.PropertyType, out value))
                {
                    propertyInfo.SetValue(result, value);
                }
            }

            return result;
        }

        private static IEnumerable<PropertyInfo> GetProperties(Type type)
        {
            if (!PropertyCache.ContainsKey(type))
            {
                PropertyCache[type] = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            }

            return PropertyCache[type];
        }

        private static bool TryConvertToType(string value, Type type, out object result)
        {
            result = value;

            var typeName = type.Name;

            if (type.IsGenericType) typeName = type.GetGenericArguments()[0].Name;

            try
            {
                switch (typeName)
                {
                    case "String":
                        return true;

                    case "Int32":
                        result = Convert.ToInt32(value);
                        return true;

                    case "Boolean":
                        result = !string.IsNullOrEmpty(value);
                        return true;

                    case "DateTime":
                        var tempDate = new DateTime(1900, 1, 1);

                        if (
                            !DateTime.TryParse(value, CultureInfo.CurrentCulture, DateTimeStyles.None,
                                out tempDate)) return false;

                        result = tempDate;

                        return true;

                    case "Double":
                        result = Convert.ToDouble(value);
                        return true;

                    case "Decimal":
                        result = Convert.ToDecimal(value);
                        return true;

                    case "Guid":
                        Guid tempGuid;

                        if (!Guid.TryParse(value, out tempGuid)) return false;

                        result = tempGuid;

                        return true;

                    default:
                        // let .NET have a crack
                        result = Convert.ChangeType(value, type);
                        return true;
                }
            }
            catch
            {
                result = "TryConvertToType failed for " + type.Name;
            }

            return false;
        }
    }
}