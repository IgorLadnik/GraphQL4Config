using GraphQL.Types;
using ConfigModelLib.Models;

namespace ConfigModelLib.Type
{
    public class SourceType : ObjectGraphType<Source>
    {
        public SourceType()
        {
            Field(s => s.Value);
        }
    }
}
