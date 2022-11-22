using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiMimic.Database;
using WebApiMimic.Models;

namespace WebApiMimic.Controllers
{
    [Route("api/palavras")]
    public class PalavrasController:ControllerBase
    {
        private readonly MimicContext _banco;
        public PalavrasController(MimicContext banco)
        {
            _banco = banco;
        }
        [Route("")]
        [HttpGet]
        public ActionResult ObterTodas()
        {
            return Ok( _banco.Palavras.Where(x => x.Ativo == true));
        }
        [Route("{id}")]
        [HttpGet]
        public ActionResult Obter(int id)
        {
            return Ok(_banco.Palavras.Find(id));
        }
        [Route("")]
        [HttpPost]
        public ActionResult Cadastar([FromBody]Palavra palavra)
        {
            _banco.Palavras.Add(palavra);
            _banco.SaveChanges();
            return Ok();
        }
        [Route("{id}")]
        [HttpPut]
        public ActionResult Atualiza(int id, [FromBody]Palavra palavra)
        {
            palavra.Id = id;
            _banco.Palavras.Update(palavra);
            _banco.SaveChanges();
            return Ok();
        }
        [Route("{id}")]
        [HttpDelete]
        public ActionResult Deletar(int id)
        {
            var palavra = _banco.Palavras.Find(id);
            palavra.Ativo = false;
            _banco.Palavras.Update(palavra);
            _banco.SaveChanges();
            return Ok();
        }
    }
}
