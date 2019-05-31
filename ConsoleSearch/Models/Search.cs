using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ConsoleSearch
{
    public class Search<T>
    { 
        internal Dictionary<string, T> Items { get; }   
          
        public Search(List<T> list)
        {
            Items = new Dictionary<string, T>();
            foreach(var item in list)
            {
                var type = item.GetType();                               
                Items.Add((string) type.GetProperty("Id").GetValue(item).ToString(), item);
            }
        }
        public bool IsValidProperty(string propertyName)
        {
            return typeof(T).GetProperty(propertyName,           
                                            BindingFlags.Public
                                            | BindingFlags.Instance 
                                            | BindingFlags.IgnoreCase) != null; 
        }
        public List<T> GetMatches(string searchProperty, string searchValue)
        {
            if (!IsValidProperty(searchProperty)) return new List<T>();

            return Items.Select(item => item.Value).
            Where(item => (item.GetType().GetProperty(searchProperty)
            .GetValue(item) ?? string.Empty).ToString().ToLower() == searchValue.ToLower())
            .ToList();
        } 
        public List<T> GetMatchById(string id)
        {
            if(Items.TryGetValue(id, out T match))
            {
                return new List<T> { match };
            }
            else
            {
                return new List<T>();
            }

        } 
        public List<T> GetMatchesByTags(string tag)
        {
            return Items.Select(item => item.Value).
            Where(item => ((List<string>)item.GetType().GetProperty("Tags").GetValue(item))
            .Contains(tag, StringComparer.OrdinalIgnoreCase))
            .ToList();
        }        
        public void ListProperties()
        {
            foreach (var prop in typeof(T).GetProperties())
            {
                Console.WriteLine(prop.Name);
            }
        }
    }
}