using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Forecast.it.Infrastructure;
using Forecast.it.Model;
using Newtonsoft.Json;
using Forecast.it.Common;
using System.Collections.ObjectModel;
using System.Runtime.Serialization.Json;

namespace Forecast.it.ViewModel
{
    class TaskViewModel : INotifyPropertyChanged
    {

        private static int pid = (App.Current as App).project_id;
        private SingletonCommon _singleton = SingletonCommon.SingletonInstance;


        public TaskViewModel()
        {

            AddTask = new RoutedCommand(CreateTasked);
            InitCatalog();
        }
        private async void InitCatalog()
        {
            UserStorys = new ObservableCollection<UserStory>();
            var userstorylist = await LoadUserStory();
            if (userstorylist != null)
                foreach (var userstory in userstorylist)
                {
                    UserStorys.Add(userstory);
                }
        }
        private async Task<IEnumerable<UserStory>> LoadUserStory()
        {

            using (var client = new HttpClient())
            {
                //27578cb2-8b15-417b-9b42-36ca8922f92c
                client.BaseAddress = new Uri((App.Current as App).BaseAddress);
                var byteArray =
                    Encoding.UTF8.GetBytes(_singleton.CurrentUsername + ":" + _singleton.CurrentPassword);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(byteArray));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("projects/712/userStories/").Result;

                try
                {
                    response.EnsureSuccessStatusCode();
                    using (var responseStream = await response.Content.ReadAsStreamAsync())
                    {
                        var ordersList = new DataContractJsonSerializer(typeof(List<UserStory>));
                        var UserStorys =
                            new ObservableCollection<UserStory>(
                                (IEnumerable<UserStory>)ordersList.ReadObject(responseStream));
                        return UserStorys;
                    }
                }
                catch (HttpRequestException)
                {
                    var msg =
                        new MessageDialog("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
                    msg.ShowAsync();

                }
                return null;
            }
        }
        private void CreateTasked(object obj)
        {
            Task = new Tasked();
            Task.title = Title;
            Task.estimate = estimate;
            Task.status = status;
            Task.userStory = userStory;
            Task.projectPhase = projectPhase;
            //Tasked task = new Tasked();
            //task.projectPhase = 1;
            //task.status = 2;
            //task.userStory = 2809;
            //task.title = "qqqq";
            CreateTask(Task);
        }
        //public ObservableCollection<UserStory> userStoryCollection { get; set; }

        // public ObservableCollection<UserStory> userCollection { get; set; }
        // List<UserStory> TestUserStoryList;

        ObservableCollection<UserStory> _userstory;

        public ObservableCollection<UserStory> UserStorys
        {
            get { return _userstory; }
            set
            {
                _userstory = value;
                OnPropertyChanged("userStory");
            }
        }

       
        //public void GetAllUserStoriesFromProjectViewModel()
        // {
        //userStoryCollection = new ObservableCollection<UserStory>();
        //Link only for our project for the moment
        // const string url = "https://api.forecast.it/api/v1/projects/712/userStories/";
        //userCollection.Add(new UserStory(url, 2799, "TestTitle", "TestDescription", 929, "TestAcceptanceCriteria", 16, "", 885, 1116, null, null, "2015-10-08T11:34:30+02:00", 6320, "2015-10-06T09:42:06+02:00", 620, null, "[]", "[]", "[]", "[7031, 7033, 7034]"));
        // userCollection.Add(new UserStory(url, 2799, "TestTitle", "TestDescription", 929, "TestAcceptanceCriteria", 16, "", 885, 166, "[]", "[]"));
        //}
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string pName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(pName));
            }
        }

        public ICommand AddTask { get; private set; }

        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        private int _status;
        public int status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged("status");
            }
        }

        private int _estimate;
        public int estimate
        {
            get { return _estimate; }
            set
            {
                _estimate = value;
                OnPropertyChanged("estimate");
            }
        }

        private int _projectPhase;

        public int projectPhase
        {
            get { return _projectPhase; }
            set
            {
                _projectPhase = value;
                OnPropertyChanged("projectPhase");
            }
        }

        private int _userStory;

        public int userStory
        {
            get { return _userStory; }
            set
            {
                _userStory = value;
                OnPropertyChanged("userStory");
            }
        }
        private Tasked _task;
        private object userStoryCollection;

        public Tasked Task
        {
            get { return _task; }
            set { _task = value; }
        }
        public async void CreateTask(Tasked task)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.forecast.it/api/v1/");

                var byteArray = Encoding.UTF8.GetBytes(_singleton.CurrentUsername + ":" + _singleton.CurrentPassword);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                //client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(byteArray));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
                //byte[] streamArray = null;
                //using (var memStream = new MemoryStream())
                //{

                //var data = new DataContractJsonSerializer(typeof(Tasked));
                //data.WriteObject(memStream,task);
                //memStream.Position = 0;
                //streamArray = memStream.ToArray();
                //string json = Encoding.UTF8.GetString(streamArray, 0, streamArray.Length);
                string json = JsonConvert.SerializeObject(task);
                var contentToPost = new StringContent(json, Encoding.UTF8, "application/json");
                //var contentToPost = new StreamContent(memStream);
                contentToPost.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                //contentToPost.Headers.ContentEncoding.Add("gzip");

                try
                {
                    var response = await client.PostAsync("projects/712/tasks", contentToPost);
                    response.EnsureSuccessStatusCode();
                    await new MessageDialog("New Task Added Successfully").ShowAsync();

                }
                catch (Exception e)
                {
                    new MessageDialog(e.Message).ShowAsync();
                }
            }
        }
    }
}

