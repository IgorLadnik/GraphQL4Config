using System.Collections.Generic;
using System.Xml;
using System.Linq;
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

        public static XmlElement ToXml(Dictionary<string, object> inp, XmlDocument xmlDocument = null)
        {
            if (xmlDocument == null)
                xmlDocument = new();

            var xmlElementCM = xmlDocument.GetCreateElementy("ConfigurationManager", 0);
            xmlElementCM.SetAttr(inp, "basePath");
            var xmlElementLoaders = xmlDocument.GetCreateElementy("Loaders", 0);

            xmlDocument.AddChildren(xmlElementLoaders, inp, "loaders", LoaderInputType.ToXml,
                (xmlDocument, xmlElement) => 
                {
                    foreach (XmlElement el in xmlDocument.GetElementsByTagName("Loader")) 
                        if (el.GetAttribute("name") == xmlElement.GetAttribute("name"))
                            return el;

                    return null;
                });
            xmlElementCM.AppendChild(xmlElementLoaders);

            return xmlElementCM;
        }
    }
}
