using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecast.it.Model
{
   public class User
    {
      
    
       public string User_Email { get; set; }
       

       public override string ToString()
       {
           return string.Format("{0}", User_Email);
       }
    }
}
