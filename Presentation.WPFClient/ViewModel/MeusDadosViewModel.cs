using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmsGateway.Presentation.WPFClient.Commands;
using SmsGateway.Presentation.WPFClient.Model;

namespace SmsGateway.Presentation.WPFClient.ViewModel
{
    public class MeusDadosViewModel
    {
        private MeusDadosModel _model;
        
        public MeusDadosModel Model
        {
            get { return _model; }
            set { _model = value; }
        }

        public MeusDadosViewModel()
        {
            Model = new MeusDadosModel();
        }
    }
}
