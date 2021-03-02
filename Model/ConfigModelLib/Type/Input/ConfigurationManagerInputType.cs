using System.Collections.Generic;
using System.Xml;
using GraphQL.Types;

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

            var basePath = dct["basePath"];
            var loaders = dct["loaders"];
            foreach (Dictionary<string, object> loader in dct["loaders"] as List<object>) 
                xmlElementLoaders.AppendChild(LoaderInputType.ToXml(loader, xmlDocument));

            xmlElementCM.AppendChild(xmlElementLoaders);

            return xmlElementCM;
        }
    }
}
