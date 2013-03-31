
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;
using Presentation.WPFClient;
using SmsAgileSoapApi;
using SmsGateway.Presentation.WPFClient.Shared;

namespace SmsGateway.Presentation.WPFClient.Model
{
    public class LoginModel : ModelBase
    {
        private string _email { get; set; }
        private string _senha { get; set; }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(_email);
            }
        }

        public string Senha
        {
            get { return _senha; }
            set
            {
                _senha = value;
                OnPropertyChanged(_senha);
            }
        }
        public void Login(string email, string senha)
        {
            try
            {
                var service = new Service(email, senha, string.Empty);
                SetApiService(service);
                var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                var page = new ClientWindow();
                Application.Current.MainWindow = page;
                currentWindow.Close();
                page.Show();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
