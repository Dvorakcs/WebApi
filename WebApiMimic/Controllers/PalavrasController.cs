using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiMimic.Database;
using WebApiMimic.Helpers;
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
        public ActionResult ObterTodas([FromQuery]PalavraUrlQuery P)
        {
            
            var item = _banco.Palavras.AsQueryable();
            if (P.data.HasValue)
            {
                item = item.Where(p => p.Criado > P.data.Value || p.Atualizado > P.data.Value && p.Ativo == true);
            }
            else
            {
               item.Where(p => p.Ativo == true);
            }
            int quantidadeTotalRegistro = item.Count();
            if (P.pagNumero.HasValue)
            {
               
                item = item.Skip((P.pagNumero.Value - 1)* P.pagRegistros.Value ).Take(P.pagRegistros.Value);
                var paginacao = new Paginacao();
                paginacao.NumeroPagina = P.pagNumero.Value;
                paginacao.RegistroPorPagina = P.pagRegistros.Value;
                paginacao.TotalRegistros = quantidadeTotalRegistro;
                paginacao.TotalPaginas = (int)Math.Ceiling((double)quantidadeTotalRegistro / P.pagRegistros.Value);
                Response.Headers.Add("X-Pagination",JsonConvert.SerializeObject(paginacao));

                if (P.pagNumero > paginacao.TotalPaginas)
                {
                    return NotFound();
                }
            }
            
            return Ok(item);
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
