using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Forecast.it.Common
{
    class SingletonCommon
    {
        private static SingletonCommon _singletonInstance = new SingletonCommon();

        public static SingletonCommon SingletonInstance
        {
            get { return _singletonInstance; }
            private set { _singletonInstance = value; }
        }

        public string CurrentUsername { get; set; }
        public string CurrentPassword { get; set; }
        public Frame CurrentPageView { get; set; }
        
        //private constructor
        private SingletonCommon(){}
    }
}
