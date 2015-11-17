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
using Windows.UI.Xaml.Controls;
using Forecast.it.Common;
using Forecast.it.View;
using ForecastModel;
using ForecastModel.Connection;
using Microsoft.Xaml.Interactions.Core;
using Newtonsoft.Json;

namespace Forecast.it.Model
{
    public class Requester
    {
        private string _username;
        private string _password;
        private readonly string _baseAddress = "https://api.forecast.it/api/v1/";
        private static SingletonCommon _singleton = SingletonCommon.SingletonInstance;
        private static Requester _instance;

       

        public Requester()
        {
            _username = _singleton.CurrentUsername;
            _password = _singleton.CurrentPassword;
        }

        public bool LogIn()
        {
            using (var handler = new HttpClientHandler())
            {
                handler.Credentials = new NetworkCredential(_singleton.CurrentUsername, _singleton.CurrentPassword);
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
                            _singleton.CurrentPageView.Frame.Navigate(typeof(ProjectListPage));

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

        /// <summary>
        /// Get request Asynchronously
        /// </summary>
        /// <typeparam name="T"> The class that the Method Should return and Get</typeparam>
        /// <param name="theUri">the specific api Call</param>
        /// <param name="id1">this is the first Id, it can be left as a zero but for some Calls cant be left </param>
        /// <param name="id2">this is the second Id, for specific subjects</param>
        /// <returns>A List of T</returns>
        public T GetRequestByIdAsync<T>(EndPoints theUri, int id1 = 0, int id2 = 0) where T : new()
        {
            using (var handler = new HttpClientHandler())
            {
                handler.Credentials = new NetworkCredential(_username, _password);
                using (var client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri(_baseAddress);
                    client.DefaultRequestHeaders.Clear();//making sure that there is no old data within headers
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var jsonData = string.Empty;
                    // attempt to download JSON data as a string
                    try
                    {
                        var ext = UriCorrector(theUri, id1, id2);

                        var response = client.GetAsync(ext).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            jsonData = response.Content.ReadAsStringAsync().Result;
                        }
                    }
                    catch (HttpRequestException exception)
                    {
                        //await new MessageDialog(exception.Message).ShowAsync();

                    }
                    // if string with JSON data is not empty, deserialize it to class and return its instance 
                    return !string.IsNullOrEmpty(jsonData) ? JsonConvert.DeserializeObject<T>(jsonData) : new T();
                }
            }

        }

        /// <summary>
        /// Get request Asynchronously
        /// </summary>
        /// <typeparam name="T"> The class that the Method Should return and Get</typeparam>
        /// <param name="theUri">the specific api Call</param>
        /// <param name="id1">this is the first Id, it can be left as a zero but for some Calls cant be left </param>
        /// <param name="id2">this is the second Id, for specific subjects</param>
        /// <returns>A List of T</returns>
        public async Task<List<T>> GetRequestAsync<T>(EndPoints theUri, int id1 = 0, int id2 = 0)
        {
            using (var handler = new HttpClientHandler())
            {
                handler.Credentials = new NetworkCredential(_username, _password);
                using (var client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri(_baseAddress);
                    client.DefaultRequestHeaders.Clear();//making sure that there is no old data within headers
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var jsonData = string.Empty;
                    // attempt to download JSON data as a string
                    try
                    {
                        var ext = UriCorrector(theUri, id1, id2);

                        var response = client.GetAsync(ext).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            jsonData = await response.Content.ReadAsStringAsync();
                        }
                    }
                    catch (HttpRequestException exception)
                    {
                        //await new MessageDialog(exception.Message).ShowAsync();

                    }
                    // if string with JSON data is not empty, deserialize it to class and return its instance 
                    return JsonConvert.DeserializeObject<List<T>>(jsonData);
                }
            }
        }

        /// <summary>
        /// Create request Asynchronously
        /// </summary>
        /// <typeparam name="T"> The class that the Method Should return and Get</typeparam>
        /// <param name="classToPost"> class wished to retrieve</param>
        /// <param name="theUri">the specific api Call</param>
        /// <param name="id1">this is the first Id, it can be left as a zero but for some Calls cant be left </param>
        /// <param name="id2">this is the second Id, for specific subjects</param>
        /// <returns>returns T that is sent to the API for debugging reasons</returns>
        public async Task<T> PostRequest<T>(T classToPost, EndPoints theUri, int id1 = 0, int id2 = 0)
        {
            using (var handler = new HttpClientHandler())
            {
                handler.Credentials = new NetworkCredential(_username, _password);
                using (var client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri(_baseAddress);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    try
                    {
                        string serializeObjectToJson = await System.Threading.Tasks.Task.Run(() => JsonConvert.SerializeObject(classToPost));

                        var ext = UriCorrector(theUri, id1, id2);


                        var response = await client.PostAsync(ext, new StringContent(serializeObjectToJson, Encoding.UTF8, "application/json"));
                        if (response.IsSuccessStatusCode)
                        {
                            //await new MessageDialog("Succesfully Created:\n\t"+classToPost).ShowAsync();
                        }
                    }
                    catch (HttpRequestException exception)
                    {
                        //await new MessageDialog(exception.Message).ShowAsync();
                    }

                    return classToPost;
                }
            }
        }


        /// <summary>
        /// Change/edit request Asynchronously
        /// </summary>
        /// <typeparam name="T"> The class that the Method Should return and Get</typeparam>
        /// <param name="classToPut"> class wished to retrieve</param>
        /// <param name="theUri">the specific api Call</param>
        /// <param name="id1">this is the first Id, it can be left as a zero but for some Calls cant be left </param>
        /// <param name="id2">this is the second Id, for specific subjects</param>
        /// <returns>returns T that is sent to the API for debugging reasons</returns>
        public async Task<T> PutRequest<T>(T classToPut, EndPoints theUri, int id1 = 0, int id2 = 0)
        {
            using (var handler = new HttpClientHandler())
            {
                handler.Credentials = new NetworkCredential(_username, _password);
                using (var client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri(_baseAddress);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    try
                    {
                        string serializeObjectToJson = await System.Threading.Tasks.Task.Run(() => JsonConvert.SerializeObject(classToPut));

                        var ext = UriCorrector(theUri, id1, id2);

                        var response = await client.PutAsync(ext, new StringContent(serializeObjectToJson, Encoding.UTF8, "application/json"));
                        if (response.IsSuccessStatusCode)
                        {
                            //await new MessageDialog("Succesfully Changed:\n\t" + classToPut).ShowAsync();
                        }
                    }
                    catch (HttpRequestException exception)
                    {
                        //await new MessageDialog(exception.Message).ShowAsync();
                    }

                    return classToPut;
                }
            }
        }

        /// <summary>
        /// Delete Request Asynchronously
        /// </summary>
        /// <param name="theUri">the specific api Call</param>
        /// <param name="id1">this is the first Id, it can be left as a zero but for some Calls it is needed</param>
        /// <param name="id2">this is the second Id, for specific subjects</param>
        public async void DelRequest(EndPoints theUri, int id1 = 0, int id2 = 0)
        {
            using (var handler = new HttpClientHandler())
            {
                handler.Credentials = new NetworkCredential(_username, _password);
                using (var client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri(_baseAddress);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    try
                    {
                        var ext = UriCorrector(theUri, id1, id2);

                        var response = await client.DeleteAsync(ext);

                        if (response.IsSuccessStatusCode)
                        {
                            //await new MessageDialog("Succesfully Deleted:\n\t").ShowAsync();
                        }
                    }
                    catch (HttpRequestException exception)
                    {
                        //await new MessageDialog(exception.Message).ShowAsync();
                    }
                }
            }
        }


        /// <summary>
        /// Corrects the string/Uri gotten from EndPointsConverter and makes sure that you dont get a invalid one.
        /// </summary>
        /// <returns>returns the Correct Uri</returns>
        private string UriCorrector(EndPoints theUri, int id1, int id2)
        {
            var ext = "";
            if (id1 == 0 && id2 == 0)
            {
                ext = EndPointConverter.Converter(theUri)
                    .Replace("{0}/", "");
                return ext;
            }
            if (id1 != 0 && id2 == 0)
            {
                ext = EndPointConverter.Converter(theUri).Replace("{0}", id1.ToString());
                return ext;
            }
            if (id1 != 0 && id2 != 0)
            {
                ext = EndPointConverter.Converter(theUri).Replace("{0}", id1.ToString()) + id2.ToString();
                return ext;
            }
            throw new ArgumentException("whoops something went wrong with, UriCorrector");
        }

    }
}
