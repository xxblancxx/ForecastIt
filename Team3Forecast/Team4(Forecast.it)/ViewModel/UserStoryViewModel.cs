using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.WebUI;
using Windows.UI.Xaml.Controls;
using Forecast.it.Annotations;
using Forecast.it.Infrastructure;
using Forecast.it.Model;

namespace Forecast.it.ViewModel
{
   public class UserStoryViewModel:INotifyPropertyChanged
    { private static int pid = (App.Current as App).project_id;


        public event PropertyChangedEventHandler PropertyChanged;


        private void OnPropertyChanged(string pName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(pName));
            }
        }

        ObservableCollection<UserStory> _UserStorys;
        public ObservableCollection<UserStory> UserStorys
        {
            get { return _UserStorys; }
            set
            {
                if (_UserStorys != value)
                {
                    _UserStorys = value;
                    OnPropertyChanged("UserStorys");
                }
            }
        }

        UserStory _UserStory;

        public UserStory UserStory
        {
            get { return _UserStory; }
            set
            {
                _UserStory = value;
                if (UserStory != null)
                {
                    id = UserStory.id;
                }
                OnPropertyChanged("UserStory");
            }
        }
        UserStory _SelectedOrder;
        public UserStory SelectedOrder
        {
            get { return _SelectedOrder; }
            set
            {
                _SelectedOrder = value;
                UserStory = _SelectedOrder;
                OnPropertyChanged("UserStory");
            }
        }

       int _UserStoryId = 0;
        public int id
        {
            get { return _UserStoryId; }
            set
            {
                _UserStoryId = value;
                OnPropertyChanged("id");
            }
        }

        //Command objects
        public ICommand NewOrders { get; private set; }
        public ICommand AddOrder { get; private set; }
        //public ICommand UpdateOrder { get; private set; }
        //public ICommand DeleteOrder { get; private set; }

        public UserStoryViewModel()
        {
            //NewOrders = new RoutedCommand(NewOrder);
            AddOrder = new RoutedCommand(CreateOrder);
           // UpdateOrder = new RoutedCommand(EditOrder);
          //DeleteOrder = new RoutedCommand(RemoveOrder);
            //connect();
            //LoadOrders();
            UserStory = new UserStory();
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

        //private async void LoadOrders()
        //{
           
        //     using (var client = new HttpClient())
        //     {
        //         //27578cb2-8b15-417b-9b42-36ca8922f92c
        //        client.BaseAddress = new Uri((App.Current as App).BaseAddress);

        //        var byteArray = Encoding.UTF8.GetBytes("silverlightjashmin@gmail.com:jashmin86527");
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

        //        //client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(byteArray));
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpResponseMessage response = client.GetAsync("projects/712/userStories/").Result;
               
        //        try
        //        {
        //            response.EnsureSuccessStatusCode();
        //            using (var responseStream = await response.Content.ReadAsStreamAsync())
        //            {
        //                var ordersList = new DataContractJsonSerializer(typeof(List<UserStory>));
        //                UserStorys = new ObservableCollection<UserStory>((IEnumerable<UserStory>)ordersList.ReadObject(responseStream));
        //            }
        //        }
        //        catch (HttpRequestException)
        //        {
        //            var msg =
        //                new MessageDialog("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
        //            msg.ShowAsync();

        //        }
        //    }
        //}

       
        async void CreateOrder(object o)
        {
           

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri((App.Current as App).BaseAddress);

                var byteArray = Encoding.UTF8.GetBytes((App.Current as App).username + ":" + (App.Current as App).password);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                //client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(byteArray));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (var memStream = new MemoryStream())
                {
                    var data = new DataContractJsonSerializer(typeof(UserStory));
                    data.WriteObject(memStream, UserStory);
                    memStream.Position = 0;
                    var contentToPost = new StreamContent(memStream);
                    contentToPost.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                     try
                    {
                        var response = await client.PostAsync("projects/"+pid+"/userStories", contentToPost);
                        response.EnsureSuccessStatusCode();
                       await new MessageDialog("New UserStory Added Successfully").ShowAsync();
                        
                    }
                    catch (Exception e)
                    {
                        new MessageDialog(e.Message).ShowAsync();
                        

                    }
                }
            }
            
           
        }
       
    
}
}
