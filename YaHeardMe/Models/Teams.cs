using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace YaHeardMe
{
    public class Teams
    {
        public class Rootobject
        {
            public Api api { get; set; }
        }

        public class Api
        {
            public Team[] teams { get; set; }
        }

        public class Team
        {
            public string city { get; set; }
            public string fullName { get; set; }
            public string teamId { get; set; }
            public string nickname { get; set; }
            public string logo { get; set; }
            public string shortName { get; set; }
            public string allStar { get; set; }
            public string nbaFranchise { get; set; }
            public Leagues leagues { get; set; }
        }

        public class Leagues
        {
            public Standard standard { get; set; }
            public Sacramento sacramento { get; set; }
            public Vegas vegas { get; set; }
            public Utah utah { get; set; }
        }

        public class Standard
        {
            public string confName { get; set; }
            public string divName { get; set; }
        }

        public class Sacramento
        {
            public string confName { get; set; }
            public string divName { get; set; }
        }

        public class Vegas
        {
            public string confName { get; set; }
            public string divName { get; set; }
        }

        public class Utah
        {
            public string confName { get; set; }
            public string divName { get; set; }
        }
    }

}
