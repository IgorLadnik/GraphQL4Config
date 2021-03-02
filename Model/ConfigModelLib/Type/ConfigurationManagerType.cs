using System.Collections.Generic;
using System.Xml;
using GraphQL.Types;
using GraphQlHelperLib;

namespace ConfigModelLib.Type
{
    public class ConfigurationManagerType : ObjectGraphType<Models.ConfigurationManager>
    {
        public ConfigurationManagerType()
        {
            Field(c => c.BasePath);

            Field<ListGraphType<LoaderType>>("loaders", resolve: context =>
            {
                var xmlDocument = context.GetXmlDocument();
                List<Models.Loader> lstLoader = new();
                var count = 0;
                foreach (XmlElement loaderType in xmlDocument.GetElementsByTagName("Loader")) 
                {
                    Models.Loader loader = new()
                    {
                        Name = loaderType.GetAttribute("name"),
                        Assembly = loaderType.GetAttribute("assembly"),
                        Type = loaderType.GetAttribute("type"),
                        Enabled = bool.Parse(loaderType.GetAttribute("enabled")),
                    };
                    context.SetCache($"loader{count++}", loader.Name);
                    lstLoader.Add(loader);
                }
                context.SetCache($"_loader", 0);
                return lstLoader;
            });
        }
    }
}
