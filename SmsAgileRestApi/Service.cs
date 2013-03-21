
using SmsGateway.Application.CoreContext.DTO.SMSModule;

namespace SmsAgileRestApi
{
    public class Service
    {
        private readonly AutenticacaoDTO _autenticacao = new AutenticacaoDTO();

        public void Autenticacao(string id, string senha)
        {
            _autenticacao.Id = id;
            _autenticacao.Senha = senha;
        }

        public Mensagem EnviarMensagem(Mensagem mensagem)
        {
            return Assembler.Convert(Factory.Service().EnviarMensagem(_autenticacao, Assembler.Convert(mensagem)));
        }
    }

    public class Mensagem 
    {
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
            return new Mensagem()
            {
                TextoDaMensagem = mensagem.TextoDaMensagem,
                Destinatario = System.Convert.ToInt64(mensagem.NumeroDoDestinatario),
                Remetente = mensagem.NumeroDoRemetente,
                Status =  mensagem.StatusMensagemAoCliente
            };

        }
    }
}
