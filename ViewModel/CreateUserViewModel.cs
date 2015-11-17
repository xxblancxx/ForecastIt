using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Forecast.it.Annotations;
using Forecast.it.Model;

namespace Forecast.it.ViewModel
{
    class CreateUserViewModel : INotifyPropertyChanged
    {

        // TODO; 
        //  - Make properties with two-way binding. - pending
        //  - Make Command which parses to API - pending

        public string Name { get; set; }
        public string Email { get; set; }

        public void CreateUserInAPI()
        {
            var req = new Requester();
            //add information to user based on props.
            User user = new User();
           // req.PostRequest()
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
