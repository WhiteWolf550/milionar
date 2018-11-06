using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace rozhrani
{
    /// <summary>
    /// Interakční logika pro Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        int diff = 1;
        bool fif = true;
        bool phon = true;
        int poradi = 1;
        int button2;
        public string questionara = "";
        public string rans = "";
        public string ans2 = "";
        public string ans3 = "";
        public string ans4 = "";
        public bool AnswerActive = true;
        bool Checkpoint1 = false;
        int Highscore = 0;
        bool Checkpoint2 = false;
        bool Checkpoint3 = false;
        bool hlpPeople = true;
        string ranswer = "";
        Button bb;
        static string pathintro = @"../../Sound/intro_music.wav";
        static string path2 = @"../../Sound/lose2.wav";
        static string gamewin = @"../../Sound/win.wav";
        static string pathright = @"../../Sound/right.wav";
        static string path3 = @"../../Sound/lose.wav";
        static string music1path = @"../../Sound/music1.wav";
        static string music2path = @"../../Sound/music2.wav";
        static string peoplepath = @"../../Sound/people.wav";
        static string music3path = @"../../Sound/music3.wav";
        static string fiftypath = @"../../Sound/fifty.wav";
        static string musicfinalpath = @"../../Sound/musicfinal.wav";
        static string pathHighScore = @"../../saves/Highscore.json";
        static string pathQuest = @"../../saves/Quest.json";
        Random rand = new Random();
        SoundPlayer intro = new SoundPlayer(pathintro);
        SoundPlayer peoples = new SoundPlayer(peoplepath);
        SoundPlayer rightanswer = new SoundPlayer(pathright);
        SoundPlayer lose = new SoundPlayer(path2);
        SoundPlayer fiftysound = new SoundPlayer(fiftypath);
        SoundPlayer wingame = new SoundPlayer(gamewin);
        SoundPlayer gamebegin = new SoundPlayer(path3);
        SoundPlayer music1 = new SoundPlayer(music1path);
        SoundPlayer music2 = new SoundPlayer(music2path);
        SoundPlayer music3 = new SoundPlayer(music3path);
        SoundPlayer musicfinal = new SoundPlayer(musicfinalpath);
        //List<Questions> quest = new List<Questions>();
        //List<Player> scores = new List<Player>();
        List<Player> players = new List<Player>();
        static JsonSerializerSettings settings = new JsonSerializerSettings {
            TypeNameHandling = TypeNameHandling.All
        };
        static string jsonFromFileHighScore = File.ReadAllText(pathHighScore);
        static string jsonFromFileQuest = File.ReadAllText(pathQuest);
        //static string jsonFromFileHighscore = File.ReadAllText(pathHS);
        //List<QaA> item = new List<QaA>();
        static List<Player> scores = JsonConvert.DeserializeObject<List<Player>>(jsonFromFileHighScore, settings);
        static List<Questions> quest = JsonConvert.DeserializeObject<List<Questions>>(jsonFromFileQuest, settings);
        public void PlayGame()
        {
            AnswerActive = true;
            gamebegin.Play();
            A.Visibility = Visibility.Visible;
            B.Visibility = Visibility.Visible;
            C.Visibility = Visibility.Visible;
            D.Visibility = Visibility.Visible;
            fifty.Visibility = Visibility.Visible;
            phone.Visibility = Visibility.Visible;
            people.Visibility = Visibility.Visible;
            back.Visibility = Visibility.Hidden;
            exit.Visibility = Visibility.Hidden;
            hint1.Visibility = Visibility.Hidden;
            hint2.Visibility = Visibility.Hidden;
            hint3.Visibility = Visibility.Hidden;
            hint4.Visibility = Visibility.Hidden;

            GenerateQuestions();
            //Random rand = new Random();
            //int count = quest.Count();
            //int num = rand.Next(0, count);
            Money();

            int randomvys2 = rand.Next(0, 201);
            int choose = rand.Next(1, 3);
            int button = rand.Next(1, 5);
            button2 = button;
            int ans = rand.Next(1, 5);
            var matches = quest.Where(s => s.difficulty == diff).ToList();
            int count = matches.Count();
            int num = rand.Next(0, count);
            question.Content = poradi + "." + matches[num].question;
            questionara = matches[num].question;
            rans = matches[num].answer1;
            ans2 = matches[num].answer2;
            ans3 = matches[num].answer3;
            ans4 = matches[num].answer4;
            ranswer = matches[num].answer1;
            if (button == 1)
            {
                A.Content = matches[num].answer1;
                B.Content = matches[num].answer2;
                C.Content = matches[num].answer3;
                D.Content = matches[num].answer4;
            }
            else if (button == 2)
            {
                A.Content = matches[num].answer3;
                B.Content = matches[num].answer4;
                C.Content = matches[num].answer1;
                D.Content = matches[num].answer2;
            }
            else if (button == 3)
            {
                A.Content = matches[num].answer3;
                B.Content = matches[num].answer4;
                C.Content = matches[num].answer2;
                D.Content = matches[num].answer1;
            }
            else if (button == 4)
            {
                A.Content = matches[num].answer4;
                B.Content = matches[num].answer1;
                C.Content = matches[num].answer2;
                D.Content = matches[num].answer3;
            }


            poradi++;
            //vysledek = cislo1 + cislo2;
            //priklad.Content = cislo1 + "+" + cislo2 + "=?";
        }
        public void GameWin()
        {
            question.Content = "Vyhrál jsi 10,000,000 grataluji!!";
            question.FontSize = 30;
            back.Visibility = Visibility.Visible;
            exit.Visibility = Visibility.Visible;
            wingame.Play();
            Highscore = 10000000;
            Input inputwindow = new Input("Napište vaše jméno", "Player");
            // inputwindow.ShowDialog();
            if (inputwindow.ShowDialog() == true) {
                string ranswer = inputwindow.Answer;
                Player playernew = new Player();
                playernew.Name = ranswer;
                playernew.Highscore = Highscore;
                scores.Add(playernew);
                //scores.Add(new Player(ranswer, Highscore));
                string jsonToFile = JsonConvert.SerializeObject(scores, settings);
                File.WriteAllText(pathHighScore, jsonToFile);
                //exit.Content = inputwindow.Answer;
            }

        }
        public void Money()
        {
            if (diff == 1)
            {
                jedna.Background = Brushes.Red;
                jedna.Foreground = Brushes.White;
            }
            else if (diff == 2)
            {
                jedna.Foreground = Brushes.Orange;
                jedna.Background = Brushes.Transparent;
                dva.Background = Brushes.Red;
                dva.Foreground = Brushes.White;
                music1.Play();
            }
            else if (diff == 3)
            {
                dva.Foreground = Brushes.Orange;
                dva.Background = Brushes.Transparent;
                tri.Background = Brushes.Red;
                tri.Foreground = Brushes.White;
                music1.Play();
            }
            else if (diff == 4)
            {
                tri.Foreground = Brushes.Orange;
                tri.Background = Brushes.Transparent;
                ctyri.Background = Brushes.Red;
                ctyri.Foreground = Brushes.White;
                music1.Play();
            }
            else if (diff == 5)
            {
                ctyri.Foreground = Brushes.Orange;
                ctyri.Background = Brushes.Transparent;
                pet.Background = Brushes.Red;
                pet.Foreground = Brushes.White;
                music1.Play();
            }
            else if (diff == 6)
            {
                pet.Foreground = Brushes.White;
                pet.Background = Brushes.Transparent;
                sest.Foreground = Brushes.White;
                sest.Background = Brushes.Red;
                Highscore = 10000;
                Checkpoint1 = true;
                music2.Play();
            }
            else if (diff == 7)
            {
                sest.Foreground = Brushes.Orange;
                sest.Background = Brushes.Transparent;
                sedm.Background = Brushes.Red;
                sedm.Foreground = Brushes.White;
                music2.Play();
            }
            else if (diff == 8)
            {
                sedm.Foreground = Brushes.Orange;
                sedm.Background = Brushes.Transparent;
                osum.Background = Brushes.Red;
                osum.Foreground = Brushes.White;
                music2.Play();
            }
            else if (diff == 9)
            {
                osum.Foreground = Brushes.Orange;
                osum.Background = Brushes.Transparent;
                devet.Background = Brushes.Red;
                devet.Foreground = Brushes.White;
                music2.Play();
            }
            else if (diff == 10)
            {
                devet.Foreground = Brushes.Orange;
                devet.Background = Brushes.Transparent;
                deset.Background = Brushes.Red;
                deset.Foreground = Brushes.White;
                music2.Play();
            }
            else if (diff == 11)
            {
                deset.Foreground = Brushes.White;
                deset.Background = Brushes.Transparent;
                jedenact.Background = Brushes.Red;
                jedenact.Foreground = Brushes.White;
                Checkpoint1 = false;
                Checkpoint2 = true;
                Highscore = 320000;
                music3.Play();
            }
            else if (diff == 12)
            {
                jedenact.Foreground = Brushes.Orange;
                jedenact.Background = Brushes.Transparent;
                dvanact.Background = Brushes.Red;
                dvanact.Foreground = Brushes.White;
                music3.Play();
            }
            else if (diff == 13)
            {
                dvanact.Foreground = Brushes.Orange;
                dvanact.Background = Brushes.Transparent;
                trinact.Background = Brushes.Red;
                trinact.Foreground = Brushes.White;
                music3.Play();
            }
            else if (diff == 14)
            {
                trinact.Foreground = Brushes.Orange;
                trinact.Background = Brushes.Transparent;
                ctrnact.Background = Brushes.Red;
                ctrnact.Foreground = Brushes.White;
                music3.Play();
            }
            else if (diff == 15)
            {
                ctrnact.Foreground = Brushes.Orange;
                ctrnact.Background = Brushes.Transparent;
                patnact.Background = Brushes.Red;
                patnact.Foreground = Brushes.White;
                //Highscore = 10000000;
                musicfinal.Play();
            }else if (diff == 16) {
                Checkpoint2 = false;
                Checkpoint3 = true;
                //Highscore = 10000000;
            }

        }
        public void GenerateQuestions()
        {
            //1
            /*quest.Add(new Questions("Být 'švorc' znamená nemít momentálně vůbec žádné:", "peníze", "vlasy", "zuby", "příbuzné", 1));
            quest.Add(new Questions("Slavná a vážená osobnost se cizím slovem označuje jako:", "celebrita", "cerebrita", "celerita", "cereblita", 1));
            quest.Add(new Questions("V pohádce Jana Wericha „Tři veteráni“ princezně Bosaně naroste:", "dlouhý nos", "jelení paroh", "kančí tesák", "sloní kel", 1));
            quest.Add(new Questions("Která barva dužiny převládá u vodního melounu", "červená", "zelená", "žlutá", "bílá", 1));
            quest.Add(new Questions("Jak dlouho trvala stoletá válka?", "116", "99", "100", "150", 1));
            //2
            quest.Add(new Questions("V písni ze kterého českého televizního seriálu se zpívá, že 'Štěstí nekoupíš za milion..'?", "Chalupáři", "Pojišťovna štěstí", "Hospoda", "Tři chlapi v chalupě", 2));
            quest.Add(new Questions("Ploché tašky z pálené hlíny, ukončené ve tvaru připomínajícím ocas jednoho hlodavce, se nazývají:", "bobrovky", "potkanky", "krysovky", "hrabošky", 2));
            //3
            quest.Add(new Questions("O někom, kdo si s velkou chutí a potěšením dopřává dobrého jídla a pití, lze říci, že si dává:", "do trumpety", "do kontrabantu", "do houslí", "do harfy", 3));
            quest.Add(new Questions("Jak dlouho trvala stoletá válka?", "116", "99", "100", "150", 3));
            //4
            quest.Add(new Questions("V knize Boženy Němcové „Babička“ se psi ze Starého bělidla jmenují:", "Sultán a Tyrl", "Slon a Tygr", "Sumec a Tuleň", "Satan a Troll", 4));
            quest.Add(new Questions("Naarden, místo posledního odpočinku Jana Ámose Komenského, je město:", "v Nizozemsku", "V Dánsku", "V Lucembursku", "V Blegii", 4));
            //5
            quest.Add(new Questions("Který chemický prvek má latinský název calcium?", "vápník", "železo", "uhlík", "dusík", 5));
            quest.Add(new Questions("Která země začala jako první na světě vyrábět porcelán? ", "Čína", "Francie", "Německo", "Rusko", 5));
            //6
            quest.Add(new Questions("Umělecká přehlídka zvaná kvadrienále se koná jednou za:", "čtyři roky", "dva roky", "tři roky", "pět let", 6));
            quest.Add(new Questions("Pro který módní doplněk byl podle řeckých pověstí vyslán hrdina Hérakles k Amazonkám", "pro pás", "pro náhrdelník", "pro čelenku", "pro zlatou jehlici", 6));
            //7
            quest.Add(new Questions("Baribal je:", "druh medvěda", "míčová hra", "ostrov v Karibiku", "Keltský vůdce", 7));
            quest.Add(new Questions("Na soutoku Labe s Orlicí leží jedno z nejstarších východočeských měst, které se nazývá:", "Hradec Králové", "Jaroměř", "Náchod", "Pardubice", 7));
            //8
            quest.Add(new Questions("Součástí moderního pětiboje není disciplína jednoho z uvedených sportovních odvětví. Kterého?", "kanoistika", "šerm", "jezdectví", "sportovní střelba", 8));
            quest.Add(new Questions("Jaký byl příbuzenský vztah mezi přírodovědcem Janem Evangelistou Purkyněm a malířem Karlem Purkyněm?", "otec a syn", "strýc a synovec", "sourozenci", "bratranci", 8));
            //9
            quest.Add(new Questions("Orlando Bloom hrál ve filmové trilogii „Pán prstenů“ roli:", "Legolase", "Gandalfa", "Aragorna", "Froda", 9));
            quest.Add(new Questions("Boris Godunov, titulní hrdina opery Modesta Petroviče Musorgskéhoo, se do dějin Ruska zapsal jako:", "car", "spisovatel", "pěvec", "spisovatel", 9));
            quest.Add(new Questions("V roce 1750 mělo v Evropě nejvíce obyvatel město:", "Londýn", "Berlín", "Paříž", "Madrid", 9));
            //10
            quest.Add(new Questions("Jak se jmenoval spolek států, který roku 1806 zřídil v Německu francouzský císař Napoleon I.?", "Rýnský spolek", "Německý spolek", "Rýnská republika", "Německá republika", 10));
            //11
            quest.Add(new Questions("Kterému z těchto států nepatřily Jónské ostrovy ležící u západního pobřeží Řecka?", "USA", "Rusku", "Francii", "Řecku", 11));
            quest.Add(new Questions("Jaké je křestní jméno herce titulního herce titulního hrdiny zvaného 'Harmonika' v kultovním Leoneho westernu 'Tenkrát na západě'?", "Frank", "Henry", "Steve", "Charles", 11));
            //12
            quest.Add(new Questions("Jak se nazývá šperkařská technika starých Slovanů, při níž se připájí drobná zrna drahého kovu na šperk?", "granulace", "vrubořez", "filigrán", "nielo", 12));
            //13
            quest.Add(new Questions("Ve kterém století vytvořil svá nejvýznamnější díla slavný architekt Le Corbusier?", "ve 20. století", "v 17. století", "v 18. století", "v 19. století ", 13));
            quest.Add(new Questions("Který časový údaj se vyskytuje v textu standardní verze písně The theatre od skupiny Pet Shop Boys", "půl jedenácté", "půl osmé", "půl deváté", "půl desáté", 13));
            //14
            quest.Add(new Questions("K zakladatelům etologie patří rakouský vědec?", "Konrad Lorenz", "herbert Spencer", "Alfred Marshall", "Richard Leakey", 14));
            //15
            quest.Add(new Questions("Které zvíře pojí Kafkovu povídku „Zpráva pro jistou akademii“ a povídku Arthura Conana Coyla „Šplhající muž“?", "opice", "kočka", "ryba", "brouk", 15));
            quest.Add(new Questions("Kolik soch miminek celkem šplhá nahoru a dolů po věži Žižkovského vysílače?", "10", "5", "12", "7", 15));
            string jsonToFile = JsonConvert.SerializeObject(quest, settings);
            File.WriteAllText(pathQuest, jsonToFile);*/
        }
        public Game()
        {


            InitializeComponent();
            hint1.Visibility = Visibility.Hidden;
            hint2.Visibility = Visibility.Hidden;
            hint3.Visibility = Visibility.Hidden;
            hint4.Visibility = Visibility.Hidden;
            //scores.Add(new Player("Arnold", 500));
            gamebegin.Play();
            PlayGame();
        }
        private void pade(object sender, RoutedEventArgs e)
        {
            int fifran = rand.Next(1, 3);
            if (fif == true)
            {
                fiftysound.Play();
                if (button2 == 1)
                {
                    C.Visibility = Visibility.Hidden;
                    B.Visibility = Visibility.Hidden;
                }
                else if (button2 == 2)
                {
                    A.Visibility = Visibility.Hidden;
                    D.Visibility = Visibility.Hidden;
                }
                else if (button2 == 3)
                {
                    B.Visibility = Visibility.Hidden;
                    A.Visibility = Visibility.Hidden;
                }
                else if (button2 == 4)
                {
                    D.Visibility = Visibility.Hidden;
                    C.Visibility = Visibility.Hidden;
                }
                Imgfif.Opacity = 0.4;
                fif = false;
            }

        }
        public void GameLose()
        {
            lose.Play();
            if (Checkpoint1 == true) {
                question.Content = "Prohrál jsi! Zvolil jsi špatnou odpověď, ale odcházíš s 10.000 Kč!!";
            }
            else if (Checkpoint2 == true) {
                question.Content = "Prohrál jsi! Zvolil jsi špatnou odpověď, ale odcházíš s 320.000 Kč!!";
            }
            else if (Checkpoint3 == true) {
                question.Content = "Prohrál jsi! Zvolil jsi špatnou odpověď, ale odcházíš s 320.000 Kč!!";
            }
            Input inputwindow = new Input("Napište vaše jméno", "Player");
            // inputwindow.ShowDialog();
            if (inputwindow.ShowDialog() == true) {
                string ranswer = inputwindow.Answer;
                Player playernew = new Player();
                playernew.Name = ranswer;
                playernew.Highscore = Highscore;
                scores.Add(playernew);
                //scores.Add(new Player(ranswer, Highscore));
                string jsonToFile = JsonConvert.SerializeObject(scores, settings);
                File.WriteAllText(pathHighScore, jsonToFile);
                //exit.Content = inputwindow.Answer;
            }
            A.Visibility = Visibility.Hidden;
            B.Visibility = Visibility.Hidden;
            C.Visibility = Visibility.Hidden;
            D.Visibility = Visibility.Hidden;
            fifty.Visibility = Visibility.Hidden;
            phone.Visibility = Visibility.Hidden;
            people.Visibility = Visibility.Hidden;
            back.Visibility = Visibility.Visible;
            exit.Visibility = Visibility.Visible;

        }

        
        private void Click(object sender, RoutedEventArgs e)
        {

            Button button = (Button)sender;
            if (AnswerActive == true) {
                if (sender is Button button1) {
                    if (button1.Content.ToString() == ranswer) {
                        if (diff != 15) {

                            rightanswer.Play();
                            button1.Background = Brushes.Green;
                            bb = button1;
                            fifty.Visibility = Visibility.Hidden;
                            phone.Visibility = Visibility.Hidden;
                            people.Visibility = Visibility.Hidden;
                            Next.Visibility = Visibility.Visible;
                            //button1.Background = Brushes.Green; 
                            diff++;
                            AnswerActive = false;
                        }
                        else {
                            A.Visibility = Visibility.Hidden;
                            B.Visibility = Visibility.Hidden;
                            C.Visibility = Visibility.Hidden;
                            D.Visibility = Visibility.Hidden;
                            GameWin();
                        }
                    }
                    else {
                        GameLose();
                        AnswerActive = false;
                    }
                }
            }
        }
        private void NextQ(object sender, RoutedEventArgs e)
        {
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString("#FF2196F3");
            bb.Background = brush;
            Next.Visibility = Visibility.Hidden;
            PlayGame();
        }
        private void BackToMenu(object sender, RoutedEventArgs e)
        {
            bool IntroA = false;
            MainWindow wind = new MainWindow();
            wind.Show();
            this.Close();

        }
        private void Phones(object sender, RoutedEventArgs e)
        {
            if (phon == true) {
                //peoples.Play();
                ImgPhone.Opacity = 0.4;
                Phone phonys = new Phone(questionara, rans, ans2, ans3, ans4);
                phonys.Show();
                phon = false;
            }
        }
        private void Peoplehelp(object sender, RoutedEventArgs e) {
            if (hlpPeople == true) {
                int chancehelp = rand.Next(0, 101);
                int chance1 = rand.Next(50, 60);
                int chance2 = rand.Next(0, 100 - chance1);
                int chance3 = rand.Next(0, 100 - chance1 - chance2);
                int chance4 = 100 - chance1 - chance2 - chance3;
                int bad = rand.Next(10, 30);
                int chance12 = rand.Next(0, 100 - bad);
                int chance22 = rand.Next(0, 100 - chance12 - bad);
                int chance32 = 100 - bad - chance12 - chance22;
                hint1.Visibility = Visibility.Visible;
                hint2.Visibility = Visibility.Visible;
                hint3.Visibility = Visibility.Visible;
                hint4.Visibility = Visibility.Visible;
                if (chancehelp > 30) {
                    if (button2 == 1) {
                        hint1.Content = chance1 + "%";
                        hint2.Content = chance3 + "%";
                        hint3.Content = chance2 + "%";
                        hint4.Content = chance4 + "%";
                    }
                    else if (button2 == 2) {
                        hint1.Content = chance4 + "%";
                        hint2.Content = chance2 + "%";
                        hint3.Content = chance1 + "%";
                        hint4.Content = chance3 + "%";
                    }
                    else if (button2 == 3) {
                        hint1.Content = chance3 + "%";
                        hint2.Content = chance2 + "%";
                        hint3.Content = chance4 + "%";
                        hint4.Content = chance1 + "%";
                    }
                    else if (button2 == 4) {
                        hint1.Content = chance3 + "%";
                        hint2.Content = chance1 + "%";
                        hint3.Content = chance4 + "%";
                        hint4.Content = chance2 + "%";
                    }
                }
                else if (chancehelp < 30) {
                    if (button2 == 1) {
                        hint1.Content = bad + "%";
                        hint2.Content = chance12 + "%";
                        hint3.Content = chance22 + "%";
                        hint4.Content = chance32 + "%";
                    }
                    else if (button2 == 2) {
                        hint1.Content = chance32 + "%";
                        hint2.Content = chance22 + "%";
                        hint3.Content = bad + "%";
                        hint4.Content = chance12 + "%";
                    }
                    else if (button2 == 3) {
                        hint1.Content = chance22 + "%";
                        hint2.Content = chance12 + "%";
                        hint3.Content = chance32 + "%";
                        hint4.Content = bad + "%";
                    }
                    else if (button2 == 4) {
                        hint1.Content = chance12 + "%";
                        hint2.Content = bad + "%";
                        hint3.Content = chance32 + "%";
                        hint4.Content = chance22 + "%";
                    }
                    
                }
                hlpPeople = false;
                ImgPeople.Opacity = 0.4;
            }
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        
       
    }
}
