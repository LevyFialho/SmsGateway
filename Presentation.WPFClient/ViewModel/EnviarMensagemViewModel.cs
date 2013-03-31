using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using SmsAgileSoapApi;
using SmsGateway.Presentation.WPFClient.Commands;
using SmsGateway.Presentation.WPFClient.Shared;

namespace SmsGateway.Presentation.WPFClient.ViewModel
{
    public class EnviarMensagemViewModel
    {
        private string _texto;
        private string _numero;
        private string _remetente;
        private DelegateCommand _enviarCommand;

        public EnviarMensagemViewModel()
        {
            Texto = string.Empty;
            Numero = string.Empty;
            Remetente = string.Empty;
        }

        public string Texto
        {
            get { return _texto; }
            set { _texto = value; }
        }

        public string Numero
        {
            get { return _numero; }
            set { _numero = value; }
        }

        public string Remetente
        {
            get { return _remetente; }
            set { _remetente = value; }
        }

        public DelegateCommand EnviarCommand
        {
            get { return _enviarCommand ?? (_enviarCommand = new DelegateCommand(Enviar, EnviarCanExecute)); }
        }

        public void Enviar()
        {
            var msg = new Mensagem(Texto, Convert.ToInt64(Numero), Remetente);
            msg = ApplicationState.GetValue<Service>("ApiService").EnviarMensagem(msg);
            Texto = string.Empty;
            Numero = string.Empty;
            Remetente = string.Empty;
            MessageBox.Show("Envio efetuado. Status de envio: " + msg.Status);
        }

        public bool EnviarCanExecute()
        {
            var regex = new Regex(@"^\d$");
            return !string.IsNullOrWhiteSpace(Texto) && !string.IsNullOrWhiteSpace(Numero) && regex.IsMatch(Numero);
        }
    }
}
