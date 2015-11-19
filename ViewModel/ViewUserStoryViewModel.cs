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
    public class ViewUserStoryViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<UserStory> userStoryCollection { get; set; }

        Requester requester= new Requester();

        public ViewUserStoryViewModel()
        {
            //GetUserStories();
        }
        public ObservableCollection<UserStory> GetUserStories()
        {
            var result = requester.GetRequestAsync<UserStory>(EndPoints.UserStories);
            userStoryCollection = new ObservableCollection<UserStory>(result.Result);
            return userStoryCollection;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
