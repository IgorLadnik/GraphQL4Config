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

        public static XmlElement GetCreateElementy(this XmlDocument xmlDocument, string name, int index)
        {
            var elements = xmlDocument.GetElementsByTagName(name);
            if (elements.Count > 0)
                return elements[index] as XmlElement;

            return xmlDocument.CreateElement(name);
        }

        public static void AddChildren(this XmlDocument xmlDocument,
                                       XmlElement parent,
                                       Dictionary<string, object> inp,
                                       string key,
                                       Func<Dictionary<string, object>, XmlDocument, XmlElement> toXml,
                                       Func<XmlDocument, XmlElement, XmlElement> filter = null)
        {
            if (!inp.TryGetValue(key, out object value))
                return;

            foreach (Dictionary<string, object> item in value as List<object>)
            {
                var newXmlElement = toXml(item, xmlDocument);
                var oldXmlElement = filter?.Invoke(xmlDocument, newXmlElement);
                if (oldXmlElement == null)
                    parent.AppendChild(newXmlElement);
                else
                {
                    foreach (XmlAttribute newAttr in newXmlElement.Attributes)
                        oldXmlElement.SetAttribute(newAttr.Name, newAttr.Value);
                }
            }
        }

        public static void SetAttr(this XmlElement xmlElement, Dictionary<string, object> inp, string attrName, bool isToLower = false) 
        {
            if (!inp.TryGetValue(attrName, out object value))
                return;
            
            var strValue = value?.ToString();
            if (isToLower)
                strValue = strValue.ToLower();

            xmlElement.SetAttribute(attrName, strValue);          
        }

        //public static void SetAttr(this XmlElement xmlElement, Dictionary<string, object> inp, string attrName, bool isToLower = false)
        //{
        //    if (inp.TryGetValue(attrName, out object value))
        //    {
        //        var strValue = value?.ToString();
        //        if (isToLower)
        //            strValue = strValue.ToLower();

        //        xmlElement.SetAttribute(attrName, strValue);
        //    }
        //}
    }
}
