using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Xml;
using GraphQL.Execution;

namespace GraphQlHelperLib
{
    public static class ProvideUserContextEx 
    {
        private static IDictionary<string, object> GetCacheDictionary(this IProvideUserContext context) => context.UserContext;
        private static object _locker = new();

        public static bool DoesCacheExist(this IProvideUserContext context, string key) =>
            GetCacheDictionary(context).ContainsKey(key);


        #region Cache

        public static T GetCache<T>(this IProvideUserContext context, string key)
        {
            lock (_locker)
            {
                if (!GetCacheDictionary(context).TryGetValue(key, out object cacheObj))
                    return default;

                return (T)cacheObj;
            }
        }

        public static void SetCache(this IProvideUserContext context, string key, object value)
        {
            lock (_locker)
            {
                if (value == null)
                    return;

                GetCacheDictionary(context)[key] = value;
            }
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

        public static XmlDocument GetXmlDocument(this IProvideUserContext context)
        {
            lock (_locker)
            {
                return GetCacheDictionary(context).TryGetValue("XmlDocument", out object xmlDocument)
                        ? (XmlDocument)xmlDocument
                        : null;
            }
        }

        public static void SetXmlDocument(this IProvideUserContext context, XmlDocument xmlDocument)
        {
            lock (_locker)
            {
                if (xmlDocument == null)
                    return;

                GetCacheDictionary(context)["XmlDocument"] = xmlDocument;
            }
        }

        #endregion // Xml

        #region User for authentication

        public static ClaimsPrincipal GetUser(this IProvideUserContext context)
        {
            lock (_locker)
            {
                return GetCacheDictionary(context).TryGetValue("_User", out object user)
                        ? (ClaimsPrincipal)user
                        : null;
            }
        }

        public static void SetUser(this IProvideUserContext context, ClaimsPrincipal user)
        {
            lock (_locker)
            {
                if (user == null)
                    return;

                GetCacheDictionary(context)["_User"] = user;
            }
        }

        #endregion // User for authentication

        public static bool GetIsAuthJwt(this IProvideUserContext context)
        {
            return GetCacheDictionary(context).TryGetValue("_IsAuthJwt", out object isAuthJwt)
                        ? (bool)isAuthJwt
                        : false;
        }

        public static void SetIsAuthJwt(this IProvideUserContext context, bool isAuthJwt)
        {
            GetCacheDictionary(context)["_IsAuthJwt"] = isAuthJwt;
        }

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
