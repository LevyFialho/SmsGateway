﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsAgileSoapApi
{
    public class DadosDoCliente
    {
        public string Id { get; set; }

        public int TotalDeMensagensEnviadas { get; set; }

        public int SaldoRemanescente { get; set; }

        public string Senha { get; set; }

        public string Nome { get; set; }
    }
}
