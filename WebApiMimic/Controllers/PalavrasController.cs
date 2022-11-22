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
            return Ok( _banco.Palavras);
        }
        [Route("{id}")]
        [HttpGet]
        public ActionResult Obter(int id)
        {
            return Ok(_banco.Palavras.Find(id));
        }
        [HttpPost]
        public ActionResult Cadastar(Palavra palavra)
        {
            _banco.Palavras.Add(palavra);
            return Ok();
        }
        [HttpPut]
        public ActionResult Atualiza(int id, Palavra palavra)
        {
            _banco.Palavras.Update(palavra);
            return Ok();
        }
        [HttpDelete]
        public ActionResult Deletar(int id)
        {
            _banco.Palavras.Remove(_banco.Palavras.Find(id));
            return Ok();
        }
    }
}
