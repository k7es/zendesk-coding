using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System.Linq;

namespace ConsoleSearch
{  
    public class Ticket : SearchItem
    {
        [JsonProperty("_id")] 
        public string Id { get; set; }
        public string Url { get; set; }
        [JsonProperty("external_id")] 
        public string ExternalId { get; set; }
        [JsonProperty("created_at")] 
        public string CreatedAt { get; set; }        
        public string Type { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        [JsonProperty("submitter_id")] 
        public int SubmitterId { get; set; }

        public string Submitter { get; set; }
        [JsonProperty("assignee_id")] 
        public int AssigneeId { get; set; }
        public string AssgnedTo { get; set; }
        [JsonProperty("organization_id")] 
        public int OrganizationId { get; set; }
        public string Organization { get; set; }
        public List<string> Tags { get; set; }
        [JsonProperty("has_incidents")] 
        public string HasIncidents { get; set; }
        [JsonProperty("due_at")] 
        public DateTime DueAt { get; set; }
        public string Via { get; set; }
        public void InitialiseOrganisation(List<Organization> organizations)
        {
            Organization = organizations
                            .Where(organization => organization.Id == OrganizationId)
                            .Select(organization => organization.Name).FirstOrDefault();
        }
        public void InitialiseUsers(List<User> users)
        {
            AssgnedTo = users.Where(user => user.Id == AssigneeId)
                            .Select(user => user.Name).FirstOrDefault();
            Submitter = users.Where(user => user.Id == SubmitterId)
                           .Select(user => user.Name).FirstOrDefault();
        }                   
    }
}