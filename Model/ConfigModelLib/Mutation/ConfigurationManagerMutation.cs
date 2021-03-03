using System.Collections.Generic;
using GraphQL;
using GraphQL.Types;
using ConfigModelLib.Type.Input;
using ConfigModelLib.Type.Output;
using System.Xml;
using System.IO;

namespace ConfigModelLib.Mutation
{
    public class ConfigurationManagerMutation : ObjectGraphType
    {
        public ConfigurationManagerMutation()
        {
            Field<ConfigurationManagerOutputType>("createConfigurationManager",
                arguments: new QueryArguments(new QueryArgument<ConfigurationManagerInputType> { Name = "configurationManagerInput" }),
                resolve: context =>
                {
                    var inp = context.GetArgument<Dictionary<string, object>>("configurationManagerInput");
                    var basePath = $"{inp["basePath"]}".Replace("/", @"\");
                    var file  = $@"{basePath}\ConfigurationManager.xml";
                    XmlDocument xmlDocument = new();
                    var isLoaded = false;
                    if (isLoaded = File.Exists(file))
                        xmlDocument.Load(file);

                    var firstChild = ConfigurationManagerInputType.ToXml(inp, xmlDocument);
                    if (isLoaded) 
                        xmlDocument.RemoveChild(xmlDocument.FirstChild);

                    xmlDocument.AppendChild(firstChild);

                    xmlDocument.Save(file);

                    return new MutationsResponse
                    {
                        OpStatus = OperationStatus.Success,
                        Message = string.Empty
                    };
                });
        }
    }
}
