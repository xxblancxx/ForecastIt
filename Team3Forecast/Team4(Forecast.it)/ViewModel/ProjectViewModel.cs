﻿using System;
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
using Forecast.it.Infrastructure;
using Forecast.it.Model;

namespace Forecast.it.ViewModel
{
    public class ProjectViewModel : INotifyPropertyChanged
    {
        private static int pid = (App.Current as App).project_id;


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

        public ProjectViewModel()
        {
            //NewOrders = new RoutedCommand(NewOrder);
           // AddOrder = new RoutedCommand(CreateOrder);
            // UpdateOrder = new RoutedCommand(EditOrder);
            //DeleteOrder = new RoutedCommand(RemoveOrder);
            
            LoadOrders();
            Project = new Project();
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
                //27578cb2-8b15-417b-9b42-36ca8922f92c
                client.BaseAddress = new Uri((App.Current as App).BaseAddress);
                var byteArray =Encoding.UTF8.GetBytes((App.Current as App).username + ":" + (App.Current as App).password);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("projects/").Result;

                try
                {
                    response.EnsureSuccessStatusCode();
                    using (var responseStream = await response.Content.ReadAsStreamAsync())
                    {
                        var ordersList = new DataContractJsonSerializer(typeof(List<Project>));
                        Projects = new ObservableCollection<Project>((IEnumerable<Project>)ordersList.ReadObject(responseStream));
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
