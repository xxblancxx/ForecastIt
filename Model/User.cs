﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecast.it.Model
{
   public class User
    {
       public string url { get; set; }
       public int id { get; set; }
       public string firstName { get; set; }
       public string lastName { get; set; }
       public string initials { get; set; }
       public string email { get; set; }
       public DateTime dateCreated { get; set; }
       public DateTime lastUpdated { get; set; }
       public bool isAdmin { get; set; }
       public bool active { get; set; }
       public int defaulRrole { get; set; }
       public string externalEmployeeId { get; set; }
       public string startPage { get; set; }
       public object integrationTimelogId { get; set; }

       public User()
       {
           
       }

       public User(string url, int id, string firstName, string lastName, string initials, string email, DateTime dateCreated, DateTime lastUpdated, bool isAdmin, bool active, int defaulRrole, string externalEmployeeId, string startPage, int integrationTimelogId, string userEmail)
       {
           this.url = url;
           this.id = id;
           this.firstName = firstName;
           this.lastName = lastName;
           this.initials = initials;
           this.email = email;
           this.dateCreated = dateCreated;
           this.lastUpdated = lastUpdated;
           this.isAdmin = isAdmin;
           this.active = active;
           this.defaulRrole = defaulRrole;
           this.externalEmployeeId = externalEmployeeId;
           this.startPage = startPage;
           this.integrationTimelogId = integrationTimelogId;
           
       }

       public override string ToString()
       {
           return $"url: {url}, id: {id}, firstName: {firstName}, lastName: {lastName}, initials: {initials}, email: {email}, dateCreated: {dateCreated}, lastUpdated: {lastUpdated}, isAdmin: {isAdmin}, active: {active}, defaulRrole: {defaulRrole}, externalEmployeeId: {externalEmployeeId}, startPage: {startPage}, integrationTimelogId: {integrationTimelogId}";
       }

       
    }
}
