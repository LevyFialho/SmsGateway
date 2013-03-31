using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsAgileSoapApi4
{
    public class ListaDeContatos
    {

        public ListaDeContatos(string nome)
        {
            Nome = nome;
            Contatos = new List<Contato>();
        }
        public Guid Id { get; set; }

        /// <summary>
        /// Contatos
        /// </summary>
        public List<Contato> Contatos { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        public string Nome { get; set; }
    }
}
