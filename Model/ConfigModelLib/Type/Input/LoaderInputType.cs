using System.Collections.Generic;
using System.Xml;
using GraphQL.Types;

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

            var xmlElement = xmlDocument.CreateElement("Loader");
            xmlElement.SetAttribute("name", $"{dct["name"]}");
            xmlElement.SetAttribute("assembly", $"{dct["assembly"]}");
            xmlElement.SetAttribute("type", $"{dct["type"]}");
            xmlElement.SetAttribute("enabled", $"{dct["enabled"]}".ToLower());
            foreach (Dictionary<string, object> source in dct["sources"] as List<object>)
                xmlElement.AppendChild(SourceInputType.ToXml(source, xmlDocument));

            return xmlElement;
        }
    }
}
