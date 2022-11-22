using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public ActionResult ObterTodas(DateTime? data, int? pagNumero,int? pagRegistrosPag)
        {
            
            var item = _banco.Palavras.AsQueryable();
            if (data.HasValue)
            {
                item = item.Where(p => p.Criado > data.Value || p.Atualizado > data.Value && p.Ativo == true);
            }
            if (pagNumero.HasValue)
            {
                item = item.Skip((pagNumero.Value - 1)* pagRegistrosPag.Value ).Take(pagRegistrosPag.Value);
            }
            return Ok(item.Where(p => p.Ativo == true));
        }


        [Route("{id}")]
        [HttpGet]
        public ActionResult Obter(int id)
        {
            var obj = _banco.Palavras.Find(id);

            if (obj == null)
                return NotFound();

            return Ok(obj);
        }
        [Route("")]
        [HttpPost]
        public ActionResult Cadastar([FromBody]Palavra palavra)
        {
            _banco.Palavras.Add(palavra);
            _banco.SaveChanges();
            return Created($"/api/palavras/{palavra.Id}", palavra);
        }
        [Route("{id}")]
        [HttpPut]
        public ActionResult Atualiza(int id, [FromBody]Palavra palavra)
        {
            var obj = _banco.Palavras.AsNoTracking().FirstOrDefault(p => p.Id == id);

            if (obj == null)
                return NotFound();

             palavra.Id = id;
            _banco.Palavras.Update(palavra);
            _banco.SaveChanges();
            return NoContent();
        }
        [Route("{id}")]
        [HttpDelete]
        public ActionResult Deletar(int id)
        {
            var palavra = _banco.Palavras.Find(id);

            if (palavra == null)
                return NotFound();

            
            palavra.Ativo = false;
            _banco.Palavras.Update(palavra);
            _banco.SaveChanges();
            return NoContent();
        }
    }
}
