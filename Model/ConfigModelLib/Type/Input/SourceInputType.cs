using System.Collections.Generic;
using System.Xml;
using GraphQL.Types;
using GraphQlHelperLib;

namespace ConfigModelLib.Type.Input
{
    public class SourceInputType : InputObjectGraphType
    {
        public SourceInputType()
        {
            Field<StringGraphType>("value");
        }

        public static XmlElement ToXml(Dictionary<string, object> dct, XmlDocument xmlDocument = null)
        {
            if (xmlDocument == null)
                xmlDocument = new();

            var xmlElement = xmlDocument.GetCreateElementy("Source", 0);
            xmlElement.SetAttribute("value", $"{dct["value"]}");

            return xmlElement;
        }
    }
}
