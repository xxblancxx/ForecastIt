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
using Forecast.it.ViewModel;

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

            var projectdeatils= new ProjectViewModel();
            var projectstage = projectdeatils.Project.projectStatus;
            var projectowner = projectdeatils.Project.projectOwner;
            var projectestimator = projectdeatils.Project.projectEstimator;
            var projectmanager = projectdeatils.Project.projectManager;

           



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
                    this.Frame.Navigate(typeof (ChooseProjectPage));

                    //
                    break;

                case "task":

                    break;
                case "project":
                    //
                    break;
            }
        }

        private void OnFlyoutSetingButtonClicked(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem settingitem = sender as MenuFlyoutItem;

            switch (settingitem.Name)
            {
                case "projectsetting":
                    //
                    break;

                case "administration":
                    break;
                case "usersetting":
                    //
                    break;
                case "support":
                    break;
                case "aboutforecast":
                    break;
                case "logout":
                    break;


            }
        }

    }
}
