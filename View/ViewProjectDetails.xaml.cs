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
using Forecast.it.Annotations;
using Forecast.it.Common;
using Forecast.it.Model;
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
            
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This Parameter is typically used to configure the page.</param>
    
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Project prjProject = e.Parameter as Project;
            if (prjProject != null)

            {
                pname.Text = prjProject.name;
                pstage.Text = prjProject.projectStatus;
                powner.Text = prjProject.projectOwner.ToString();
                pmanager.Text = prjProject.projectManager.ToString();
                pplanner.Text = prjProject.projectEstimator.ToString();

            }
        }

        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
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
