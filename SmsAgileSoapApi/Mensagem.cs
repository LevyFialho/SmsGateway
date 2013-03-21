using System;

namespace SmsAgileSoapApi
{
    public class Mensagem
    {
        public Mensagem(string texto, long destinatario, string remetente)
        {
            TextoDaMensagem = texto;
            Destinatario = destinatario;
            Remetente = remetente;
        }

        public Guid Id { get; set; }
        /// <summary>
        /// texto do SMS Enviado
        /// </summary>
        public string TextoDaMensagem { get; set; }

        /// <summary>
        /// Número de telefone do destinatário
        /// </summary>
        public long Destinatario { get; set; }

        /// <summary>
        /// Número de telefone ou nome do remetente
        /// </summary>
        public string Remetente { get; set; }

        /// <summary>
        /// Código do Status da mensagem
        /// </summary>
        public string Status { get; set; }

    }
}
