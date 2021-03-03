using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using GraphQlHelperLib;
using GraphQL.Types;
using GraphQL;

namespace GraphQlService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ConfigController : GqlControllerBase
    {
        private GraphqlProcessor _gql;

        public ConfigController(ISchema schema, IDocumentExecuter documentExecuter, IConfiguration configuration)
            : base(schema, documentExecuter, configuration)
        {
        }

        [HttpGet("cm/{basePath}")]
        public async Task<IActionResult> Get(string basePath)
        {
            basePath = basePath.Replace('-', '/');
            var query =
                @"query Query {
                    configurationManagerQuery {
                      configurationManager(basePath: /*basePath*/) {
                          basePath
                          loaders {
                            name
                            type
                            sources {
                               value
                            }
                          }
                        }
                      }
                    }
                  ";

            query = query
                .Replace("/*basePath*/", $"\"{basePath}\"")
                .Replace("\r", string.Empty)
                .Replace("\n", string.Empty);

            return await ProcessQuery(query);
        }
    }
}
