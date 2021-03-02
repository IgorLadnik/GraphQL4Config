using GraphQL.Types;

namespace ConfigModelLib.Mutation
{
    public class RootMutation : ObjectGraphType
    {
        public RootMutation()
        {
            Field<ConfigurationManagerMutation>("configurationManagerMutation", resolve: contect => new { });
        }
    }
}
