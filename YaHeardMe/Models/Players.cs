using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace YaHeardMe.Models
{

    public class Players
    {

        public class Rootobject
        {
            public Api api { get; set; }
        }

        public class Api
        {
            [JsonProperty("status")]
            public int status { get; set; }

            [JsonProperty("message")]
            public string message { get; set; }

            [JsonProperty("results")]
            public int results { get; set; }

            [JsonProperty("filters")]
            public string[] filters { get; set; }

            [JsonProperty("players")]
            public Player[] players { get; set; }
        }

        public class Player
        {
            [JsonProperty("firstName")]
            public string firstName { get; set; }

            [JsonProperty("lastName")]
            public string lastName { get; set; }

            [JsonProperty("TeamId")]
            public string TeamId { get; set; }

            [JsonProperty("yearsPro")]
            public string yearsPro { get; set; }

            [JsonProperty("collegeName")]
            public string collegeName { get; set; }

            [JsonProperty("country")]
            public string country { get; set; }

            [JsonProperty("playerId")]
            public string playerId { get; set; }

            [JsonProperty("dateOfBirth")]
            public string dateOfBirth { get; set; }

            [JsonProperty("affiliation")]
            public string affiliation { get; set; }

            [JsonProperty("startNba")]
            public string startNba { get; set; }

            [JsonProperty("heightInMeters")]
            public string heightInMeters { get; set; }

            [JsonProperty("weightInKilograms")]
            public string weightInKilograms { get; set; }

            [JsonProperty("leagues")]
            public Leagues leagues { get; set; }

        }

        public class Leagues
        {
            public Standard standard { get; set; }
        }
        public class Standard
        {
            [JsonProperty("jersey")]
            public string jersey { get; set; }

            [JsonProperty("active")]
            public string active { get; set; }

            [JsonProperty("pos")]
            public string pos { get; set; }
        }


    }

    

}
