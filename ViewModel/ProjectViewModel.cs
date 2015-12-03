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
using Forecast.it.View;
using ForecastModel.Connection;

namespace Forecast.it.ViewModel
{
    public class ProjectViewModel : INotifyPropertyChanged
    {
       private SingletonCommon _singleton = SingletonCommon.SingletonInstance;


        public event PropertyChangedEventHandler PropertyChanged;


        private void OnPropertyChanged(string pName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(pName));
            }
        }

        ObservableCollection<Project> _Projects;

        public ObservableCollection<Project> Projects
        {
            get { return _Projects; }
            set
            {
                _Projects = value;
                OnPropertyChanged("Projects");
            }
        }

        Project _Project;

        public Project Project
        {
            get { return _Project; }
            set
            {
                _Project = value;
                if (Project != null)
                {
                    id = Project.id;
                }
                OnPropertyChanged("Project");
            }
        }

        Project _SelectedOrder;

        public Project SelectedOrder
        {
            get { return _SelectedOrder; }
            set
            {
                _SelectedOrder = value;
                Project = _SelectedOrder;
                OnPropertyChanged("Project");
            }
        }

        int _ProjectId = 0;

        public int id
        {
            get { return _ProjectId; }
            set
            {
                _ProjectId = value;
                OnPropertyChanged("id");
            }
        }

        //Command objects
        public ICommand NewOrders { get; private set; }
        public ICommand AddOrder { get; private set; }

        public ProjectViewModel()
        {
            AddOrder = new RoutedCommand(CreateOrder);
            Project = new Project();
            LoadOrders();

           
        }
        private RelayCommand _createUserCommand;

        public RelayCommand CreateUserCommand
        {
            get
            {
                _createUserCommand = new RelayCommand(postproject);
                return _createUserCommand;
            }
            private set { }
        }
        private async void LoadOrders()
        {

            using (var client = new HttpClient())
            {
                //27578cb2-8b15-417b-9b42-36ca8922f92c
                client.BaseAddress = new Uri((App.Current as App).BaseAddress);
                var byteArray =
                    Encoding.UTF8.GetBytes(_singleton.CurrentUsername +":"+_singleton.CurrentPassword);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(byteArray));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("projects/").Result;

                try
                {
                    response.EnsureSuccessStatusCode();
                    using (var responseStream = await response.Content.ReadAsStreamAsync())
                    {
                        var ordersList = new DataContractJsonSerializer(typeof (List<Project>));
                        Projects =
                            new ObservableCollection<Project>(
                                (IEnumerable<Project>) ordersList.ReadObject(responseStream));
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

        public async void LoadOrderswithid(int passedid)
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

                HttpResponseMessage response = client.GetAsync("projects/"+passedid).Result;

                try
                {
                    response.EnsureSuccessStatusCode();
                    using (var responseStream = await response.Content.ReadAsStreamAsync())
                    {
                        var ordersList = new DataContractJsonSerializer(typeof(List<Project>));
                        Projects =
                            new ObservableCollection<Project>(
                                (IEnumerable<Project>)ordersList.ReadObject(responseStream));
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

        async void CreateOrder(object o)
        {

            

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri((App.Current as App).BaseAddress);

                var byteArray = Encoding.UTF8.GetBytes(_singleton.CurrentUsername + ":" + _singleton.CurrentPassword);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

              client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (var memStream = new MemoryStream())
                {
                    var data = new DataContractJsonSerializer(typeof(Project));
                    data.WriteObject(memStream, Project);
                    memStream.Position = 0;
                    var contentToPost = new StreamContent(memStream);
                    contentToPost.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    try
                    {
                        var response = await client.PostAsync("projects", contentToPost);
                        response.EnsureSuccessStatusCode();
                        await new MessageDialog("New Project Created Successfully").ShowAsync();
                        _singleton.CurrentPageView.Frame.Navigate(typeof(ProjectListPage));


                    }
                    catch (HttpRequestException e)
                    {
                        await new MessageDialog(e.Message).ShowAsync();


                    }
                }
            }


        }

        public void postproject()
        {
            var req = new Requester();



            Project newProject = Project;
            req.PostRequest(newProject, EndPoints.Projects);

        }

    }
}
