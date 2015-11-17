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
    public class ViewTaskViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Model.Task> TaskCollection { get; set; }
        Requester requester = new Requester();

        public ViewTaskViewModel()
        {
            GetTasks();
        }

        public ObservableCollection<Model.Task> GetTasks()
        {
            var result = requester.GetRequestAsync<Model.Task>(EndPoints.Users);
            TaskCollection = new ObservableCollection<Model.Task>(result.Result);
            return TaskCollection;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
