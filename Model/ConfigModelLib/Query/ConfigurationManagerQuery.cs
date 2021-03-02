using System.Collections.Generic;
using System.IO;
using System.Xml;
using GraphQL.Types;
using GraphQlHelperLib;
using ConfigModelLib.Type;

namespace ConfigModelLib.Query
{
    public class ConfigurationManagerQuery : ObjectGraphType
    {
        public ConfigurationManagerQuery() =>
            Field<ListGraphType<ConfigurationManagerType>>("configurationManager", resolve: context =>
            {
                var currDir = Directory.GetCurrentDirectory();
                XmlDocument xmlDocument = new();
                xmlDocument.Load("ConfigurationManager.xml");
                
                context.SetXmlDocument(xmlDocument);

                List<Models.ConfigurationManager> lstCm = new()
                {
                    new() { BasePath = "xxx" }
                };

                return lstCm; 
            });
    }
}
