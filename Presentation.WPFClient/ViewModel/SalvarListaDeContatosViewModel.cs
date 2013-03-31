using System;
using System.Windows;
using SmsAgileSoapApi;
using SmsGateway.Presentation.WPFClient.Commands;
using SmsGateway.Presentation.WPFClient.Shared;

namespace SmsGateway.Presentation.WPFClient.ViewModel
{
    public class SalvarListaDeContatosViewModel
    {
        private SmsAgileSoapApi.ListaDeContatos _model;
        private DelegateCommand _saveCommand;

        public SalvarListaDeContatosViewModel(Guid listaId)
        {
            _model = new ListaDeContatos(string.Empty);
            if(listaId != Guid.Empty)
            {
                _model = ApplicationState.GetValue<Service>("ApiService").GetListaDeContatos(listaId);
            }
        }

        public SmsAgileSoapApi.ListaDeContatos Model
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
            ApplicationState.GetValue<Service>("ApiService").AdicionarListaDeContatos(Model);

            MessageBox.Show("Contato salvo com sucesso.");
        }

        public bool SaveCanExecute()
        {
            return Model != null && !string.IsNullOrEmpty(Model.Nome) && !string.IsNullOrEmpty(Model.Nome);
        }


    }
}
