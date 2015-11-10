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

namespace Forecast.it.ViewModel
{
    public class SeeDevelopersViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<User> userCollection { get; set; }
       

        public SeeDevelopersViewModel()
        {
            userCollection = new ObservableCollection<User>();
            const string url = "https://api.forecast.it/api/v1/users/";
            userCollection.Add(new User("URL Test",1,"bob","bobsen", "BB","bob@email.farout",DateTime.Now, DateTime.Now, true,true,1,"bob knows his id","default startpage",4,""));


        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
