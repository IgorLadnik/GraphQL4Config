using System.Collections.Generic;
using System.Xml;
using GraphQL.Types;
using GraphQlHelperLib;

namespace ConfigModelLib.Type.Input
{
    public class LoaderInputType : InputObjectGraphType
    {
        public LoaderInputType()
        {
            Field<StringGraphType>("name");
            Field<StringGraphType>("assembly");
            Field<StringGraphType>("type");
            Field<BooleanGraphType>("enabled");
            Field<ListGraphType<SourceInputType>>("Sources");
        }

        public static XmlElement ToXml(Dictionary<string, object> dct, XmlDocument xmlDocument = null)
        {
            if (xmlDocument == null)
                xmlDocument = new();

            var xmlElementLoader = xmlDocument.CreateElement("Loader");
            xmlElementLoader.SetAttribute("name", $"{dct["name"]}");
            xmlElementLoader.SetAttribute("assembly", $"{dct["assembly"]}");
            xmlElementLoader.SetAttribute("type", $"{dct["type"]}");
            xmlElementLoader.SetAttribute("enabled", $"{dct["enabled"]}".ToLower());

            xmlDocument.AddChildren(xmlElementLoader, dct["sources"] as List<object>, (item, doc) => SourceInputType.ToXml(item, doc));

            return xmlElementLoader;
        }
    }
}
