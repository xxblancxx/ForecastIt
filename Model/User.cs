using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecast.it.Model
{
   public class User
    {


      
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string initials { get; set; }
        public string email { get; set; }
        public DateTime dateCreated { get; set; }
        public DateTime lastUpdated { get; set; }
        public bool isAdmin { get; set; }
        public bool active { get; set; }
        public int defaultRole { get; set; }
        public int externalEmployeeId { get; set; }


       
    }
}
