using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System.Linq;

namespace ConsoleSearch
{
    public class Organization : SearchItem
    {
        [JsonProperty("_id")] 
        public int Id { get; set; }
        public string Url { get; set; }
        [JsonProperty("external_id")] 
        public string ExternalId { get; set; }
        public string Name { get; set; }
        [JsonProperty("domain_names")] 
        public List<string> DomainNames { get; set; }
        [JsonProperty("created_at")] 
        public string CreatedAt { get; set; }
        public string Details { get; set; }
        [JsonProperty("shared_tickets")] 
        public string SharedTickets { get; set; }
        public List<string> Tags { get; set; }
        public List<string> Users { get; set; }
        public List<string> Tickets {get; set;}        
        public void AddUsers(List<User> users)
        {
            var userList = users.Where(user => user.OrganizationId == Id).ToList();
            foreach(var user in userList)
            {
                Users.Add(user.Name);
            }
        }
        public void AddTickets(List<Ticket> tickets)
        {
            var ticketList = tickets.Where(ticket => ticket.OrganizationId == Id).ToList();
            foreach(var ticket in ticketList)
            {
                Tickets.Add(ticket.Subject);
            }
        }       
    }
}