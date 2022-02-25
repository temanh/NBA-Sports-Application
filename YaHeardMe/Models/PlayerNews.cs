using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YaHeardMe.Models
{
    public class PlayerNews
    {
        public class Rootobject
        {
           
            public int page_size { get; set; }
            public Article[] articles { get; set; }
            public User_Input user_input { get; set; }
        }

        public class User_Input
        {
        }

        public class Article
        {

            public string link { get; set; }
            public string clean_url { get; set; } 
           
        }





    }
}
