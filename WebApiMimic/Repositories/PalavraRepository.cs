using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApiMimic.Database;
using WebApiMimic.Helpers;
using WebApiMimic.Models;
using WebApiMimic.Repositories.Contracts;

namespace WebApiMimic.Repositories
{
    public class PalavraRepository : IPalavraRepository
    {
        private readonly MimicContext _banco;
        public PalavraRepository(MimicContext banco)
        {
            _banco = banco;
        }
        public PaginationList<Palavra> ObterPalavras(PalavraUrlQuery query)
        {
            var Lista = new PaginationList<Palavra>();
            var item = _banco.Palavras.AsNoTracking().AsQueryable();
            if (query.data.HasValue)
            {
                item = item.Where(p => p.Criado > query.data.Value || p.Atualizado > query.data.Value && p.Ativo == true);
            }
            else
            {
                item.Where(p => p.Ativo == true);
            }
            int quantidadeTotalRegistro = item.Count();
            if (query.pagNumero.HasValue)
            {

                item = item.Skip((query.pagNumero.Value - 1) * query.pagRegistros.Value).Take(query.pagRegistros.Value);
                
                var paginacao = new Paginacao();
                paginacao.NumeroPagina = query.pagNumero.Value;
                paginacao.RegistroPorPagina = query.pagRegistros.Value;
                paginacao.TotalRegistros = quantidadeTotalRegistro;
                paginacao.TotalPaginas = (int)Math.Ceiling((double)quantidadeTotalRegistro / query.pagRegistros.Value);
                Lista.Paginacao = paginacao;
            }
            Lista.AddRange(item);

            return Lista;
        }

        public Palavra Obter(int id)
        {
            var obj = _banco.Palavras.AsNoTracking().FirstOrDefault(p => p.Id == id);
            return obj;
        }

        public void Cadastrar(Palavra palavra)
        {
            _banco.Palavras.Add(palavra);
            _banco.SaveChanges();
        }
        public void Atualizar(Palavra palavra)
        {
            _banco.Palavras.Update(palavra);
            _banco.SaveChanges();
        }
        public void Deletar(int id)
        {           
             var palavra = this.Obter(id);
             palavra.Ativo = false;
            _banco.Palavras.Update(palavra);
            _banco.SaveChanges();
        }
       
    }
}
