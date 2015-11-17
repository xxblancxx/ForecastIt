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
using ForecastModel.Connection;

namespace Forecast.it.ViewModel
{
    class CreateUserViewModel : INotifyPropertyChanged
    {

        // TODO; 
        //  - Make properties with two-way binding. - pending
        //  - Make Command which parses to API - pending

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        private RelayCommand _createUserCommand;

        public RelayCommand CreateUserCommand
        {
            get
            {
                _createUserCommand = new RelayCommand(CreateUserInAPI);
                return _createUserCommand;
            }
            private set { }
        }

        public void CreateUserInAPI()
        {
            var req = new Requester();

            string initials = "";
            var splitFirstName = FirstName.Split(' ');
            var splitLastName = LastName.Split(' ');
            foreach (var word in splitFirstName)
            {
                initials += word[0];
            }
            foreach (var word in splitLastName)
            {
                initials += word[0];
            }
           initials = initials.ToUpper();

            User newUser = new User(FirstName, LastName, initials, Email, false, "Dashboard", 0);
            req.PostRequest(newUser, EndPoints.Users);
        }

        #region Inotify implementation
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion
    }
}
