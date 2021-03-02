using GraphQL.Types;

namespace ConfigModelLib.Query
{
    public class RootQuery : ObjectGraphType
    {
        public RootQuery()
        {
            Field<ConfigurationManagerQuery>("configurationManagerQuery", resolve: context => new { });
        }
    }
}
