﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using GraphQL;
using GraphQL.Types;

namespace GraphQlHelperLib
{
    public class GqlControllerBase : ControllerBase
    {
        protected GraphqlProcessor Gql { get; private set;  }

        public GqlControllerBase(ISchema schema, IDocumentExecuter documentExecuter, IConfiguration configuration)
        {
            Gql = new(schema, documentExecuter, configuration);
        }

        protected async Task<IActionResult> ProcessQuery(GraphqlQuery query)
        {
            var result = await Gql.Process(query);
            return TransformResult(result) ?? Ok(result.Data);
        }

        protected async Task<IActionResult> ProcessQuery(string query)
        {
            var result = await Gql.Process(query);
            return TransformResult(result) ?? Ok(result.Data);
        }

        protected IActionResult TransformResult(ExecutionResult er)
        {
            if (er.Errors?.Count > 0)
            {
                var res = BadRequest(er);
                var firstErr = er.Errors[0];
                switch (firstErr.Code)
                {
                    case "UNAUTHORIZED_ACCESS":
                        res.StatusCode = 401;
                        res.Value = firstErr.InnerException.Message;
                        break;
                }

                return res;
            }

            return null;
        }
    }
}

