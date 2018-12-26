using Game.Model;
using Game.Model.Interface;
using Game.Model.Service;
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
        string Tybing1Water = @"./Images2/Truba_11.png";
        string Tybing2Water = @"./Images2/Truba_22.png";
        string Tybing3Water = @"./Images2/Truba_33.png";
        string Tybing4Water = @"./Images2/Truba_44.png";

        string Tybing1NonWater = @"./Images/Truba_1.png";
        string Tybing2NonWater = @"./Images/Truba_2.png";
        string Tybing3NonWater = @"./Images/Truba_3.png";
        string Tybing4NonWater = @"./Images/Truba_4.png";

        string Brick = @"./Images/kirpih_2.png";


        DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            Level.SaveAllLevel();

            Map.Water += Water;

            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 1, 0);
        }

        /// <summary>
        /// Вызывается из класа Map
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <param name="isReset"></param>
        void Water(int y, int x, bool isReset = false)
        {
            var findTybing = GridMapTybing.Children.Cast<UIElement>().First(h => Grid.GetRow(h) == y && Grid.GetColumn(h) == x);
            var image = findTybing as Image;

                switch (Map.MapTybings[y, x].TubingType)
                {
                    case Model.Enum.SubjectType.Tybing1:
                        image.Source = new BitmapImage(new Uri(!isReset? Tybing1Water : Tybing1NonWater, UriKind.Relative));
                        break;
                    case Model.Enum.SubjectType.Tybing2:
                        image.Source = new BitmapImage(new Uri(!isReset ? Tybing2Water : Tybing2NonWater, UriKind.Relative));
                        break;
                    case Model.Enum.SubjectType.Tybing3:
                        image.Source = new BitmapImage(new Uri(!isReset ? Tybing3Water : Tybing3NonWater, UriKind.Relative));
                        break;
                    case Model.Enum.SubjectType.Tybing4:
                        image.Source = new BitmapImage(new Uri(!isReset ? Tybing4Water : Tybing4NonWater, UriKind.Relative));
                        break;
                    default:
                        break;
                }
        }

        void image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var image = sender as Image;
            if (image == null) throw new Exception("Хьюстон у нас проблемы");

            int y = Grid.GetRow(image);
            int x = Grid.GetColumn(image);

            var tybing = Map.MapTybings[y, x];
            if (tybing == null) throw new Exception("Хьюстон у нас проблемы");

            if ((tybing.SubjectState == Model.Enum.SubjectStateType.Entry) ||
                (tybing.SubjectState == Model.Enum.SubjectStateType.Exit) ||
                (tybing.TubingType == Model.Enum.SubjectType.Brick)) return;

            Map.MapTybings[y, x].Rotation = 1;

            image.RenderTransform = new RotateTransform(Map.MapTybings[y, x].Rotation);

        }

        void LetTheWater(object sender, RoutedEventArgs e)
        {
            if (GridMapTybing.Children.Count > 1)
            {
                Map.Reset();
                Map.Start();
                if (Map.IsCompletedAllConnectedTybing)
                {
                    timer.Stop();
                    MessageBox.Show("Ты выиграл!");
                    GridMapTybing.IsEnabled = false;
                    
                }
                else
                {
                    if (Map.TravelTime.Seconds == 0)
                    {
                        timer.Stop();
                        MessageBox.Show("Время вышло! вы проиграли");
                        GridMapTybing.IsEnabled = false;
                        
                    }
                    else
                    {
                        timer.Stop();
                        MessageBox.Show("Где-то течет..(( Подумай еще");
                        Map.Reset();
                        timer.Start();
                    }
                }
            }

            return;
        }

        void Lave2_Click(object sender, RoutedEventArgs e)
        {
            ClerGrid();
            Map.Level(2);
            DrowGrid();

            timer.Start();
        }

        void Level1_Click(object sender, RoutedEventArgs e)
        {
            ClerGrid();
            Map.Level(1);
            DrowGrid();
            timer.Start();
        }

        void DrowGrid()
        {
            //GridMapTybing.ShowGridLines = true;

            for (int y = 0; y < Map.SizeX; y++)
            {
                var col = new ColumnDefinition();
                col.Width = new GridLength(1, GridUnitType.Star);
                GridMapTybing.ColumnDefinitions.Add(col);
            }

            for (int x = 0; x < Map.SizeY; x++)
            {
                var row = new RowDefinition();
                row.Height = new GridLength(1, GridUnitType.Star);
                GridMapTybing.RowDefinitions.Add(row);
            }

            for (int y = 0; y < Map.SizeY; y++)
            {
                for (int x = 0; x < Map.SizeX; x++)
                {
                    var image = new Image();
                    image.RenderTransformOrigin = new System.Windows.Point() { X = 0.5, Y = 0.5 };
                    //image.Margin = new Thickness(10);

                    switch (Map.MapTybings[y, x].TubingType)
                    {
                        case Model.Enum.SubjectType.Tybing1:
                            image.Source = new BitmapImage(new Uri(Tybing1NonWater, UriKind.Relative));
                            image.RenderTransform = new RotateTransform(Map.MapTybings[y, x].Rotation);
                            break;
                        case Model.Enum.SubjectType.Tybing2:
                            image.Source = new BitmapImage(new Uri(Tybing2NonWater, UriKind.Relative));
                            break;
                        case Model.Enum.SubjectType.Tybing3:
                            image.Source = new BitmapImage(new Uri(Tybing3NonWater, UriKind.Relative));
                            break;
                        case Model.Enum.SubjectType.Tybing4:
                            image.Source = new BitmapImage(new Uri(Tybing4NonWater, UriKind.Relative));
                            break;
                        case Model.Enum.SubjectType.Brick:
                            image.Source = new BitmapImage(new Uri(Brick, UriKind.Relative));
                            image.Stretch = Stretch.Fill;
                            break;
                        default:
                            break;
                    }

                    image.MouseLeftButtonUp += image_MouseLeftButtonUp;
                    Grid.SetRow(image, y);
                    Grid.SetColumn(image, x);
                    GridMapTybing.Children.Add(image);
                }
            }
        }

        void ClerGrid()
        {
            GridMapTybing.IsEnabled = true;
            GridMapTybing.Children.Clear();
            GridMapTybing.RowDefinitions.Clear();
            GridMapTybing.ColumnDefinitions.Clear();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            Time.Header = string.Format("|Время| {0}",Map.TravelTime.ToString(@"mm\:ss"));

            if (Map.TravelTime.Ticks == 0)
            {
                timer.Stop();
                LetTheWater(null, null);
                return;
            }

            Map.TravelTime = Map.TravelTime.Add(TimeSpan.FromSeconds(-1));
        }
    }
}
