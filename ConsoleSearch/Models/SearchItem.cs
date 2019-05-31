using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ConsoleSearch
{
    public class SearchItem
    {
        public override string ToString()
       {
            var builder = new StringBuilder();
            foreach(var property in this.GetType().GetProperties())
            {
                var value = property.GetValue(this);

                if(value is IEnumerable<string>)
                {                
                    builder.Append(String.Format("{0,-20}: {1}\n", property.Name, PrintList(value as IEnumerable<string>)));
                }
                else
                {
                    builder.Append(String.Format("{0,-20}: {1}\n", property.Name, value));
                }  
            }
            return builder.ToString();
        }
        private string PrintList(IEnumerable<string> list)
        {
            var content = string.Join(",", list.Select(i => string.Format("{0}", i)));
            return content;
        }
    }
}