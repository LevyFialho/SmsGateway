using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SmsAgileSoapApi;
using SmsGateway.Presentation.WPFClient.Commands;
using SmsGateway.Presentation.WPFClient.Shared;

namespace SmsGateway.Presentation.WPFClient.ViewModel
{
    public class SalvarContatoViewModel
    {
        private SmsAgileSoapApi.Contato _model;
        private DelegateCommand _saveCommand;

        public SalvarContatoViewModel(Guid contatoId)
        {
            _model = new Contato(string.Empty, 5521) {Id = contatoId};
        }

        public SmsAgileSoapApi.Contato Model
        {
            get { return _model; }
            set { _model = value; }
        }

        public DelegateCommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new DelegateCommand(Save, SaveCanExecute));
            }
        }

        public void Save()
        {
            if (Model.Id == Guid.Empty)
                ApplicationState.GetValue<Service>("ApiService").AdicionarContato(Model);
            else
                ApplicationState.GetValue<Service>("ApiService").AtualizarContato(Model);

            MessageBox.Show("Contato salvo com sucesso.");
        }

        public bool SaveCanExecute()
        {
            return Model != null && !string.IsNullOrEmpty(Model.Nome) && !string.IsNullOrEmpty(Model.Nome);
        }


    }
}
