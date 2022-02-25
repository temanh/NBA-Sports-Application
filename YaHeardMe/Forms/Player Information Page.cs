using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;
using Newtonsoft.Json;
using YaHeardMe.Forms;
using YaHeardMe.Models;
using Timer = System.Windows.Forms.Timer;


namespace YaHeardMe
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        

        public async void GetPlayerPicture(List<string> playerInfo) //imageSearch
        {

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://bing-image-search1.p.rapidapi.com/images/search?q={playerInfo[1]}%20{playerInfo[2]}"),
                Headers =
                {
                    {"x-rapidapi-host", "bing-image-search1.p.rapidapi.com"},
                    {"x-rapidapi-key", ""},//fill in your own API key
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                PlayerPicture.Rootobject playerPicture = JsonConvert.DeserializeObject<PlayerPicture.Rootobject>(body);
                //File.WriteAllText(@"D:\YaHeardMe\YaHeardMe\YaHeardMe\TextFiles\PlayerPicture.json", body);
                ShowPlayerPicture(playerPicture, playerInfo);
            }


            if (playerInfo[9] != "" && playerInfo[10] != "")
            {
                var constFt = 3.2808399;
                var meters = Convert.ToDouble(playerInfo[9]);
                var answer = constFt * meters;
                var round = Math.Round(answer, 1);
                var feetToDisplay = round.ToString();

                var constPds = 2.20462262;
                var kiloGrams = Convert.ToDouble(playerInfo[10]);
                var answer2 = constPds * kiloGrams;
                var round2 = Math.Round(answer2);
                var poundsToDisplay = round2.ToString();


                button6.Text = playerInfo[0];
                button5.Text = playerInfo[11];
                button4.Text = playerInfo[1] + ' ' + playerInfo[2];
                richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
                richTextBox1.AppendText("Player Information");
                richTextBox1.AppendText("\r");
                richTextBox1.AppendText("\r");
                richTextBox1.AppendText("\r");
                richTextBox1.AppendText("\r");
                richTextBox1.AppendText("\r");
                richTextBox1.AppendText("Years Pro : " + playerInfo[3] + "\r");
                richTextBox1.AppendText("College Attended : " + playerInfo[4] + "\r");
                richTextBox1.AppendText("Birth Country : " + playerInfo[5] + "\r");
                richTextBox1.AppendText("Date of Birth : " + playerInfo[6] + "\r");
                richTextBox1.AppendText("Affiliation : " + playerInfo[7] + "\r");
                richTextBox1.AppendText("Year Drafted : " + playerInfo[8] + "\r");
                richTextBox1.AppendText("Height Feet.Inches : " + feetToDisplay + "\r");
                richTextBox1.AppendText("Weight in Pounds : " + poundsToDisplay + "\r");

            }
            else
            {

                button6.Text = playerInfo[0];
                button5.Text = playerInfo[11];
                button4.Text = playerInfo[1] + ' ' + playerInfo[2];
                richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
                richTextBox1.AppendText("Player Information");
                richTextBox1.AppendText("\r");
                richTextBox1.AppendText("\r");
                richTextBox1.AppendText("\r");
                richTextBox1.AppendText("\r");
                richTextBox1.AppendText("\r");
                richTextBox1.AppendText("\r");
                richTextBox1.AppendText("\r");
                richTextBox1.AppendText("The player did not have sufficient information.");
                richTextBox1.AppendText("\r");
                richTextBox1.AppendText("\r");
                richTextBox1.AppendText("\r");
                richTextBox1.AppendText("Years Pro : " + playerInfo[3] + "\r");
                richTextBox1.AppendText("College Attended : " + playerInfo[4] + "\r");
                richTextBox1.AppendText("Birth Country : " + playerInfo[5] + "\r");
                richTextBox1.AppendText("Date of Birth : " + playerInfo[6] + "\r");
                richTextBox1.AppendText("Affiliation : " + playerInfo[7] + "\r");
                richTextBox1.AppendText("Year Drafted : " + playerInfo[8] + "\r");
            }



        }

        public async void ShowPlayerPicture(PlayerPicture.Rootobject showPlayerPicture, List<string> playerInfo) //picture
        {
            string playerPicture = showPlayerPicture.value[5].contentUrl;

            using (var client = new WebClient())
            {
                Loading loading = new Loading();
                loading.TimerInterval();
                client.DownloadFile(playerPicture, @"D:\ImagesYaHeard\Images\pic3.jpg");
                pictureBox1.Load(playerPicture);
                GetPlayerNews(playerInfo);

            }

        }


        private void button1_Click(object sender, EventArgs e) // return to search
        {
            Hide();
            Form1 form1 = new Form1();
            InitializeComponent();
            form1.Show();

        }

        public async void GetPlayerNews(List<string> playerInfo)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage()
                {
                    Method = HttpMethod.Get,
                    RequestUri =
                        new Uri(
                            $"https://free-news.p.rapidapi.com/v1/search?q={playerInfo[1]}%20{playerInfo[2]}&lang=en"),
                    Headers =
                    {
                        {"x-rapidapi-host", "free-news.p.rapidapi.com"},
                        {"x-rapidapi-key", ""}, //fill in your own API key
                    },

                };
                using (var response = await client.SendAsync(request))
                {

                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    PlayerNews.Rootobject playernews = JsonConvert.DeserializeObject<PlayerNews.Rootobject>(body);

                    if (playernews.articles == null)
                    {
                        Loading loading = new Loading();
                        loading.timer_tick(null, EventArgs.Empty);
                    }
                    else
                    {
                        webView21.CoreWebView2.Navigate(playernews.articles[0].link);
                    }



                }
            }
            catch(NullReferenceException e)
            {
                MessageBox.Show("Please Enter a Valid Player Name", "Invalid Player Name",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                throw new Exception("Something went wrong, please try again", e);
            }

        }


        private void button2_Click(object sender, EventArgs e) // Get Stats Button
        {
            string playerId = button6.Text;
            string teamId = button5.Text;
            string fullName = button4.Text;
            Hide();
            StatisticsPage form3 = new StatisticsPage();
            form3.Show();
            form3.ShowWebPage(fullName, teamId, playerId);

        }

        private void button7_Click(object sender, EventArgs e) // close app
        {
            Application.Exit();
        }
    }
}
