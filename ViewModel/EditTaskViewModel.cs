using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Forecast.it.Annotations;
using Forecast.it.Model;
using Task = System.Threading.Tasks.Task;

namespace Forecast.it.ViewModel
{
    public class EditTaskViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Task> TaskCollection { get; set; }
        Requester requester = new Requester();

        public EditTaskViewModel()
        {
            
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
