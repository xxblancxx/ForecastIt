using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecast.it.Model
{
    public class Status
    {
        public string url { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string category { get; set; }
        public string description { get; set; }

        public Status()
        {
            
        }
    }

}
