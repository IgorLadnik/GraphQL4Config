using System.Collections.Generic;
using System.Xml;
using GraphQL.Types;
using GraphQlHelperLib;

namespace ConfigModelLib.Type.Input
{
    public class ConfigurationManagerInputType : InputObjectGraphType
    {
        public ConfigurationManagerInputType()
        {
            Field<StringGraphType>("basePath");
            Field<ListGraphType<LoaderInputType>>("loaders");
        }

        public static XmlElement ToXml(Dictionary<string, object> dct, XmlDocument xmlDocument = null)
        {
            if (xmlDocument == null)
                xmlDocument = new();
            
            var xmlElementCM = xmlDocument.CreateElement("ConfigurationManager");
            xmlElementCM.SetAttribute("basePath", dct["basePath"] as string);
            var xmlElementLoaders = xmlDocument.CreateElement("Loaders");

            xmlDocument.AddChildren(xmlElementLoaders, dct["loaders"] as List<object>, LoaderInputType.ToXml);
            xmlElementCM.AppendChild(xmlElementLoaders);

            return xmlElementCM;
        }
    }
}
