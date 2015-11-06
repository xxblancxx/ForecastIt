using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecast.it.Common
{
    class SingletonCommon
    {
        public static SingletonCommon SingletonInstance { get; private set; }
        public string CurrentUsername { get; set; }
        public string CurrentPassword { get; set; }
        
        //private constructor
        private SingletonCommon(){}
    }
}
