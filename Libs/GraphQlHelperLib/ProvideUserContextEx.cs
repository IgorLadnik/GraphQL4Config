using System;
using System.Collections.Generic;
using System.Xml;
using GraphQL.Execution;

namespace GraphQlHelperLib
{
    public static class ProvideUserContextEx 
    {
        private static IDictionary<string, object> GetCacheDictionary(this IProvideUserContext context) => context.UserContext;

        #region Cache

        public static T GetCache<T>(this IProvideUserContext context, string key)
        {
            if (!GetCacheDictionary(context).TryGetValue(key, out object cacheObj))
                return default;

            return (T)cacheObj;
        }

        public static void SetCache(this IProvideUserContext context, string key, object value)
        {
            if (value == null)
                return;

            GetCacheDictionary(context)[key] = value;
        }

        public static string GetFilterPattern(this IProvideUserContext context, string prefix)
        {
            var count = context.GetCache<int>($"_{prefix}");
            var pattern = context.GetCache<string>($"{prefix}{count}");
            if (pattern == null)
            {
                count = 0;
                pattern = context.GetCache<string>($"{prefix}{count++}");
            }

            context.SetCache($"_{prefix}", count + 1);
            return pattern;
        }

        #endregion // Cache

        #region Xml 

        public static XmlDocument GetXmlDocument(this IProvideUserContext context) =>
            GetCacheDictionary(context).TryGetValue("XmlDocument", out object xmlDocument)
                ? (XmlDocument)xmlDocument
                : null;

        public static void SetXmlDocument(this IProvideUserContext context, XmlDocument xmlDocument)
        {
            if (xmlDocument == null)
                return;

            GetCacheDictionary(context)["XmlDocument"] = xmlDocument;
        }

        #endregion // Xml

        public static void AddChildren(this XmlDocument xmlDocument,
                                       XmlElement parent,
                                       List<object> lst, 
                                       Func<Dictionary<string, object>, XmlDocument, XmlElement> toXml) 
        {
            foreach (Dictionary<string, object> item in lst)
                parent.AppendChild(toXml(item, xmlDocument));
        }
    }
}
