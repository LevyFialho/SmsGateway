using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsAgileSoapApi4
{
    public class Contato
    {

        public Contato(string nome, long numero)
        {
            Nome = nome;
            Numero = numero;
        }

        public Guid Id { get; set; }

        /// <summary>
        /// Nome do Contato
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Numero do Contato
        /// </summary>
        public long Numero { get; set; }
        

    }
}
