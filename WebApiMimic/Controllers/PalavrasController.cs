using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using WebApiMimic.Helpers;
using WebApiMimic.Models;
using WebApiMimic.Models.DTO;
using WebApiMimic.Repositories.Contracts;

namespace WebApiMimic.Controllers
{
    [Route("api/palavras")]
    public class PalavrasController:ControllerBase
    {
        private readonly IPalavraRepository _repository;
        private readonly IMapper _mapper;
        public PalavrasController(IPalavraRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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


        
        [HttpGet("{id}",Name = "obterPalavra")]
        public ActionResult Obter(int id)
        {
            var obj = _repository.Obter(id);
            PalavrasDTO palavrasDTO = _mapper.Map<Palavra, PalavrasDTO>(obj);
            palavrasDTO.Links = new List<LinkDTO>();
            palavrasDTO.Links.Add(new LinkDTO("self", Url.Link("obterPalavra",new { id = palavrasDTO.Id }),"GET"));
            palavrasDTO.Links.Add(new LinkDTO("update", Url.Link("atualizaPalavra", new { id = palavrasDTO.Id }), "PUT"));
            palavrasDTO.Links.Add(new LinkDTO("delete", Url.Link("deletarPalavra", new { id = palavrasDTO.Id }), "DELETE"));

            if (obj == null)
                return NotFound();

            return Ok(palavrasDTO);
        }
        [Route("")]
        [HttpPost]
        public ActionResult Cadastar([FromBody]Palavra palavra)
        {          
            _repository.Cadastrar(palavra);
            return Created($"/api/palavras/{palavra.Id}", palavra);
        }
        
        [HttpPut("{id}", Name = "atualizaPalavra")]
        public ActionResult Atualiza(int id, [FromBody]Palavra palavra)
        {
            var obj = _repository.Obter(id);
            if (obj == null)
                return NotFound();

             palavra.Id = id;
           _repository.Atualizar(palavra);
            return NoContent();
        }

        [HttpDelete("{id}", Name = "deletarPalavra")]
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
