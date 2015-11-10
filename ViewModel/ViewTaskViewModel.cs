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
using Task = Forecast.it.Model.Task;

namespace Forecast.it.ViewModel
{
    public class ViewTaskViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Task> taskCollection { get; set; }
        List<int> ints = new List<int>(); 

        public ViewTaskViewModel()
        {
            taskCollection = new ObservableCollection<Task>();
            const string url = "https://api.forecast.it/api/v1/projects/712/tasks";
            taskCollection.Add(new Task("bla",1,"","",1,1,1,1,1,ints,1,DateTime.Now, ints,true,1,1,1,DateTime.Now, 1,DateTime.Today, 1));

        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
