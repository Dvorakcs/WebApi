using AutoMapper;
using WebApiMimic.V1.Models;
using WebApiMimic.V1.Models.DTO;

namespace WebApiMimic.Helpers
{
    public class DTOMapperProfile: Profile
    {
        public DTOMapperProfile()
        {
            CreateMap<Palavra, PalavrasDTO>();
        }
    }
}
/*
             * para que serve o automapper
             * Copiar objetos e transformar em outros
             * palavras = palavrasDTO
             * Simplifica na hora de transformar um obj em outro
             * sem o AutoMapper  {
             *  palavrasDTO.Id = Palavras.id
             *  palavrasDTO.Nome = Palavras.Nome
             *  palavrasDTO.Ativo = Palavras.Ativo
             *  
             * }
             */