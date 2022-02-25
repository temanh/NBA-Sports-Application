using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using YaHeardMe.Forms;

namespace YaHeardMe
{
    public partial class CustomMsgBox : Form
    {
        public CustomMsgBox()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }


        private void label4_Click(object sender, EventArgs e)
        {

        }

        public void button1_Click(object sender, EventArgs e) //close 75th form
        {
           CustomMsgBox.ActiveForm.Close(); 

        }



        private void button2_Click(object sender, EventArgs e) //Search for Player Button
        {
            #region 75th Player List
            List<string> playersList = new List<string>()
            {
                "Kareem Abdul-Jabbar",

                "Ray Allen",

                "Giannis Antetokounmpo",

                "Carmelo Anthony",

                "Nate Archibald",

                "Paul Arizin",

                "Charles Barkley",

                "Rick Barry",

                "Elgin Baylor",

                "Dave Bing",

                "Larry Bird",

                "Kobe Bryant",

                "Wilt Chamberlain",

                "Bob Cousy",

                "Dave Cowens",

                "Billy Cunningham",

                "Stephen Curry",

                "Anthony Davis",

                "Dave DeBusschere",

                "Clyde Drexler",

                "Tim Duncan",

                "Kevin Durant",

                "Julius Erving",

                "Patrick Ewing",

                "Walt Frazier",

                "Kevin Garnett",

                "George Gervin",

                "Hal Greer",

                "James Harden",

                "John Havlicek",

                "Elvin Hayes",

                "Allen Iverson",

                "LeBron James",

                "Magic Johnson",

                "Sam Jones",

                "Michael Jordan",

                "Jason Kidd",

                "Kawhi Leonard",

                "Damian Lillard",

                "Jerry Lucas",

                "Karl Malone",

                "Moses Malone",

                "Pete Maravich",

                "Bob McAdoo",

                "Kevin McHale",

                "George Mikan",

                "Reggie Miller",

                "Earl Monroe",

                "Steve Nash",

                "Dirk Nowitzki",

                "Hakeem Olajuwon",

                "Shaquille O'Neal",

                "Robert Parish",

                "Chris Paul",

                "Gary Payton",

                "Bob Pettit",

                "Paul Pierce",

                "Scottie Pippen",

                "Willis Reed",

                "Oscar Robertson",

                "David Robinson",

                "Dennis Rodman",

                "Bill Russell",

                "Dolph Schayes",

                "Bill Sharman",

                "John Stockton",

                "Isiah Thomas",

                "Nate Thurmond",

                "Wes Unseld",

                "Dwyane Wade",

                "Bill Walton",

                "Jerry West",

                "Russell Westbrook",

                "Lenny Wilkens",

                "Dominique Wilkins",

                "James Worthy"
            };

            #endregion
            foreach (string player in playersList)
            {
                if (textBox1.Text != null && playersList.Contains(textBox1.Text, StringComparer.InvariantCultureIgnoreCase))
                {
                    CustomMsgBox.ActiveForm.Hide();
                    Success75th success75 = new Success75th();
                    success75.TopMost = true;
                    success75.StartPosition = FormStartPosition.CenterScreen;
                    success75.ShowDialog();
                    break;
                    
                }

                if (textBox1.Text == "")
                {
                    MessageBox.Show("Please enter a name within the field", "A Name Would Be Nice...", MessageBoxButtons.OK);
                    break;
                }
                else
                {
                    MessageBox.Show("Player is Not On the List!", "Please Try Again!", MessageBoxButtons.OK);
                    break;
                }


            }
        }

        public void textBox1_TextChanged(object sender, EventArgs e) // Search for Player Text
        {


        }
    }
}
