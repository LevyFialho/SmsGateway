using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsGateway.Presentation.WPFClient.Model
{
    public sealed class MeusDadosModel: ModelBase
    {
        private string _id;
        private int _totalDeMensagensEnviadas;
        private int _saldoRemanescente;
        private string _senha;
        private string _nome;

        public MeusDadosModel()
        {
            var service = GetApiService();
            var dados = service.Dados();

            Id = dados.Id;
            TotalDeMensagensEnviadas = dados.TotalDeMensagensEnviadas;
            SaldoRemanescente = dados.SaldoRemanescente;
            Senha = dados.Senha;
            Nome = dados.Nome;

        }

        public string Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged("Id");}
        }

        public int TotalDeMensagensEnviadas
        {
            get { return _totalDeMensagensEnviadas; }
            set { _totalDeMensagensEnviadas = value; OnPropertyChanged("TotalDeMensagensEnviadas"); }
        }

        public int SaldoRemanescente
        {
            get { return _saldoRemanescente; }
            set { _saldoRemanescente = value; OnPropertyChanged("SaldoRemanescente"); }
        }

        public string Senha
        {
            get { return _senha; }
            set { _senha = value; OnPropertyChanged("Senha"); }
        }

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; OnPropertyChanged("Nome"); }
        }
    }
}
