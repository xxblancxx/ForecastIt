using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecast.it.Model
{
    public class Project
    {
       public int id { get; set; }
        public string projectIdString { get; set; }
        public string name { get; set; }
        public string projectStatus { get; set; }
        public int projectType { get; set; }
        public int businessUnit { get; set; }
        public int customer { get; set; }
        public int projectOwner { get; set; }
        public int projectManager { get; set; }
        public int projectEstimator { get; set; }
        public int parentProject { get; set; }
        public DateTime startDate { get; set; }
        public string useCost { get; set; }
        public bool excludeFromStatistics { get; set; }
        public bool useALA { get; set; }
        public IList<int> tags { get; set; }
    }
}
