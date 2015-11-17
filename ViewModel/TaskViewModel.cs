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

namespace Forecast.it.ViewModel
{
    class TaskViewModel:INotifyPropertyChanged
    {
        
         private static int pid = (App.Current as App).project_id;
            public TaskViewModel()
            {

                AddTask = new RoutedCommand(CreateTasked);

            }

            private void CreateTasked(object obj)
            {
                Task = new Model.Task();
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
            private Model.Task _task;


            public Model.Task Task
            {
                get { return _task; }
                set { _task = value; }
            }




            public async void CreateTask(Model.Task task)
            {


                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://api.forecast.it/api/v1/");

                    var byteArray = Encoding.UTF8.GetBytes((App.Current as App).username + ":" + (App.Current as App).password);
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
                        var response = await client.PostAsync("projects/"+pid+"/tasks", contentToPost);
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

