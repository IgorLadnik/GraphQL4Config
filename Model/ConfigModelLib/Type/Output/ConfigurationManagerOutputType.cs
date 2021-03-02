using GraphQL.Types;
using ConfigModelLib.Mutation;

namespace ConfigModelLib.Type.Output
{
    public class ConfigurationManagerOutputType : ObjectGraphType<MutationsResponse>
    {
        public ConfigurationManagerOutputType()
        {
            Field(p => p.Status);
            Field(p => p.Message);
        }
    }
}
