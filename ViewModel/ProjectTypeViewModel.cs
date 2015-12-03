using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Forecast.it.Common;
using Forecast.it.Infrastructure;

namespace Forecast.it.Model
{
    public class ProjectTypeViewModel : INotifyPropertyChanged
    {
        //POST  shuld be posted like-https://api.forecast.it/api/v1/projects/1/tasks

        private static int pid = (App.Current as App).project_id;
        private SingletonCommon _singleton = SingletonCommon.SingletonInstance;


        public event PropertyChangedEventHandler PropertyChanged;


        private void OnPropertyChanged(string pName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(pName));
            }
        }

        ObservableCollection<ProjectType> _ProjectTypes;
        public ObservableCollection<ProjectType> ProjectTypes
        {
            get { return _ProjectTypes; }
            set
            {
                _ProjectTypes = value;
                OnPropertyChanged("ProjectTypes");
            }
        }

        ProjectType _ProjectType;

        public ProjectType ProjectType
        {
            get { return _ProjectType; }
            set
            {
                _ProjectType = value;
                if (ProjectType != null)
                {
                    id = ProjectType.id;
                }
                OnPropertyChanged("ProjectType");
            }
        }
        ProjectType _SelectedOrder;
        public ProjectType SelectedOrder
        {
            get { return _SelectedOrder; }
            set
            {
                _SelectedOrder = value;
                ProjectType = _SelectedOrder;
                OnPropertyChanged("ProjectType");
            }
        }
        int _ProjectTypeId = 0;
        public int id
        {
            get { return _ProjectTypeId; }
            set
            {
                _ProjectTypeId = value;
                OnPropertyChanged("id");
            }
        }

        //Command objects
        public ICommand NewOrders { get; private set; }
        public ICommand AddOrder { get; private set; }
        //public ICommand UpdateOrder { get; private set; }
        //public ICommand DeleteOrder { get; private set; }

        public ProjectTypeViewModel()
        {
            //NewOrders = new RoutedCommand(NewOrder);
            //AddOrder = new RoutedCommand(CreateOrder);
            // UpdateOrder = new RoutedCommand(EditOrder);
            //DeleteOrder = new RoutedCommand(RemoveOrder);
            //connect();
            LoadOrders();
            ProjectType = new ProjectType();
        }
        
        private async void LoadOrders()
        {

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri((App.Current as App).BaseAddress);

                var byteArray = Encoding.UTF8.GetBytes(_singleton.CurrentUsername + ":" + _singleton.CurrentPassword);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                //client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(byteArray));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("projectTypes/").Result;

                try
                {
                    response.EnsureSuccessStatusCode();
                    using (var responseStream = await response.Content.ReadAsStreamAsync())
                    {
                        var ordersList = new DataContractJsonSerializer(typeof(List<ProjectType>));
                        ProjectTypes = new ObservableCollection<ProjectType>((IEnumerable<ProjectType>)ordersList.ReadObject(responseStream));
                    }
                }
                catch (HttpRequestException)
                {
                    var msg =
                        new MessageDialog("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
                    msg.ShowAsync();

                }
            }
        }



    }
}
