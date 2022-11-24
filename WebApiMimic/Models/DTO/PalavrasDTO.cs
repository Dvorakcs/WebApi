using System;

namespace WebApiMimic.Models.DTO
{
    public class PalavrasDTO:BaseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Pontuacao { get; set; }
        public bool Ativo { get; set; }
        public DateTime Criado { get; set; }
        public DateTime? Atualizado { get; set; }
    }
}
