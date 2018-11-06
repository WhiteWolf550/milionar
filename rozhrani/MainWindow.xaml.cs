using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace rozhrani {
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        int diff = 1;
        bool fif = true;
        int poradi = 1;
        int button2;
        string ranswer = "";
        bool Checkpoint1 = false;
        bool Checkpoint2 = false;
        bool Checkpoint3 = false;
        static bool Intro_Alive = true;
        static string pathintro = @"../../Sound/intro_music.wav";
        static string path2 = @"../../Sound/lose2.wav";
        static string gamewin = @"../../Sound/win.wav";
        static string pathright = @"../../Sound/right.wav";
        static string path3 = @"../../Sound/lose.wav";
        static string music1path = @"../../Sound/music1.wav";
        static string music2path = @"../../Sound/music2.wav";
        static string music3path = @"../../Sound/music3.wav";
        static string fiftypath = @"../../Sound/fifty.wav";
        static string musicfinalpath = @"../../Sound/musicfinal.wav";
        Random rand = new Random();
        Game win2 = new Game();
        SoundPlayer intro = new SoundPlayer(pathintro);
        SoundPlayer rightanswer = new SoundPlayer(pathright);
        SoundPlayer lose = new SoundPlayer(path2);
        SoundPlayer fiftysound = new SoundPlayer(fiftypath);
        SoundPlayer wingame = new SoundPlayer(gamewin);
        SoundPlayer gamebegin = new SoundPlayer(path3);
        SoundPlayer music1 = new SoundPlayer(music1path);
        SoundPlayer music2 = new SoundPlayer(music2path);
        SoundPlayer music3 = new SoundPlayer(music3path);
        SoundPlayer musicfinal = new SoundPlayer(musicfinalpath);
        List<Questions> quest = new List<Questions>();
        
        public MainWindow() {
            InitializeComponent();
            Intro.Volume = 0;
            Intro.LoadedBehavior = MediaState.Manual;
            Intro.Play();
            if (Intro_Alive == true) {
                Opone.Visibility = Visibility.Visible;
                Intro.Visibility = Visibility.Visible;
                //Intro.Volume = 0;
                intro.Play();
                Intro.MediaEnded += media_MediaEnded;
                Intro_Alive = false;
            }else {
                videoskip.Visibility = Visibility.Hidden;
                intro.Play();
            }
            //Game();
        }
        private void media_MediaEnded(object sender, RoutedEventArgs e)
        {
            Intro.Visibility = Visibility.Hidden;
            main.Visibility = Visibility.Visible;
            Opecity();
            //intro.Play();

        }
        private void skipvideo(object sender, RoutedEventArgs e) {
            Intro.Stop();
            videoskip.Visibility = Visibility.Hidden;
            Intro.Visibility = Visibility.Hidden;
            Opecity();
        }
        public void Opecity()
        {
            var animation = new DoubleAnimation
            {
                To = 0,
                BeginTime = TimeSpan.FromSeconds(1),
                Duration = TimeSpan.FromSeconds(2),
                FillBehavior = FillBehavior.Stop
            };

            animation.Completed += (s, a) => Opone.Visibility = Visibility.Hidden;

            Opone.BeginAnimation(UIElement.OpacityProperty, animation);
        }
        private void StartGame(object sender, RoutedEventArgs e)
        {
            
            win2.Show();
            this.Close();
            gamebegin.Play();
            
        }
        private void ExitGame(object sender, RoutedEventArgs e) {
            System.Windows.Application.Current.Shutdown();
        }

        private void HighScore(object sender, RoutedEventArgs e) {
            HighScoreWindow scorewin = new HighScoreWindow();
            scorewin.Show();
        }


    }
}
