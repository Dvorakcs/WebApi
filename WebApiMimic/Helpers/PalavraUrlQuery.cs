﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiMimic.Helpers
{
    public class PalavraUrlQuery
    {
        public DateTime? data { get; set; }
        public int? pagNumero { get; set; }
        public int? pagRegistros { get; set; }
    }
}