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

        

        private static UserStory StaticUserStory
        {

            get
            {
                if (_staticUserStory != null)
                {
                     return _staticUserStory;
                }
               _staticUserStory = new UserStory();
                return _staticUserStory;
            }
            set { _staticUserStory = value; }
        }


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
        public static UserStory _staticUserStory;


        public ViewTaskViewModel()
        {
            StaticUserStory = StaticUserStory;
            GetTasks();
        }

        public ObservableCollection<Task> GetTasks()
        {
            List<Task> result = requester.GetRequestAsync<Model.Task>(EndPoints.Tasks, 712).Result;
            var sortByID = result.Where(a => a.userStory == StaticUserStory.id).OrderBy(a => a.id);
            TaskCollection = new ObservableCollection<Model.Task>(sortByID);

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
