using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using WebApiMimic.Helpers;
using WebApiMimic.Models;
using WebApiMimic.Repositories.Contracts;

namespace WebApiMimic.Controllers
{
    [Route("api/palavras")]
    public class PalavrasController:ControllerBase
    {
        private readonly IPalavraRepository _repository;
        public PalavrasController(IPalavraRepository repository)
        {
            _repository = repository;
        }
        [Route("")]
        [HttpGet]
        public ActionResult ObterTodas([FromQuery]PalavraUrlQuery query)
        {
            var item = _repository.ObterPalavras(query);
            if (item.Paginacao != null)
            {
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(item.Paginacao));
                if (query.pagNumero > item.Paginacao.TotalPaginas)
                {
                    return NotFound();
                }
            }
            
           
            return Ok(item.ToList());
        }


        [Route("{id}")]
        [HttpGet]
        public ActionResult Obter(int id)
        {
            var obj = _repository.Obter(id);

            if (obj == null)
                return NotFound();

            return Ok(obj);
        }
        [Route("")]
        [HttpPost]
        public ActionResult Cadastar([FromBody]Palavra palavra)
        {          
            _repository.Cadastrar(palavra);
            return Created($"/api/palavras/{palavra.Id}", palavra);
        }
        [Route("{id}")]
        [HttpPut]
        public ActionResult Atualiza(int id, [FromBody]Palavra palavra)
        {
            var obj = _repository.Obter(id);
            if (obj == null)
                return NotFound();

             palavra.Id = id;
           _repository.Atualizar(palavra);
            return NoContent();
        }
        [Route("{id}")]
        [HttpDelete]
        public ActionResult Deletar(int id)
        {
            var palavra = _repository.Obter(id);

            if (palavra == null)
                return NotFound();
                      
            _repository.Deletar(id);
            return NoContent();
        }
    }
}
