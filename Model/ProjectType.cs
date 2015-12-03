using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecast.it.Model
{
    public class ProjectType
    {
        public string url { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string developmentModel { get; set; }
        public int sprintLength { get; set; }
        public bool cocomoEnabled { get; set; }
        public bool versionEnabled { get; set; }
    }
}
