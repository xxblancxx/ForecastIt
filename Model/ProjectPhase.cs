using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecast.it.Model
{
    public class ProjectPhase
    {
        public string url { get; set; }
        public int id { get; set; }
        public string phase { get; set; }
        public string description { get; set; }
        public int weight { get; set; }
        public int calculatedWeight { get; set; }
        public int order { get; set; }

        public ProjectPhase()
        {
            
        }
    }
}
