using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace rozhrani {
    /// <summary>
    /// Interakční logika pro Phone.xaml
    /// </summary>
    public partial class Phone : Window {
        static string peoplepath = @"../../Sound/people.wav";
        Random rand = new Random();
        public DispatcherTimer timer = new DispatcherTimer();
        public DispatcherTimer timer2 = new DispatcherTimer();
        SoundPlayer peoples = new SoundPlayer(peoplepath);
        List<People> people = new List<People>();
        string question = "";
        string ChosenName = "";
        string rans = "";
        string ans2 = "";
        string ans3 = "";
        string ans4 = "";
        private int add = 1;
        public string questiona = "";
        public Phone(string gg, string ranss, string anss2, string anss3, string anss4) {
            InitializeComponent();
            GeneratePeople();
            int cpeople = people.Count();
            int whname1 = rand.Next(0, cpeople);
            int whname2 = rand.Next(0, cpeople);
            int whname3 = rand.Next(0, cpeople);
            int whname4 = rand.Next(0, cpeople);
            mess1.Visibility = Visibility.Hidden;
            mess2.Visibility = Visibility.Hidden;
            mess3.Visibility = Visibility.Hidden;
            mess35.Visibility = Visibility.Hidden;
            mess4.Visibility = Visibility.Hidden;
            mess5.Visibility = Visibility.Hidden;
            mess6.Visibility = Visibility.Hidden;
            name1.Content = people[whname1].Name;
            name2.Content = people[whname2].Name;
            name3.Content = people[whname3].Name;
            name4.Content = people[whname4].Name;
            question = gg;
            rans = ranss;
            ans2 = anss2;
            ans3 = anss3;
            ans4 = anss4;
            //SelectPerson();
            //mess1.Content = gg;
            //exit();

        }
        public void GeneratePeople() {
            people.Add(new People("Karel"));
            people.Add(new People("Robert"));
            people.Add(new People("Jaroslav"));
            people.Add(new People("Marcela"));
            people.Add(new People("Tomáš"));
            people.Add(new People("Vladimír"));
            people.Add(new People("Jozef"));
            people.Add(new People("Rostislav"));
            people.Add(new People("Lukáš"));
            people.Add(new People("David"));
        }
        public void SelectPerson(object sender, RoutedEventArgs e) {
            Button button = (Button)sender;
            if (sender is Button button1) {

                ChosenName = button1.Content.ToString();
                //ButStack.Visibility = Visibility.Hidden;
                peoples.Play();
                exit();
            }
        }
        public void exit() {
            ButStack.Visibility = Visibility.Hidden;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 17000);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

            timer2.Interval = TimeSpan.FromSeconds(1);
            timer2.Tick += new EventHandler(timer_Tick2);
            timer2.Start();


        }

        void timer_Tick(object sender, EventArgs e) {
            mess1.Visibility = Visibility.Visible;
            this.Close();
            timer.Stop();
        }
        void timer_Tick2(object sender, EventArgs e) {
            int rend = rand.Next(0, 101);
            mess1.Visibility = Visibility.Visible;
            add++;
            if (add == 3) {
                mess1.Visibility = Visibility.Visible;
            }
            if (add == 5) {
                mess2.Content = ChosenName + ": No dobře, tak povídej";
                mess2.Visibility = Visibility.Visible;
            }
            if (add == 7) {
                mess3.Visibility = Visibility.Visible;
                mess35.Visibility = Visibility.Visible;
                mess3.Content = "Ty: Otázka zní: " + question;
                mess35.Content = ans4+ " " + " " + ans2+ " " + rans + " " + ans3;
            }
            if (add == 10) {
                mess4.Content = ChosenName + ": Nech mě přemýšlet...";
                mess4.Visibility = Visibility.Visible;
            }
            if (add == 13) {
                if (rend > 25) {
                    mess5.Visibility = Visibility.Visible;
                    mess5.Content = ChosenName +": Myslím si že správná odpověď je " + rans;
                }else {
                    mess5.Visibility = Visibility.Visible;
                    mess5.Content = ChosenName + ": Myslím si že správná odpověď je " + ans2;
                }
            if (add == 15) {
                    mess6.Visibility = Visibility.Visible;
                    mess6.Content = "Ty: Děkuji za pomoc!";
            }

            }
        }






    }
}
