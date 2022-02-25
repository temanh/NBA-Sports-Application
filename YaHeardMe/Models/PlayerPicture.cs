using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Runtime.Remoting;
using System.Text.Json.Serialization;
using System.Windows.Controls;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace YaHeardMe.Models
{
    public class PlayerPicture
    {
        public class Rootobject
        {

            public Value[] value { get; set; }

        }

        public class Instrumentation
        {
            public string _type { get; set; }
        }


        public class Value
        {
            public string webSearchUrl { get; set; }
            public string name { get; set; }
            public string thumbnailUrl { get; set; }
            public DateTime datePublished { get; set; }
            public bool isFamilyFriendly { get; set; }
            public string contentUrl { get; set; }



        }
    }
}