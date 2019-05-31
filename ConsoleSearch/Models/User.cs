using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System.Linq;

namespace ConsoleSearch
{
    public class User : SearchItem
    {
        [JsonProperty("_id")] 
        public int Id { get; set; }
        public string Url { get; set; }
        [JsonProperty("external_id")] 
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        [JsonProperty("created_at")] 
        public string CreatedAt { get; set; }
        public string Active { get; set; }
        public string Verified { get; set; }
        public string Shared { get; set; }
        public string Locale { get; set; }
        public string Timezone { get; set; }
        [JsonProperty("last_login_at")] 
        public string LastLoginAt { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Signature { get; set; }
        [JsonProperty("organization_id")] 
        public int OrganizationId { get; set; }
        public List<string> Tags { get; set; }
        public string Suspended { get; set; }
        public string Role { get; set; }  
        public List<string> SubmittedTickets { get; set; }  
        public List<string> AssignedTickets { get; set; }  
        public string Organization { get; set; }  
        public void InitialiseOrganisation(List<Organization> organizations)
        {
            Organization = organizations
                            .Where(organization => organization.Id == OrganizationId)
                            .Select(organization => organization.Name).FirstOrDefault();
        }
        public void InitialiseTickets(List<Ticket> tickets)
        {
            var submittedTicketList = tickets.Where(ticket => ticket.SubmitterId == Id).ToList();

            foreach(var ticket in submittedTicketList)
            {
                SubmittedTickets.Add(ticket.Subject);
            }

            var assignedTicketList = tickets.Where(ticket => ticket.AssigneeId == Id).ToList();
            AssignedTickets = new List<string>();
            foreach (var ticket in assignedTicketList)
            {
                AssignedTickets.Add(ticket.Subject);
            }
        }
    }  
}