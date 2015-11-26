using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Forecast.it.Annotations;
using Forecast.it.Common;
using Forecast.it.Model;
using ForecastModel.Connection;
using Task = System.Threading.Tasks.Task;

namespace Forecast.it.ViewModel
{
    public class EditTaskViewModel : INotifyPropertyChanged
    {
        //public int Id { get; set; }

        public static Model.Task StaticTask { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Estimate { get; set; }

        public int Status { get; set; }



        public ObservableCollection<Task> TaskCollection { get; set; }
        Requester requester = new Requester();
        private ICommand _saveTask;

        public ICommand SaveTask
        {
            get
            {
                _saveTask = new RelayCommand(EditTask);
                return _saveTask;
            }
            set { _saveTask = value; }
        }

        public EditTaskViewModel()
        {
            StaticTask = StaticTask;
            Title = StaticTask.title;
            Description = StaticTask.description;
            Estimate = StaticTask.estimate;
            Status = StaticTask.status;

        }



        public void EditTask()
        {
            var req = new Requester();
            
            Model.Task editTask = new Model.Task(Title, Description, Estimate, Status);
            req.PutRequest(editTask, EndPoints.Tasks, 712, StaticTask.id);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
