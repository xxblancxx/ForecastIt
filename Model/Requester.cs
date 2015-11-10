using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Forecast.it.Common;
using Forecast.it.View;
using Newtonsoft.Json;

namespace Forecast.it.Model
{
    class Requester
    {
        private readonly string _baseAddress = "https://api.forecast.it/api/v1/";
        private SingletonCommon _singleton = SingletonCommon.SingletonInstance;

        public bool LogIn()
        {
            using (var handler = new HttpClientHandler())
            {
                handler.Credentials = new NetworkCredential(_singleton.CurrentUsername,_singleton.CurrentPassword);
                using (var client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri(_baseAddress + "users");
                    client.DefaultRequestHeaders.Clear(); //making sure that there is no old data within headers
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var jsonData = string.Empty;
                    // attempt to download JSON data as a string
                    try
                    {
                        var response = client.GetAsync("").Result;
                        if (response.IsSuccessStatusCode)
                        {
                            _singleton.CurrentPageView.Navigate(typeof (ProjectListPage));
                            return true;
                        }
                        else
                        {
                            var msg = new MessageDialog("Wrong Username or Password");
                            msg.ShowAsync();
                            return false;
                        }
                        
                    }
                    catch (JsonException e)
                    {
                       var msg = new MessageDialog("Wrong Username or Password");
                        msg.ShowAsync();
                        return false;
                    }
                    catch (HttpRequestException)
                    {
                        var msg = new MessageDialog("Wrong Username or Password");
                        msg.ShowAsync();
                        return false;
                    }
                }
            }
        }
    }
}
