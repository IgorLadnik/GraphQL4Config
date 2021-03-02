using System;
using Microsoft.Extensions.DependencyInjection;
using ConfigModelLib.Query;
using ConfigModelLib.Mutation;

namespace GraphQlService.Schema
{
    public class RootSchema : GraphQL.Types.Schema
    {
        public RootSchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<RootQuery>();
            Mutation = serviceProvider.GetRequiredService<RootMutation>();
        }
    }
}
