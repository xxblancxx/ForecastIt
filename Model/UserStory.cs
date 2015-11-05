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
   public class UserStory:INotifyPropertyChanged
    {
       public event PropertyChangedEventHandler PropertyChanged;

       [NotifyPropertyChangedInvocator]
       protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
       {
           PropertyChangedEventHandler handler = PropertyChanged;
           if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
       }

       public int id { get; set; }
       public string title { get; set; }
       public string description { get; set; }
       public int type { get; set; }
       public string acceptanceCriteria { get; set; }
       public int estimate { get; set; }
       public string epic { get; set; }
       public int sprint { get; set; }
       public int status { get; set; }
     
       public List<int> owners { get; set; }
       public List<int> tags { get; set; }
       
       
       
    }
}
