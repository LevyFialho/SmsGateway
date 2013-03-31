using System.Collections.Generic;
using System.Linq;
using SmsAgileSoapApi4.SmsAgileSoapService;
using System;
namespace SmsAgileSoapApi4
{
    internal static class Assembler
    {
        public static MensagemDTO Convert(Mensagem mensagem)
        {
            return new MensagemDTO()
            {
                TextoDaMensagem = mensagem.TextoDaMensagem,
                NumeroDoDestinatario = mensagem.Destinatario.ToString(),
                NumeroDoRemetente = mensagem.Remetente
            };

        }

        public static Mensagem Convert(MensagemDTO mensagem)
        {
            
            return new Mensagem(mensagem.TextoDaMensagem, System.Convert.ToInt64(mensagem.NumeroDoDestinatario), mensagem.NumeroDoRemetente)
            {
                Status = mensagem.StatusMensagemAoCliente,
                Id = mensagem.Id
            };

        }

        public static Contato Convert(ContatoDTO contato)
        {
            return new Contato(contato.Nome,contato.Numero)
                {
                    Id = contato.Id,
                   };
        }

        public static ContatoDTO Convert(Contato contato, Guid idCliente)
        {

            return new ContatoDTO()
                {
                    Nome = contato.Nome,
                    Numero = contato.Numero,
                    ClienteId =  idCliente,
                    Id = contato.Id

                };
        }

        public static ListaDeContatos Convert(ListaDeContatosDTO dto)
        {
            var lista = new ListaDeContatos(dto.Nome)
                {
                    Id = dto.Id,
                    Contatos = new List<Contato>(),
                };
            lista.Contatos.AddRange(dto.Contatos.Select(Convert));
            return lista;
        }

        public static ListaDeContatosDTO Convert(ListaDeContatos lista, Guid idCliente)
        {
            var dto = new ListaDeContatosDTO()
            {
                Nome =  lista.Nome,
                Id = lista.Id, 
                ClienteId = idCliente
            };
            var contatos = new List<ContatoDTO>();
            contatos.AddRange(lista.Contatos.Select(c => Convert(c, idCliente)));
            dto.Contatos = contatos.ToArray();
            return dto;

        }
    }
}
