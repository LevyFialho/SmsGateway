using System; 
using System.Windows.Input;
using SmsGateway.Presentation.WPFClient.Commands;
using SmsGateway.Presentation.WPFClient.Model;

namespace SmsGateway.Presentation.WPFClient.ViewModel
{
    public class LoginViewModel
    {
        private DelegateCommand _loginCommand;
        public LoginModel Dados { get; set; }
     
        public LoginViewModel()
        {
            Dados = new LoginModel(){ Email = string.Empty, Senha = string.Empty};
        }

        public ICommand LoginCommand
        {
            get
            {                 
                if(_loginCommand == null)
                    _loginCommand = new DelegateCommand(new Action(LoginExecuted), new Func<bool>(LoginCanExecute));

                return _loginCommand;
            }

        }

        

        public bool LoginCanExecute()
        {
            return   !string.IsNullOrWhiteSpace(Dados.Email) && !string.IsNullOrEmpty(Dados.Senha);
        }

        public void LoginExecuted()
        {
            Dados.Login(Dados.Email, Dados.Senha);
        }
    }
}
