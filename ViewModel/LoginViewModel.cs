using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Forecast.it.Annotations;
using Forecast.it.Common;
using Forecast.it.Model;

namespace Forecast.it.ViewModel
{
    class LoginViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private SingletonCommon _singleton = SingletonCommon.SingletonInstance;
        private RelayCommand _loginCommand;

        public RelayCommand LoginCommand
        {
            get
            {
                _loginCommand = new RelayCommand(TryLoggingIn);
                return _loginCommand;
            }
           private set { }
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }

        private void TryLoggingIn()
        {
            _singleton.CurrentUsername = Username;
            _singleton.CurrentPassword = Password;
            var requester = new Requester();
            requester.LogIn();
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion
    }
}
