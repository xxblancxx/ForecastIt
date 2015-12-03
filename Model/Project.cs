using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Forecast.it.Annotations;

namespace Forecast.it.Model
{
    public class Project:INotifyPropertyChanged
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
       public DateTime startDate { get; set; }
        public string useCost { get; set; }
        public bool excludeFromStatistics { get; set; }
        public bool useALA { get; set; }
        public IList<int> tags { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
