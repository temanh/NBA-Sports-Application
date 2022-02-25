using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI.WebControls.Adapters;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YaHeardMe.Forms;
using YaHeardMe.Models;

namespace YaHeardMe
{
    public partial class Form1 : Form 
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Text = "JSON Binding";
        }



        #region Search Paths
        public async void button2_Click(object sender, EventArgs e) //search player button
        {
            if (textBox2.Text != "")
            {
                var url = $"https://api-nba-v1.p.rapidapi.com/players/firstName/{textBox2.Text}";
                CallFirstNameApi(url);
            }

            if (textBox3.Text != "")
            {
                var url = $"https://api-nba-v1.p.rapidapi.com/players/lastName/{textBox3.Text}";
                CallLastNameApi(url);
            }
            else
            {
                var url = $"https://api-nba-v1.p.rapidapi.com/teams/nickName/{textBox3.Text}";
                CallTeamApi(url);
            }

        } 
        #endregion

        #region FirstNameApi
        public async Task<Players.Rootobject> CallFirstNameApi(string url)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://api-nba-v1.p.rapidapi.com/players/firstName/{textBox2.Text}"),
                    Headers =
                    {
                        { "x-rapidapi-host", "api-nba-v1.p.rapidapi.com" },
                        { "x-rapidapi-key", "" }, //fill in your own API key
                    },
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();

                    File.WriteAllText(@"D:\YaHeardMe\YaHeardMe\YaHeardMe\TextFiles\Players.json", body);

                    var path = @"D:\YaHeardMe\YaHeardMe\YaHeardMe\TextFiles\Players.json";
                    string jsonFile = File.ReadAllText(path);


                    Players.Rootobject player = JsonConvert.DeserializeObject<Players.Rootobject>(jsonFile);
                    Players.Rootobject players = JsonConvert.DeserializeObject<Players.Rootobject>(body);

                    if (players.api.results == 0)
                    {
                        MessageBox.Show("Player is Not in The Database or Invalid Player Name", "No results found",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    }
                    else
                    {
                        dataGridView1.DataSource = player.api.players;
                    }
                    
                    return player;
                }

            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong, please try again", e);

            }
        }
        #endregion

        #region LastNameApi
        public async Task<Players.Rootobject> CallLastNameApi(string url)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://api-nba-v1.p.rapidapi.com/players/lastName/{textBox3.Text}"),
                    Headers =
                    {
                        { "x-rapidapi-host", "api-nba-v1.p.rapidapi.com" },
                        { "x-rapidapi-key", "" },//fill in your own API key
                    },
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    Players.Rootobject players = JsonConvert.DeserializeObject<Players.Rootobject>(body);
                    dataGridView1.DataSource = players.api.players;

                    File.WriteAllText(@"D:\YaHeardMe\YaHeardMe\YaHeardMe\TextFiles\Players.json", body);
                    var path = @"D:\YaHeardMe\YaHeardMe\YaHeardMe\TextFiles\Players.json";
                    string jsonFile = File.ReadAllText(path);

                    if (players.api.results == 0)
                    {
                        MessageBox.Show("Player is Not in The Database or Invalid Player Name", "No results found",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    }
                    else
                    {
                        dataGridView1.DataSource = players.api.players;
                    }




                    return players;
                    
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Please Enter a Valid Player Name", "Invalid Player Name",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                throw new Exception("Something went wrong, please try again", e);

            }
        }
        #endregion

        #region TeamApi
        public async Task<Teams.Rootobject> CallTeamApi(string url)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://api-nba-v1.p.rapidapi.com/teams/nickName/{textBox4.Text}"),
                    Headers =
                    {
                        { "x-rapidapi-host", "api-nba-v1.p.rapidapi.com" },
                        { "x-rapidapi-key", "" },//fill in your own API key
                    },
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();

                    Teams.Rootobject teams = JsonConvert.DeserializeObject<Teams.Rootobject>(body);
                    File.WriteAllText(@"D:\YaHeardMe\YaHeardMe\YaHeardMe\TextFiles\Teams.json", body);
                    ShowTeamPicture(teams);
                    dataGridView2.DataSource = teams.api.teams;
                    return teams;

                }
                

            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong, please try again", e);
            }
        }
        #endregion

        public void ShowTeamPicture(Teams.Rootobject showTeamLogo)
        {
            #region Switch Statement

                switch (showTeamLogo.api.teams[0].shortName)
                {
                    case "ATL":
                        pictureBox1.Image = Image.FromFile(@"C:\Images\ATL.jpg");
                        break;
                    case "BKN":
                        pictureBox1.Image = Image.FromFile(@"C:\Images\BKN.jpeg");
                        break;
                    case "BOS":
                        pictureBox1.Image = Image.FromFile(@"C:\Images\BOS.jpeg");
                        break;
                    case "CHA":
                        pictureBox1.Image = Image.FromFile(@"C:\Images\CHA.jpeg");
                        break;
                    case "CHI":
                        pictureBox1.Image = Image.FromFile(@"C:\Images\CHI.jpeg");
                        break;
                    case "CLE":
                        pictureBox1.Image = Image.FromFile(@"C:\Images\CLE.jfif");
                        break;
                    case "DAL":
                        pictureBox1.Image = Image.FromFile(@"C:\Images\DAL.jpeg");
                        break;
                    case "DEN":
                        pictureBox1.Image = Image.FromFile(@"C:\Images\DEN.jpg");
                        break;
                    case "DET":
                        pictureBox1.Image = Image.FromFile(@"C:\Images\DET.jpg");
                        break;
                    case "GSW":
                        pictureBox1.Image = Image.FromFile(@"C:\Images\GSW.jpg");
                        break;
                    case "HOU":
                        pictureBox1.Image = Image.FromFile(@"C:\Images\HOU.jpg");
                        break;
                    case "IND":
                        pictureBox1.Image = Image.FromFile(@"C:\Images\IND.jfif");
                        break;
                    case "LAC":
                        pictureBox1.Image = Image.FromFile(@"C:\Images\LAC.jpg");
                        break;
                    case "LAL":
                        pictureBox1.Image = Image.FromFile(@"C:\Images\LAL.jpg");
                        break;
                    case "MEM":
                        pictureBox1.Image = Image.FromFile(@"C:\Images\MEM.png");
                        break;
                    case "MIA":
                        pictureBox1.Image = Image.FromFile(@"C:\Images\MIA.jpg");
                        break;
                    case "MIL":
                        pictureBox1.Image = Image.FromFile(@"C:\Images\MIL.jfif");
                        break;
                    case "MIN":
                        pictureBox1.Image = Image.FromFile(@"C:\Images\MIN.jpg");
                        break;
                    case "NOP":
                        pictureBox1.Image = Image.FromFile(@"C:\Images\NOP.jpg");
                        break;
                    case "NYK":
                        pictureBox1.Image = Image.FromFile(@"C:\Images\NYK.jpg");
                        break;
                    case "OKC":
                        pictureBox1.Image = Image.FromFile(@"C:\Images\OKC.jfif");
                        break;
                    case "ORL":
                        pictureBox1.Image = Image.FromFile(@"C:\Images\ORL.jfif");
                        break;
                    case "PHI":
                        pictureBox1.Image = Image.FromFile(@"C:\Images\PHI.jpg");
                        break;
                    case "PHX":
                        pictureBox1.Image = Image.FromFile(@"C:\Images\PHX.jpeg");
                        break;
                    case "POR":
                        pictureBox1.Image = Image.FromFile(@"C:\Images\POR.jpg");
                        break;
                    case "SAC":
                        pictureBox1.Image = Image.FromFile(@"C:\Images\SAC.jpeg");
                        break;
                    case "SAS":
                        pictureBox1.Image = Image.FromFile(@"C:\Images\SAS.jfif");
                        break;
                    case "TOR":
                        pictureBox1.Image = Image.FromFile(@"C:\Images\TOR.jpg");
                        break;
                    case "UTA":
                        pictureBox1.Image = Image.FromFile(@"C:\Images\UTA.jpg");
                        break;
                    case "WAS":
                        pictureBox1.Image = Image.FromFile(@"C:\Images\WAS.jfif");
                        break;
                }

                #endregion
        }

        #region ReadOnlyLabels
        public void label1_Click(object sender, EventArgs e)
        {

        }

        public void label1_Click_1(object sender, EventArgs e) //first name readOnly
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e) //first name readOnly
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e) //first name text
        {

        }



        private void textBox6_TextChanged(object sender, EventArgs e) //last name readOnly
        {

        }
        private void textBox3_TextChanged(object sender, EventArgs e) //last name text
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e) //team readOnly
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e) // team text
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void playerBindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        } 
        #endregion

        public void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows[0].Selected && dataGridView1.SelectedRows != null)
            {
                List<string> players = new List<string>();
                players.Add(dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[0].Value.ToString());
                players.Add( dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[1].Value.ToString());
                players.Add(dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[2].Value.ToString());
                players.Add(dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[3].Value.ToString());
                players.Add(dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[4].Value.ToString());
                players.Add(dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[5].Value.ToString());
                players.Add(dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[6].Value.ToString());
                players.Add(dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[7].Value.ToString());
                players.Add(dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[8].Value.ToString());
                players.Add(dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[9].Value.ToString());
                players.Add(dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[10].Value.ToString());
                players.Add(dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[11].Value.ToString());

                LoadPlayer(players);
                Hide();
            }

        }

        public static void LoadPlayer(List<string> playerInfo)
        {
            
            Form2 form2 = new Form2();
            form2.Show();
            form2.GetPlayerPicture(playerInfo);

        }


        public void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        public void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void richTextBox1_TextChanged_1(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        public void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            richTextBox2.SelectionAlignment = HorizontalAlignment.Center;
        }


        public void Search75Yes_Click(object sender, EventArgs e)
        {
            CustomMsgBox cstBox = new CustomMsgBox();
            cstBox.TopMost = true;
            cstBox.StartPosition = FormStartPosition.CenterScreen;
            cstBox.ShowDialog();
            
        }

        public void No75thButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("At Least Try! No Pressure >:)", "Disappointing....", MessageBoxButtons.OK,
                MessageBoxIcon.None, MessageBoxDefaultButton.Button1);

        }

        private void ImNotSureButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The 75th Anniversary Team Consists of NBA All Time Greats. \rJust try and think about famous athletes, one might be on the list!", "You Got This!", MessageBoxButtons.OK,
                MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }
    }
}
