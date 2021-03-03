using System.Collections.Generic;
using GraphQL;
using GraphQL.Types;
using ConfigModelLib.Type.Input;
using ConfigModelLib.Type.Output;
using System.Xml;

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
                    XmlDocument xmlDocument = new();
                    var inp = context.GetArgument<Dictionary<string, object>>("configurationManagerInput");
                    xmlDocument.AppendChild(ConfigurationManagerInputType.ToXml(inp, xmlDocument));

                    var basePath = $"{inp["basePath"]}".Replace("/", @"\");
                    xmlDocument.Save($@"{basePath}\ConfigurationManager.xml");

                    return new MutationsResponse
                    {
                        OpStatus = OperationStatus.Success,
                        Message = string.Empty
                    };
                });
        }
    }
}
