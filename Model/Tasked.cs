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
   public class Tasked
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

       public Tasked()
       {
           
       }

       public Tasked(string url, int id, string title, string description, int estimate, int timeLeft, int projectPhase, int status, object waterfallStatus, IList<int> owners, int userStory, DateTime deadline, IList<int> tags, bool integrationTimelogTask, object integrationTfsId, object integrationTimelogId, object integrationTimelogGuid, DateTime modifiedOn, int modifiedBy, DateTime createdOn, int createdBy)
       {
           this.url = url;
           this.id = id;
           this.title = title;
           this.description = description;
           this.estimate = estimate;
           this.timeLeft = timeLeft;
           this.projectPhase = projectPhase;
           this.status = status;
           this.waterfallStatus = waterfallStatus;
           this.owners = owners;
           this.userStory = userStory;
           this.deadline = deadline;
           this.tags = tags;
           this.integrationTimelogTask = integrationTimelogTask;
           this.integrationTfsId = integrationTfsId;
           this.integrationTimelogId = integrationTimelogId;
           this.integrationTimelogGuid = integrationTimelogGuid;
           this.modifiedOn = modifiedOn;
           this.modifiedBy = modifiedBy;
           this.createdOn = createdOn;
           this.createdBy = createdBy;
       }

       public override string ToString()
       {
           return $"createdBy: {createdBy}, createdOn: {createdOn}, deadline: {deadline}, description: {description}, estimate: {estimate}, id: {id}, integrationTfsId: {integrationTfsId}, integrationTimelogGuid: {integrationTimelogGuid}, integrationTimelogId: {integrationTimelogId}, integrationTimelogTask: {integrationTimelogTask}, modifiedBy: {modifiedBy}, modifiedOn: {modifiedOn}, owners: {owners}, projectPhase: {projectPhase}, status: {status}, tags: {tags}, timeLeft: {timeLeft}, title: {title}, url: {url}, userStory: {userStory}, waterfallStatus: {waterfallStatus}";
       }
    }
}
