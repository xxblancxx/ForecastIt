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
        public ObservableCollection<object> userStoryCollection { get; set; }
        public ObservableCollection<UserStory> tempCollection { get; set; }

        Requester requester= new Requester();

        public ViewUserStoryViewModel()
        {
            GetUserStories(712);
        }
        public ObservableCollection<object> GetUserStories(int projectId)
        {
            var result = requester.GetRequestAsync<UserStory>(EndPoints.UserStories, projectId);
            tempCollection = new ObservableCollection<UserStory>(result.Result);
            IterateCollection();
            return userStoryCollection;
        }

        public ObservableCollection<object> IterateCollection()
        {
            userStoryCollection = new ObservableCollection<object>();
            foreach (var var in tempCollection)
            {
                userStoryCollection.Add(var.title);
                userStoryCollection.Add(var.estimate);
            }
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
