using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
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

        public async void CreateUserInAPI()
        {
            if (FirstName != null && LastName != null && Email != null)
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
                User newUser = new User(FirstName, LastName, initials, Email, false, "Dashboard", 1368);

                var status = req.PostRequest(newUser, EndPoints.Users);
                if (await status)
                {
                    SingletonCommon.SingletonInstance.CurrentPageView.Frame.Navigate(typeof(MainPage));
                }
                else
                {
                    new MessageDialog("To create new user, first return and log in with Admin user.").ShowAsync();
                }


            }
            else
            {
                var msg = new MessageDialog("Fill out all fields");
                msg.ShowAsync();
            }

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
