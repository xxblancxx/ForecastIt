using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForecastModel.Connection;
using Newtonsoft.Json.Converters;

namespace ForecastModel
{
    public static class EndPointConverter
    {

        public static string Converter(EndPoints endpoint)
        {
            var Url = "";

            switch (endpoint)
            {
                case EndPoints.Users:
                    Url = "users/";
                    break;
                case EndPoints.Roles:
                    Url = "roles/";
                    break;
                case EndPoints.Teams:
                    Url = "teams/";
                    break;
                case EndPoints.Customers:
                    Url = "customers/";
                    break;
                case EndPoints.BusinessUnits:
                    Url = "businessunits/";
                    break;
                case EndPoints.Projects:
                    Url = "projects/";
                    break;
                case EndPoints.ProjectPhase:
                    Url = "projects/{0}/projectPhase/";
                    break;
                case EndPoints.ProjectTypes:
                    Url = "projectTypes/";
                    break;
                case EndPoints.ProjectPeopleResources:
                    Url = "projects/{0}/projectResources/";
                    break;
                case EndPoints.CostTypes:
                    Url = "projects/{0}/costTypes/";
                    break;
                case EndPoints.UserStories:
                    Url = "projects/{0}/userStories/";
                    break;
                case EndPoints.UserStoryTypes:
                    Url = "userStoryTypes/";
                    break;
                case EndPoints.Sprints:
                    Url = "projects/{0}/sprints/";
                    break;
                case EndPoints.Tasks:
                    Url = "projects/{0}/tasks/";
                    break;
                case EndPoints.TimeTrackingRegistration:
                    Url = "projects/{0}/time/";
                    break;
                case EndPoints.ScrumStages:
                    Url = "projects/{0}/scrumStages/";
                    break;
                case EndPoints.Tags:
                    Url = "tags/";
                    break;
                case EndPoints.FileAttachments:
                    throw new NotImplementedException("FileAttachments at this moment isn't implemented for API Calls yet");
                case EndPoints.Milestones:
                    throw new NotImplementedException("Milestones at this moment isn't implemented for API Calls yet");
                case EndPoints.Versions:
                    throw new NotImplementedException("Milestones at this moment isn't implemented for API Calls yet");

                default:
                    throw new ArgumentException("Apicall hasn't been set to anything, this is your Fault");
            }
            return Url;
        }
    }
}
