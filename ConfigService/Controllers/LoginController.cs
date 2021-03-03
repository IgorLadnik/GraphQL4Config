using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using GraphQlService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        public LoginController(IConfiguration configuration)
        {
            
        }

        public IActionResult Index()
        {
            
            return Ok("qq");
        }
    }
}
