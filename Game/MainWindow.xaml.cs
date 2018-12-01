using Game.Model;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Game
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        string z="";
        int time_min, time_sec=59;
      
        public MainWindow()
        {
            timer = new DispatcherTimer();
            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            InitializeComponent();
        }

        void timer_Tick(object sender, EventArgs e)
        {

            T.Header = string.Format("|Время| {0}:{1}", time_min, time_sec);
            if (time_sec == 0 && time_min == 0)
            {
                timer.Stop();
                result_vote();
                return;
            }
            if (time_sec == 0)
            {
                time_sec = 60;
                time_min--;

            }
            time_sec--;

        }

        private void Window_Initialized(object sender, EventArgs e) //   Main
        {
            
            z = "level_1.txt";
            time_min = timer_level(z);
            timer.Start();
            Map map = new Map(z);
            GameMapView.Source = map;

        }

        private void Lave2_Click(object sender, RoutedEventArgs e)
        {
            z = "level_2.txt";
            time_min = timer_level(z);
            timer.Start();
            Map map = new Map(z);
            GameMapView.Source = map;
        }

        private void Lavel3_Click(object sender, RoutedEventArgs e)
        {
            z = "level_3.txt";
            time_min = timer_level(z);
            timer.Start();
            Map map = new Map(z);
            GameMapView.Source = map;

        }

        private void Level_Click(object sender, RoutedEventArgs e)
        {
            z = "level_1.txt";
            time_min = timer_level(z);
            timer.Start();
            Map map = new Map(z);
            GameMapView.Source = map;
        }      

        private void Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            result_vote();
        }

        void result_vote()
        {            
            GameMapView.ZAlivka(0, 1);  //Пошло соединение труб       
            string message = "Поздравляю вы выиграли!";
            string message1 = "Вы проиграли!";
            if (GameMapView.Finih() == 1) { MessageBoxResult result = MessageBox.Show(message, "Вай,Вай,Вай !"); }
            if (GameMapView.Finih() == 0) { MessageBoxResult result = MessageBox.Show(message1, "I'm sorry"); }
           
            time_min = timer_level(z);
            timer.Start();
            Map map = new Map(z);
            GameMapView.Source = map;
        }

        int timer_level(string str)
        {
            time_min = 0; time_sec = 59;
            return int.Parse(str.Substring(6,1))-1;
        }
 
    }
}
