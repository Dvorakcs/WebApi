﻿using System;

namespace WebApiMimic.V1.Models
{
    public class Palavra
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Pontuacao { get; set; }
        public bool Ativo { get; set; }
        public DateTime Criado { get; set; }
        public DateTime? Atualizado { get; set; }
    }
}
