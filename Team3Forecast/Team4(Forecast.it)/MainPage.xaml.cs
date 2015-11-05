using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641
using Forecast.it.View;
using Forecast.it.ViewModel;

namespace Forecast.it
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //(App.Current as App).username = txt_name.Text;
            //(App.Current as App).password = txt_name.Password;


            using (var client = new HttpClient())
                 {
                    
                    client.BaseAddress = new Uri((App.Current as App).BaseAddress);

                    var byteArray = Encoding.UTF8.GetBytes((App.Current as App).username + ":" + (App.Current as App).password);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                    //client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(byteArray));
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.GetAsync("users").Result;


                     if (response.IsSuccessStatusCode)
                     {
                         this.Frame.Navigate(typeof (MainPage));

                     }
                     else
                     {
                         var msg=new MessageDialog("Wrong Username or Password");
                         msg.ShowAsync();
                     }

                 }

                 }

      

        //private void Txt_name_OnGotFocus_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    txt_name.Text = "";

        //    // throw new NotImplementedException();
        //}

        //private void Txt_pass_OnGotFocus_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    txt_pass.Password = "";


        //    // throw new NotImplementedException();
        //}

        
    }
}
