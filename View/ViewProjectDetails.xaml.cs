using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Forecast.it.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewProjectDetails : Page
    {
        public ViewProjectDetails()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
    
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void OnFlyoutButtonClicked(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = sender as MenuFlyoutItem;

            switch (item.Name)
            {
                case "userstory":
                    this.Frame.Navigate(typeof(ChooseProjectPage));

                    //
                    break;

                case "task":

                    break;
                case "project":
                    //
                    break;
            }
        }

        private void Option_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SettingPage));
        }

        private void Txt_tile_GotFocus(object sender, RoutedEventArgs e)
        {
            txt_title.Text = "";

            // throw new NotImplementedException();
        }

        private void Txt_description_GotFocus(object sender, RoutedEventArgs e)
        {
            txt_description.Text = "";

            // throw new NotImplementedException();
        }
        private void Txt_estimate_GotFocus(object sender, RoutedEventArgs e)
        {
            txt_estimate.Text = "";

            // throw new NotImplementedException();
        }


        private void Txt_epic_GotFocus(object sender, RoutedEventArgs e)
        {
            txt_epic.Text = "";

            // throw new NotImplementedException();
        }


        private void Txt_owner_GotFocus(object sender, RoutedEventArgs e)
        {
            txt_owner.Text = "";

            // throw new NotImplementedException();
        }


        private void Txt_acceptance_GotFocus(object sender, RoutedEventArgs e)
        {
            txt_acceptance.Text = "";

            // throw new NotImplementedException();
        }

        private void Txt_type_GotFocus(object sender, RoutedEventArgs e)
        {
            txt_type.Text = "";

            // throw new NotImplementedException();
        }

        private void Txt_sprint_GotFocus(object sender, RoutedEventArgs e)
        {
            txt_sprint.Text = "";

            // throw new NotImplementedException();
        }

        private void Txt_status_GotFocus(object sender, RoutedEventArgs e)
        {
            txt_status.Text = "";

            // throw new NotImplementedException();
        }
        private void Txt_tags_GotFocus(object sender, RoutedEventArgs e)
        {
            txt_tag.Text = "";

            // throw new NotImplementedException();
        }

    
    }
}
