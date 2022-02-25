using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Windows.Forms;
using HtmlAgilityPack;
using Newtonsoft.Json;
using YaHeardMe.Models;
using Application = System.Windows.Forms.Application;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using Image = System.Drawing.Image;
using Label = System.Web.UI.WebControls.Label;
using MessageBox = System.Windows.Forms.MessageBox;

namespace YaHeardMe.Forms
{
    //Welcome to my program
    public partial class StatisticsPage : Form 
    {
        public StatisticsPage()
        {
            InitializeComponent();
        }
        public void ShowWebPage(string fullName, string teamId, string playerId)
        {
            var commands = fullName.Split(' ');
            string firstName = commands[0];
            string lastName = commands[1];


            if (fullName.Length <= 7) //short first and last (Bol Bol)
            {
                Loading loading = new Loading();
                loading.TimerInterval();
                string splitLas = lastName.Substring(0, 3).ToLower();
                string splitFir = firstName.Substring(0, 2).ToLower();
                string firstI = splitLas.Remove(1).ToLower();
                string webPageName = string.Concat(splitLas, splitFir + "01");
                webBrowser1.Navigate($"https://www.basketball-reference.com/players/{firstI}/{webPageName}.html");
                GetStatsToMeasure(teamId, playerId, fullName);
            }
            else if(lastName.Length <= 4) //short last (Lonzo Ball)
            {
                
                string splitLas = lastName.Substring(0, 4).ToLower();
                string splitFir = firstName.Substring(0, 2).ToLower();
                string firstI = splitLas.Remove(1).ToLower();
                string webPageName = string.Concat(splitLas, splitFir + "01");
                webBrowser1.Navigate($"https://www.basketball-reference.com/players/{firstI}/{webPageName}.html");
                GetStatsToMeasure(teamId, playerId, fullName);
            }
            else //(Normal Names)
            {
                string splitLas = lastName.Substring(0, 5).ToLower();
                string splitFir = firstName.Substring(0, 2).ToLower();
                string firstI = splitLas.Remove(1).ToLower();
                string webPageName = string.Concat(splitLas, splitFir + "01");
                webBrowser1.Navigate($"https://www.basketball-reference.com/players/{firstI}/{webPageName}.html");
                GetStatsToMeasure(teamId, playerId, fullName);
            }


        }

        public async void GetStatsToMeasure(string teamId, string playerId, string fullName)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api-nba-v1.p.rapidapi.com/players/statistics?id={playerId}&team={teamId}&season=2021"),
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
                GameStats.Rootobject gameStats = JsonConvert.DeserializeObject<GameStats.Rootobject>(body);
                if (gameStats.results != 0 && gameStats.response[0].comment != "DNP - Injury/Illness")
                {
                    UseStats(gameStats);
                }
                else
                {
                    SkipStats(fullName);
                }

            }

        }

        public void UseStats(GameStats.Rootobject measureStats)
        {
            var length = measureStats.response.Length;
            var points = "";
            string firstName = measureStats.response[0].player.firstname;

            List<string> playerPoints = new List<string>();
            for (int i = length -1 ; i >= 0; i-- )
            {
                points = measureStats.response[i].points;
                playerPoints.Add(points);
            }

            playerPoints.RemoveAll(item => item == null);

            List<double> result = playerPoints.Select(x => double.Parse(x)).ToList();

            var pointsAvg = result.Average();

            if (pointsAvg == 0)
            {
                textBox1.Text = ($"{firstName} has not played in the 2021-22 NBA Season. Most likely an injury has sidelined him for the season. Well Wishes!");
                textBox2.Visible = false;
                textBox3.Visible = false;
            }
            else if (pointsAvg <= 10)
            {
                textBox1.Text = ($"Averaging under 10 points this season, {firstName} is most likely coming off the bench.");
            }

            else if (pointsAvg > 10 && pointsAvg <= 15)
            {
                textBox1.Text = ($"Averaging between 10-15 points this season, {firstName} plays a substantial role on the team and provides reliable scoring.");
            }
            else if (pointsAvg > 15 && pointsAvg <= 20)
            {
                textBox1.Text = ($"Averaging between 15-20 points this season, {firstName} is one of the premier stars in the league and is a consistent offensive threat.");
            }
            else if (pointsAvg > 20 && pointsAvg <= 25)
            {
                textBox1.Text = ($"Averaging between 20-25 points this season, {firstName} is probably the best player on the team, and is playing substantial minutes");
            }
            else
            {
                textBox1.Text = ($"Averaging over 25 points this season, {firstName} is definitely the best player on the team, a complete offensive threat and is most likely on the NBA 75th Anniversary Team");
            }

            var rebounds = "";
            List<string> playerRebounds = new List<string>();
            for (int i = length - 1; i >= 0; i--)
            {
                rebounds = measureStats.response[i].totReb;
                playerRebounds.Add(rebounds);
            }

            playerRebounds.RemoveAll(item => item == null);

            List<double> reDoubles = playerRebounds.Select(x => double.Parse(x)).ToList();

            var rbsAvg = reDoubles.Average();

            if (rbsAvg == 0)
            {
                textBox1.Text = ($"{firstName} has not played in the 2021-22 NBA Season Most likely an injury has sidelined him for the season. Well Wishes!");
                textBox2.Visible = false;
                textBox3.Visible = false;
            }
            else if (rbsAvg < 5)
            {
                textBox2.Text = ($"Averaging under 5 rebounds this season, {firstName} is not a dominant presence on the glass");
            }
            else if (rbsAvg > 5 && rbsAvg < 10)
            {
                textBox2.Text = ($"Averaging between 5-10 rebounds this season, {firstName} is most likely a SF/PF. Definitely an aggresive presence on the glass and he is playing a vital role in assisting the center in securing rebounds");
            }
            else
            {
                textBox2.Text = ($"Averaging over 10 rebounds this season, {firstName} is most likely a C, which makes their primary job securing rebounds for the team.");
            }




            var assists = "";
            List<string> playerAssists = new List<string>();
            for (int i = length - 1; i >= 0; i--)
            {
                assists = measureStats.response[i].assists;
                playerAssists.Add(assists);
            }

            playerAssists.RemoveAll(item => item == null);

            List<double> assDoubles = playerAssists.Select(x => double.Parse(x)).ToList();

            var assAvg = assDoubles.Average(); //lol

            if (assAvg == 0)
            {
                textBox1.Text = ($"{firstName} has not played in the 2021-22 NBA Season. Most likely an injury has sidelined him for the season. Well Wishes!");
                textBox2.Visible = false;
                textBox3.Visible = false;
            }
            else if (assAvg > 0 && assAvg <= 5)
            {
                textBox3.Text = ($"Averaging under 5 assists this season, {firstName} is not the primary facilitator of the offense.");
            }
            else if (assAvg > 5 && assAvg <= 7)
            {
                textBox3.Text = ($"Averaging between 5-7 assists this season, {firstName} is an efficient passer and can reliably get their teammates involved.");
            }
            else if (assAvg > 7 && assAvg <= 10)
            {
                textBox3.Text = ($"Averaging between 7-10 assists this season, {firstName} is most likely the primary distributor for the team and has superb court vision.");
            }
            else
            {
                textBox3.Text = ($"Averaging over 10 assists this season, {firstName} definitely has a pass-first mentality, legendary court vision and is possibly on the NBA 75th Team.");
            }

        }

        public void SkipStats(string fullName)
        {
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            NoResult noResult = new NoResult();
            noResult.Show();
            if (fullName == "Kobe Bryant")
            {
                noResult.textBox1.Text = "RIP to the GOAT. Feel Free to browse career stats.";
            }
            else
            {
                noResult.textBox1.Text = $"{fullName} is not a active player or did not play this season due to Injury/Illness, feel free to browse career stats!";
            }
        }

        public void button1_Click(object sender, EventArgs e)
        {
            Hide();
            Form1 form1 = new Form1();
            InitializeComponent();
            form1.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }

    
    
}
