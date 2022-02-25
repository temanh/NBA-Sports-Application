using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaHeardMe.Models
{
    public class GameStats
    {

        public class Rootobject
        {
            public Parameters parameters { get; set; }
            public int results { get; set; }
            public Response[] response { get; set; }
        }

        public class Parameters
        {
            public string id { get; set; }
            public string team { get; set; }
            public string season { get; set; }
        }

        public class Response
        {
            public Player player { get; set; }

            public string points { get; set; }

            public string fgp { get; set; }
            public string ftp { get; set; }
            public string tpm { get; set; }
            public string tpp { get; set; }
            public string offReb { get; set; }
            public string defReb { get; set; }
            public string totReb { get; set; }
            public string assists { get; set; }
            public string steals { get; set; }
            public string turnovers { get; set; }
            public string blocks { get; set; }
            public string plusMinus { get; set; }
            public string comment { get; set; }
        }

        public class Player
        {
            public int id { get; set; }
            public string firstname { get; set; }
            public string lastname { get; set; }
        }




    }
}
