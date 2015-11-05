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
   public class Tasked:INotifyPropertyChanged
    {
        public string url { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int estimate { get; set; }
        public int timeLeft { get; set; }
        public int projectPhase { get; set; }
        public int status { get; set; }
        public object waterfallStatus { get; set; }
        public IList<int> owners { get; set; }
        public int userStory { get; set; }
        public DateTime deadline { get; set; }
        public IList<int> tags { get; set; }
        public bool integrationTimelogTask { get; set; }
        public object integrationTfsId { get; set; }
        public object integrationTimelogId { get; set; }
        public object integrationTimelogGuid { get; set; }
        public DateTime modifiedOn { get; set; }
        public int modifiedBy { get; set; }
        public DateTime createdOn { get; set; }
        public int createdBy { get; set; }

       public event PropertyChangedEventHandler PropertyChanged;

       [NotifyPropertyChangedInvocator]
       protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
       {
           PropertyChangedEventHandler handler = PropertyChanged;
           if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
       }
       protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
