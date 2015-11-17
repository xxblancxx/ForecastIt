using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Forecast.it.Annotations;
using Forecast.it.Model;
using ForecastModel.Connection;

namespace Forecast.it.ViewModel
{
    public class SeeDevelopersViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<User> userCollection { get; set; }
        Requester requester = new Requester();

        public SeeDevelopersViewModel()
        {

            //const string url = "https://api.forecast.it/api/v1/users/";
            //userCollection.Add(new User(url, 1, "bob", "bobsen", "BB", "bob@email.farout", DateTime.Now, DateTime.Now, true, true, 1, "bob knows his id", "default startpage", 4, ""));
            GetUsers();


        }

        public ObservableCollection<User> GetUsers()
        {
            var result = requester.GetRequestAsync<User>(EndPoints.Users);
            userCollection = new ObservableCollection<User>(result.Result);
            return userCollection;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
