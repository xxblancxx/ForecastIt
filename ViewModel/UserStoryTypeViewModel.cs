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
using Forecast.it.Model;

namespace Forecast.it.ViewModel
{
    public class UserStoryTypeViewModel : INotifyPropertyChanged
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

        ObservableCollection<UserStoryType> _UserStoryTypes;
        public ObservableCollection<UserStoryType> UserStoryTypes
        {
            get { return _UserStoryTypes; }
            set
            {
                _UserStoryTypes = value;
                OnPropertyChanged("UserStoryTypes");
            }
        }

        UserStoryType _UserStoryType;

        public UserStoryType UserStoryType
        {
            get { return _UserStoryType; }
            set
            {
                _UserStoryType = value;
                if (UserStoryType != null)
                {
                    id = UserStoryType.id;
                }
                OnPropertyChanged("UserStoryType");
            }
        }
        UserStoryType _SelectedOrder;
        public UserStoryType SelectedOrder
        {
            get { return _SelectedOrder; }
            set
            {
                _SelectedOrder = value;
                UserStoryType = _SelectedOrder;
                OnPropertyChanged("UserStoryType");
            }
        }
        int _UserStoryTypesId = 0;
        public int id
        {
            get { return _UserStoryTypesId; }
            set
            {
                _UserStoryTypesId = value;
                OnPropertyChanged("id");
            }
        }

        //Command objects
        public ICommand NewOrders { get; private set; }
        public ICommand AddOrder { get; private set; }
        //public ICommand UpdateOrder { get; private set; }
        //public ICommand DeleteOrder { get; private set; }

        public UserStoryTypeViewModel()
        {
            //NewOrders = new RoutedCommand(NewOrder);
            //AddOrder = new RoutedCommand(CreateOrder);
            // UpdateOrder = new RoutedCommand(EditOrder);
            //DeleteOrder = new RoutedCommand(RemoveOrder);
            //connect();
            LoadOrders();
            UserStoryType = new UserStoryType();
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

                client.BaseAddress = new Uri("https://api.forecast.it/api/v1/");

                var byteArray =
                   Encoding.UTF8.GetBytes(_singleton.CurrentUsername + ":" + _singleton.CurrentPassword);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                //client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(byteArray));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("userStoryTypes").Result;

                try
                {
                    response.EnsureSuccessStatusCode();
                    using (var responseStream = await response.Content.ReadAsStreamAsync())
                    {
                        var ordersList = new DataContractJsonSerializer(typeof(List<UserStoryType>));
                        UserStoryTypes = new ObservableCollection<UserStoryType>((IEnumerable<UserStoryType>)ordersList.ReadObject(responseStream));
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
