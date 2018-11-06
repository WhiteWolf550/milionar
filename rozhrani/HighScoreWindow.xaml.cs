using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace rozhrani {
    /// <summary>
    /// Interakční logika pro HighScoreWindow.xaml
    /// </summary>
    
    public partial class HighScoreWindow : Window {
        static JsonSerializerSettings settings = new JsonSerializerSettings {
            TypeNameHandling = TypeNameHandling.All
        };
        static string pathHighScore = @"../../saves/Highscore.json";
        static string jsonFromFileHighScore = File.ReadAllText(pathHighScore);
        static List<Player> scores = JsonConvert.DeserializeObject<List<Player>>(jsonFromFileHighScore, settings);
        int pom = 0;
        string poms = "";
        int pom2 = 5000000;
        string poms2 = "";
        int pom3 = 5000000;
        string poms3 = "";
        int lowest = 0;
        public HighScoreWindow() {
            InitializeComponent();
            ShowScores();
        }
        public void ShowScores() {
            int i = 0;
            foreach (var item in scores) {
                if (i == 0) {
                    pom = item.Highscore;
                    lowest = item.Highscore;
                }
                if (item.Highscore < pom2 && item.Highscore > lowest) {
                    pom3 = item.Highscore;
                    poms3 = item.Name;
                }
                if (item.Highscore == pom || item.Highscore < pom) {
                    pom2 = item.Highscore;
                    poms2 = item.Name;
                }
                if (item.Highscore > pom) {
                    pom = item.Highscore;
                    poms = item.Name;
                    
                }
                if (item.Highscore < lowest) {

                }
                
                i++;
            }
            player1.Content = poms;
            score1.Content = pom;

            player2.Content = poms2;
            score2.Content = pom2;

            player3.Content = poms3;
            score3.Content = pom3;
            score5.Content = lowest;
            /*var matches5 = scores.Where(s => s.Highscore == 10000).ToList();
            player5.Content = matches5[0].Name;
            score5.Content = matches5[0].Highscore;

            var matches4 = scores.Where(s => s.Highscore <= 320000 && s.Highscore >= 10000).ToList();
            player4.Content = matches4[0].Name;
            score4.Content = matches4[0].Highscore;

            var matches3 = scores.Where(s => s.Highscore <= 1000000 && s.Highscore >= 10000).ToList();
            player3.Content = matches3[0].Name;
            score3.Content = matches3[0].Highscore;*/
        }
    }
}
