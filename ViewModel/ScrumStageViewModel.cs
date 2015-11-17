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
    public class ScrumStageViewModel : INotifyPropertyChanged
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

        ObservableCollection<ScrumStage> _ScrumStages;
        public ObservableCollection<ScrumStage> ScrumStages
        {
            get { return _ScrumStages; }
            set
            {
                _ScrumStages = value;
                OnPropertyChanged("ScrumStages");
            }
        }

        ScrumStage _ScrumStage;

        public ScrumStage ScrumStage
        {
            get { return _ScrumStage; }
            set
            {
                _ScrumStage = value;
                if (ScrumStage != null)
                {
                    id = ScrumStage.id;
                }
                OnPropertyChanged("ScrumStage");
            }
        }
        ScrumStage _SelectedOrder;
        public ScrumStage SelectedOrder
        {
            get { return _SelectedOrder; }
            set
            {
                _SelectedOrder = value;
                ScrumStage = _SelectedOrder;
                OnPropertyChanged("ScrumStage");
            }
        }
        int _ScrumStageId = 0;
        public int id
        {
            get { return _ScrumStageId; }
            set
            {
                _ScrumStageId = value;
                OnPropertyChanged("id");
            }
        }

        //Command objects
        public ICommand NewOrders { get; private set; }
        public ICommand AddOrder { get; private set; }
        //public ICommand UpdateOrder { get; private set; }
        //public ICommand DeleteOrder { get; private set; }

        public ScrumStageViewModel()
        {
            //NewOrders = new RoutedCommand(NewOrder);
            //AddOrder = new RoutedCommand(CreateOrder);
            // UpdateOrder = new RoutedCommand(EditOrder);
            //DeleteOrder = new RoutedCommand(RemoveOrder);
            //connect();
            LoadOrders();
            ScrumStage = new ScrumStage();
        }
        //async void connect()
        //{
        //    using (var client = new HttpClient())
        //    {

        //        client.BaseAddress = new Uri("https://api.forecast.it/api/v1/projects");

        //        var byteArray = Encoding.UTF8.GetBytes("27578cb2-8b15-417b-9b42-36ca8922f92c:jashmin86527");
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
        //            Convert.ToBase64String(byteArray));

        //        //client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(byteArray));
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    }
        //}
        private async void LoadOrders()
        {

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri((App.Current as App).BaseAddress);

                var byteArray = Encoding.UTF8.GetBytes(_singleton.CurrentUsername + ":" + _singleton.CurrentPassword);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                //client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(byteArray));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("projects/712/scrumStages/").Result;

                try
                {
                    response.EnsureSuccessStatusCode();
                    using (var responseStream = await response.Content.ReadAsStreamAsync())
                    {
                        var ordersList = new DataContractJsonSerializer(typeof(List<ScrumStage>));
                        ScrumStages = new ObservableCollection<ScrumStage>((IEnumerable<ScrumStage>)ordersList.ReadObject(responseStream));
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
