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
    public class ViewTaskViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Model.Task> TaskCollection { get; set; }

        public ViewTaskViewModel()
        {
            TaskCollection = new ObservableCollection<Model.Task>();
            const string url = "https://api.forecast.it/api/v1/projects/1/tasks/1";
            TaskCollection.Add(new Model.Task());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
