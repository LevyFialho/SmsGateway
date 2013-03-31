using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SmsAgileSoapApi;
using SmsGateway.Presentation.WPFClient.Shared;

namespace SmsGateway.Presentation.WPFClient.Model
{
    public class ModelBase : INotifyPropertyChanged
    {

        protected virtual Service GetApiService()
        {
            return ApplicationState.GetValue<Service>("ApiService");
        }
        protected virtual void SetApiService(object service)
        {
             ApplicationState.SetValue("ApiService", service);
        }
        protected virtual void HandleException(Exception ex)
        {
            MessageBox.Show("Erro inesperado: " + ex.Message + " - " + ex.StackTrace);
        }
        #region INotifyPropertyChanged Members

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
