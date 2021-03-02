using System.Xml;
using GraphQL;
using GraphQL.Types;
using GraphQlHelperLib;
using ConfigModelLib.Type;
using ConfigModelLib.Models;

namespace ConfigModelLib.Query
{
    public class ConfigurationManagerQuery : ObjectGraphType
    {
        public ConfigurationManagerQuery() =>
            Field<ConfigurationManagerType>("configurationManager",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "path" }),
                resolve: context =>
                {
                    const string defaultFileName = "ConfigurationManager.xml";
                    var path = context.GetArgument<string>("path");
                    if (string.IsNullOrEmpty(path))
                        return "Empty path";

                    XmlDocument xmlDocument = new();
                    var file = !string.IsNullOrEmpty(path) && path.Substring(path.Length - 4)?.ToLower() == ".xml"
                        ? path
                        : $@"{path}\{defaultFileName}";

                    xmlDocument.Load(file);

                    context.SetXmlDocument(xmlDocument);

                    return new ConfigurationManager { BasePath = (xmlDocument?.FirstChild as XmlElement).GetAttribute("basePath") };
                });
    }
}
