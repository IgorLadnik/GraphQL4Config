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

        public static XmlElement ToXml(Dictionary<string, object> inp, XmlDocument xmlDocument = null)
        {
            if (xmlDocument == null)
                xmlDocument = new();

            var xmlElementLoader = xmlDocument.CreateElement("Loader");
            xmlElementLoader.SetAttr(inp, "name");
            xmlElementLoader.SetAttr(inp, "assembly");
            xmlElementLoader.SetAttr(inp, "type");
            xmlElementLoader.SetAttr(inp, "enabled", true);

            xmlDocument.AddChildren(xmlElementLoader, inp, "sources", SourceInputType.ToXml);

            return xmlElementLoader;
        }
    }
}
