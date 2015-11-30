using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Forecast.it.Annotations;
using Forecast.it.Common;
using Forecast.it.Model;
using ForecastModel.Connection;
using Task = Forecast.it.Model.Task;


namespace Forecast.it.ViewModel
{
    public class ViewTaskViewModel : INotifyPropertyChanged
    {
        public static int TaskId { get; set; }

        //public RelayArgCommand<int> SelectedTaskArgCommand          
        //{
        //    get { return _relayArgCommand ?? (_relayArgCommand = new RelayArgCommand<int>(OnSelectionChanged,OnSelectedTask)); }
        //    set
        //    {

        //        _relayArgCommand = value;
        //    }
        //}

        //private bool OnSelectedTask(int id)
        //{
            
        //}

        

        public void OnSelectionChanged(int obj)
        {
            
        }

        public ObservableCollection<Task> TaskCollection
        {
            get { return _taskCollection; }
            set { _taskCollection = value; }
        }

        Requester requester = new Requester();
        private RelayArgCommand<int> _relayArgCommand;
        private ObservableCollection<Task> _taskCollection;


        public ViewTaskViewModel()
        {
            GetTasks();
        }

        public ObservableCollection<Task> GetTasks()
        {
            List<Task> result = requester.GetRequestAsync<Model.Task>(EndPoints.Tasks, 712).Result;
            
            TaskCollection = new ObservableCollection<Model.Task>(result);
            
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
