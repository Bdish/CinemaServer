using CinemaDomain.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;

using Web.ViewData;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string js = "{\"Name\":\"sdfsd\",\"Start\":\"2018-10-08 23:30\"}";
             js=js.Replace("{\"","").Replace("\"}", "");

            string[] filter = { "\",\"", "\":\"" };

            string[] words = js.Split(filter,StringSplitOptions.None);

            Dictionary<string, string> list = new Dictionary<string, string>();
            for(int i=0;i< words.Length / 2; i++)
            {
               
                list.Add(words[i * 2], words[i * 2 + 1]);
            }

           

            Seance seance = new Seance();
            seance.Name = list["Name"];
            seance.Start = DateTime.ParseExact(list["Start"].Replace(" ", ""), "yyyy-MM-ddHH:mm", System.Globalization.CultureInfo.InvariantCulture);


            Console.WriteLine("Hello World!");
        }
    }

    public class DD
    {
        public string Name { get; set; }
        [JsonConverter( typeof(DateTimeConverter))]
        public  DateTime? date { get; set; }
    }

   
}
