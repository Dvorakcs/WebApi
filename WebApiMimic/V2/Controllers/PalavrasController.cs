using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiMimic.V2
{
    // /api/v2.0/palavras
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Route("api/[controller]")]
    [Produces("application/json")]
    [ApiVersion("2.0")]
    public class PalavrasController : ControllerBase
    {
        [HttpGet("", Name = "obterTodas")]
        public string ObterTodas()
        {
            return "versao 2.0";
           
        }
    }
}
