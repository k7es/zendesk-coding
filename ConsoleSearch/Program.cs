using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace ConsoleSearch
{
    public class Program
    {
        public static Search<User> UsersSearch;
        public static Search<Ticket> TicketsSearch;
        public static Search<Organization> OrganizationsSearch;

        public static bool Running;
        
        public static void Main(string[] args)
        {            
            Running = true;
            LoadData();
            Console.WriteLine("Welcome to Zendesk Search.");            
            while(Running)
            {
                Console.WriteLine("Options:\no) Organizations u) Users t) Tickets.\nWhat would you like to search for?");
                Console.WriteLine("Type 'exit' to quit the program or 'help' for more options");   
                var option = Console.ReadLine();
                switch (option.ToLower())
                {             
                    case "t":   
                        SearchItems(TicketsSearch);                 
                        break;
                    case "u":
                        SearchItems(UsersSearch);
                        break;
                    case "o":
                        SearchItems(OrganizationsSearch);
                        break;  
                    case "exit":
                        Running = false;
                        break;                   
                    default:
                        Console.WriteLine("That is not a valid option.");
                        break;
                }
            }  
        }

        public static void SearchItems<T>(Search<T> items)
        {            
            Console.WriteLine("What would you like to search by?\nType 'list' to list all searchable fields.");
            var option = Console.ReadLine().ToLower();
            var searchField = char.ToUpper(option[0]) + option.Substring(1).ToLower();
            switch (option)
            {
                case "list":
                    items.ListProperties();
                    break;
                case "exit":
                    Running = false;
                    break;
                default:
                    if(!items.IsValidProperty(searchField) || String.IsNullOrEmpty(searchField) || String.IsNullOrWhiteSpace(searchField))
                    {
                        Console.WriteLine("\"" + searchField + "\" is not a valid search field.");
                        break;
                    }
                    Console.WriteLine("Enter search value: ");
                    var searchValue = Console.ReadLine();
                    var matches = new List<T>();
                    switch (searchField)
                    {
                        case "Id":
                            matches = items.GetMatchById(searchValue);
                            break;
                        case "Tags":
                            matches = items.GetMatchesByTags(searchValue);
                            break;
                        default:
                            matches = items.GetMatches(searchField, searchValue);
                            break;
                    }                    
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine(String.Format("Found {0} results", matches.Count));   
                    if(!matches.Any())
                    {                        
                        break;                     
                    }                                               
                    foreach(var match in matches)
                    {                        
                        Console.WriteLine("------------------------------------------");
                        Console.WriteLine(match.ToString());                        
                    }                      
                    Console.WriteLine("------------------------------------------");
                    break;
            }
        }

        public static void LoadData()
        {                        
            using (StreamReader reader = new StreamReader("data/users.json"))
            {
                string json = reader.ReadToEnd();
                UsersSearch = new Search<User>(JsonConvert.DeserializeObject<List<User>>(json));
            }
            using (StreamReader reader = new StreamReader("data/tickets.json"))
            {
                string json = reader.ReadToEnd();
                TicketsSearch = new Search<Ticket>(JsonConvert.DeserializeObject<List<Ticket>>(json));
            }
            using (StreamReader reader = new StreamReader("data/organizations.json"))
            {
                string json = reader.ReadToEnd();
                OrganizationsSearch = new Search<Organization>(JsonConvert.DeserializeObject<List<Organization>>(json));                                        
            }

            var users = UsersSearch.Items.Values.ToList();            
            var tickets = TicketsSearch.Items.Values.ToList();
            var organizations = OrganizationsSearch.Items.Values.ToList();

            foreach(User user in users)
            {                
                user.SubmittedTickets = new List<string>();
                user.AssignedTickets = new List<string>();
                user.InitialiseTickets(tickets);
                user.InitialiseOrganisation(organizations);
            }
            foreach(Ticket ticket in tickets)
            {
                ticket.InitialiseUsers(users);
                ticket.InitialiseOrganisation(organizations);                
            }
            foreach(Organization organization in organizations)
            {                
                organization.Tickets = new List<string>();
                organization.Users = new List<string>();
                organization.AddTickets(tickets);
                organization.AddUsers(users);
            }
        }
    }
}
