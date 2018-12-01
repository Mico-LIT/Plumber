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

namespace Game.View
{
    /// <summary>
    /// Логика взаимодействия для MapView.xaml
    /// </summary>
    public partial class MapView : UserControl
    {
        public MapView()
        {
            InitializeComponent();
        }

        Map map;


        public Map Source
        {
            get
            {
                return map;
            }
            set
            {
                map = value;
                Redraw();
            }
        }

        void Redraw()
        {
            MapGrid.ColumnDefinitions.Clear();
            MapGrid.RowDefinitions.Clear();
            MapGrid.Children.Clear();
            #region @"./Images/Truba_(1,2,3).png"
            for (int x = 0; x < map.SizeX; x++)
                MapGrid.ColumnDefinitions.Add(new ColumnDefinition());
            for (int y = 0; y < map.SizeY; y++)
                MapGrid.RowDefinitions.Add(new RowDefinition());

            BitmapImage Truba_1Bitmap = new BitmapImage();
            Truba_1Bitmap.BeginInit();
            Truba_1Bitmap.UriSource = new Uri(@"./Images/Truba_1.png", UriKind.Relative);
            Truba_1Bitmap.EndInit();

            BitmapImage Truba_2Bitmap = new BitmapImage();
            Truba_2Bitmap.BeginInit();
            Truba_2Bitmap.UriSource = new Uri(@"./Images/Truba_2.png", UriKind.Relative);
            Truba_2Bitmap.EndInit();

            BitmapImage Truba_3Bitmap = new BitmapImage();
            Truba_3Bitmap.BeginInit();
            Truba_3Bitmap.UriSource = new Uri(@"./Images/Truba_3.png", UriKind.Relative);
            Truba_3Bitmap.EndInit();

            BitmapImage kirpih_2Bitmap = new BitmapImage();
            kirpih_2Bitmap.BeginInit();
            kirpih_2Bitmap.UriSource = new Uri(@"./Images/kirpih_2.png", UriKind.Relative);
            kirpih_2Bitmap.EndInit();

            BitmapImage Truba_11Bitmap = new BitmapImage();
            Truba_11Bitmap.BeginInit();
            Truba_11Bitmap.UriSource = new Uri(@"./Images2/Truba_11.png", UriKind.Relative);
            Truba_11Bitmap.EndInit();

            BitmapImage Truba_22Bitmap = new BitmapImage();
            Truba_22Bitmap.BeginInit();
            Truba_22Bitmap.UriSource = new Uri(@"./Images2/Truba_22.png", UriKind.Relative);
            Truba_22Bitmap.EndInit();

            BitmapImage Truba_33Bitmap = new BitmapImage();
            Truba_33Bitmap.BeginInit();
            Truba_33Bitmap.UriSource = new Uri(@"./Images2/Truba_33.png", UriKind.Relative);
            Truba_33Bitmap.EndInit();



   #endregion
            for (int x = 0; x < map.SizeX; x++)
                for (int y = 0; y < map.SizeY; y++)
                {
                    Frame image = new Frame();
                    image.BorderThickness = new Thickness(1);
                    var celltype = map[x,y].CellType;                           
                    switch (celltype)
                    {
                        case CellType.Truba_1:
                            image.Background = new ImageBrush(Truba_1Bitmap);                          
                                image.RenderTransform = new RotateTransform() { Angle = map[x, y].Rotation };
                                image.Tag = new Coordinate() { X = x, Y = y };

                                Povorot(image);                             

                            break;
                        case CellType.Truba_2:
                            image.Background = new ImageBrush(Truba_2Bitmap);
                            image.RenderTransform = new RotateTransform()  {Angle = map[x, y].Rotation};  
                            image.Tag = new Coordinate() { X = x, Y = y };

                            Povorot(image);   

                            break;
                        case CellType.Truba_3:
                            image.Background = new ImageBrush(Truba_3Bitmap);
                            image.RenderTransform = new RotateTransform() { Angle = map[x, y].Rotation };           
                            image.Tag = new Coordinate() { X = x, Y = y };

                            Povorot(image); 

                            break;
                        case CellType.kirpih_2:
                            image.Background = new ImageBrush(kirpih_2Bitmap);
                         
                            break;
                        case CellType.Truba_11:
                            image.Background = new ImageBrush(Truba_11Bitmap);
                            image.RenderTransform = new RotateTransform() { Angle = map[x, y].Rotation };
                            blue_Z(image); 
                         
                            break;
                        case CellType.Truba_22:
                            image.Background = new ImageBrush(Truba_22Bitmap);
                            image.RenderTransform = new RotateTransform() { Angle = map[x, y].Rotation };
                            blue_Z(image); 
                            break;
                            case CellType.Truba_33:
                            image.Background = new ImageBrush(Truba_33Bitmap);
                            image.RenderTransform = new RotateTransform() { Angle = map[x, y].Rotation };
                            blue_Z(image); 
                            break;
                        default:
                            break;
                    }
                    MapGrid.Children.Add(image);
                    Grid.SetColumn(image, x);
                    Grid.SetRow(image, y);

                    
                }
        }

        void RefreshRotation(Frame image)
        {
            var transform = image.RenderTransform as RotateTransform;
            transform.CenterX = image.ActualWidth * 0.5;
            transform.CenterY = image.ActualHeight * 0.5;     
        }

        void image_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var image = sender as Frame;
            RefreshRotation(image);

        }

        void image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Frame image = sender as Frame;
            Coordinate c = image.Tag as Coordinate;                    // координаты трубы            
           
            map[c.X, c.Y].Rotation += 90;
            if (map[c.X, c.Y].Rotation >= 360) map[c.X, c.Y].Rotation -= 360;
#region
            switch (map[c.X, c.Y].CellType)
            {
                case CellType.Truba_1:
                    if (map[c.X, c.Y].Rotation==0 )  
                    {     map[c.X, c.Y].x1 = 1;
                          map[c.X, c.Y].x2 = 0;
                          map[c.X, c.Y].x3 = 1;
                          map[c.X, c.Y].x4 = 0;
                    }
                    if (map[c.X, c.Y].Rotation == 90 )
                    {
                        map[c.X, c.Y].x1 = 0;
                        map[c.X, c.Y].x2 = 1;
                        map[c.X, c.Y].x3 = 0;
                        map[c.X, c.Y].x4 = 1;
                    }
                    if (map[c.X, c.Y].Rotation == 180)
                    {
                        map[c.X, c.Y].x1 = 1;
                        map[c.X, c.Y].x2 = 0;
                        map[c.X, c.Y].x3 = 1;
                        map[c.X, c.Y].x4 = 0;
                    }
                    if (map[c.X, c.Y].Rotation == 270)
                    {
                        map[c.X, c.Y].x1 = 0;
                        map[c.X, c.Y].x2 = 1;
                        map[c.X, c.Y].x3 = 0;
                        map[c.X, c.Y].x4 = 1;
                    }

                    break;
                case CellType.Truba_2:
                    if (map[c.X, c.Y].Rotation == 0)
                    {
                        map[c.X, c.Y].x1 = 1;
                        map[c.X, c.Y].x2 = 1;
                        map[c.X, c.Y].x3 = 0;
                        map[c.X, c.Y].x4 = 0;
                    }
                    if (map[c.X, c.Y].Rotation == 90)
                    {
                        map[c.X, c.Y].x1 = 0;
                        map[c.X, c.Y].x2 = 1;
                        map[c.X, c.Y].x3 = 1;
                        map[c.X, c.Y].x4 = 0;
                    }
                    if (map[c.X, c.Y].Rotation == 180)
                    {
                        map[c.X, c.Y].x1 = 0;
                        map[c.X, c.Y].x2 = 0;
                        map[c.X, c.Y].x3 = 1;
                        map[c.X, c.Y].x4 = 1; ;
                    }
                    if (map[c.X, c.Y].Rotation == 270)
                    {
                        map[c.X, c.Y].x1 = 1;
                        map[c.X, c.Y].x2 = 0;
                        map[c.X, c.Y].x3 = 0;
                        map[c.X, c.Y].x4 = 1;
                    }
                    break;
                case CellType.Truba_3:
                    if (map[c.X, c.Y].Rotation == 0)
                    {
                        map[c.X, c.Y].x1 = 1;
                        map[c.X, c.Y].x2 = 1;
                        map[c.X, c.Y].x3 = 0;
                        map[c.X, c.Y].x4 = 1;
                    }
                    if (map[c.X, c.Y].Rotation == 90)
                    {
                        map[c.X, c.Y].x1 = 1;
                        map[c.X, c.Y].x2 = 1;
                        map[c.X, c.Y].x3 = 1;
                        map[c.X, c.Y].x4 = 0;
                    }
                    
                    if (map[c.X, c.Y].Rotation == 180)
                    {
                        map[c.X, c.Y].x1 = 0;
                        map[c.X, c.Y].x2 = 1;
                        map[c.X, c.Y].x3 = 1;
                        map[c.X, c.Y].x4 = 1;
                    }
                        if (map[c.X, c.Y].Rotation == 270)
                        {
                            map[c.X, c.Y].x1 = 1;
                            map[c.X, c.Y].x2 = 0;
                            map[c.X, c.Y].x3 = 1;
                            map[c.X, c.Y].x4 = 1;
                        }
                    break;
               
                default:
                    break;
            }
      
            
#endregion

            Redraw();
            image.SizeChanged -= image_SizeChanged;
            image.MouseDown -= image_MouseDown;  
            

        }
        void Povorot(Frame image)
        {
            
            RefreshRotation(image);
            image.SizeChanged += image_SizeChanged;
            image.MouseDown += image_MouseDown;    
        }
        void blue_Z(Frame image)
        {
            RefreshRotation(image);
            image.SizeChanged += image_SizeChanged;
        }
        public void ZAlivka(int x,int y)
        {
            CellType type=CellType.kirpih_2;
            CellType[] type1={CellType.Truba_11,CellType.Truba_22,CellType.Truba_33};

            switch (map[x,y].CellType)
            {
                case CellType.Truba_1:                   

                    if (map[x, y].Rotation == 0 || map[x, y].Rotation == 180) /* Либо вверх либо вниз */
                    {
                        //Условия что бы не выйти за границы массива 
                        #region   map[x, y-1]
                        if (map[x, y-1].CellType!=CellType.kirpih_2)
                        {                           
                            
                                if (map[x, y - 1].CellType == type1[0]) type = type1[0];
                                if (map[x, y - 1].CellType == type1[1]) type = type1[1];
                                if (map[x, y - 1].CellType == type1[2]) type = type1[2];
                                if (type == CellType.Truba_11)
                                {
                                    if (map[x, y - 1].Rotation == 0 || map[x, y - 1].Rotation == 180)
                                    {
                                        map[x, y].CellType = CellType.Truba_11;
                                        Redraw();
                               
                                        ZAlivka(x, y + 1);
                                        
                                        break;
                                    }
                                }
                                if (type == CellType.Truba_22) 
                                {
                                    if (map[x, y - 1].Rotation == 90 || map[x, y - 1].Rotation == 180) 
                                    {
                                        map[x, y].CellType = CellType.Truba_11;
                                        Redraw();
                                        ZAlivka(x, y + 1);
                                        break;
                                    }

                                }
                                if (type == CellType.Truba_33) 
                                {
                                    if (map[x, y - 1].Rotation == 90 || map[x, y - 1].Rotation == 180 || map[x, y - 1].Rotation == 270)
                                    {
                                        map[x, y].CellType = CellType.Truba_11;
                                        Redraw();
                                        ZAlivka(x, y + 1);
                                        break;
                                    }
                                } type = CellType.kirpih_2;     
                            
                        }                  
                        
#endregion            
                        #region   map[x, y+1]
                        if (map[x, y+1].CellType!=CellType.kirpih_2)
                        {

                            if (map[x, y + 1].CellType == type1[0]) type = type1[0];
                            if (map[x, y + 1].CellType == type1[1]) type = type1[1];
                            if (map[x, y + 1].CellType == type1[2]) type = type1[2];
                            if (type == CellType.Truba_11)
                            {
                                if (map[x, y + 1].Rotation == 0 || map[x, y + 1].Rotation == 180)
                                {
                                    map[x, y].CellType = CellType.Truba_11;
                                    Redraw();
                                    ZAlivka(x, y - 1);
                                    break;
                                }
                            }
                            if (type == CellType.Truba_22)
                            {
                                if (map[x, y + 1].Rotation == 0 || map[x, y + 1].Rotation == 270)
                                {
                                    map[x, y].CellType = CellType.Truba_11;
                                    Redraw();
                                    ZAlivka(x, y - 1);
                                    break;
                                }

                            }
                            if (type == CellType.Truba_33)
                            {
                                if (map[x, y + 1].Rotation == 0 || map[x, y + 1].Rotation == 270 || map[x, y + 1].Rotation == 90)
                                {
                                    map[x, y].CellType = CellType.Truba_11;
                                    Redraw();
                                    ZAlivka(x, y - 1);
                                    break;
                                }
                            } type = CellType.kirpih_2;

                        }

                        #endregion            
                    }


                    if (map[x, y].Rotation == 90 || map[x, y].Rotation == 270) /* Либо враво либо влево */
                    {
                        //Условия что бы не выйти за границы массива 

                        #region map[x - 1, y]
                        if (map[x - 1, y].CellType == type1[0]) type = type1[0];
                        if (map[x - 1, y].CellType == type1[1]) type = type1[1];
                        if (map[x - 1, y].CellType == type1[2]) type = type1[2];
                        if (type == CellType.Truba_11)
                        {
                            if (map[x - 1, y].Rotation == 90 || map[x - 1, y].Rotation == 270)
                            {
                                map[x, y].CellType = CellType.Truba_11;
                                Redraw();
                                ZAlivka(x + 1, y);
                                break;
                            }
                        }
                        if (type == CellType.Truba_22)
                        {
                            if (map[x - 1, y].Rotation == 0 || map[x - 1, y].Rotation == 90)
                            {
                                map[x, y].CellType = CellType.Truba_11;
                                Redraw();
                                ZAlivka(x + 1, y);
                                break;
                            }

                        }
                        if (type == CellType.Truba_33)
                        {
                            if (map[x - 1, y].Rotation == 0 || map[x - 1, y].Rotation == 90 || map[x - 1, y].Rotation == 180)
                            {
                                map[x, y].CellType = CellType.Truba_11;
                                Redraw();
                                ZAlivka(x + 1, y);
                                break;
                            }
                        } type = CellType.kirpih_2;              
                        #endregion
                        #region map[x + 1, y]
                        if (map[x + 1, y].CellType == type1[0]) type = type1[0];
                        if (map[x + 1, y].CellType == type1[1]) type = type1[1];
                        if (map[x + 1, y].CellType == type1[2]) type = type1[2];
                        if (type == CellType.Truba_11)
                        {
                            if (map[x + 1, y].Rotation == 90 || map[x + 1, y].Rotation == 270)
                            {
                                map[x, y].CellType = CellType.Truba_11;
                                Redraw();
                                ZAlivka(x - 1, y);
                                break;
                            }
                        }
                        if (type == CellType.Truba_22)
                        {
                            if (map[x + 1, y].Rotation == 180 || map[x + 1, y].Rotation == 270)
                            {
                                map[x, y].CellType = CellType.Truba_11;
                                Redraw();
                                ZAlivka(x - 1, y);
                                break;
                            }

                        }
                        if (type == CellType.Truba_33)
                        {
                            if (map[x + 1, y].Rotation == 180 || map[x + 1, y].Rotation == 270 || map[x + 1, y].Rotation == 0)
                            {
                                map[x, y].CellType = CellType.Truba_11;
                                Redraw();
                                ZAlivka(x - 1, y);
                                break;
                            }
                        } type = CellType.kirpih_2;          
#endregion                   
                    }

                    break;
                case CellType.Truba_2:
                    if (map[x, y].Rotation == 0 )
                    {
                        //Условия что бы не выйти за границы массива
                        #region   map[x, y-1]
                       
                            if (map[x, y - 1].CellType == type1[0]) type = type1[0];
                            if (map[x, y - 1].CellType == type1[1]) type = type1[1];
                            if (map[x, y - 1].CellType == type1[2]) type = type1[2];
                            if (type == CellType.Truba_11)
                            {
                                if (map[x, y - 1].Rotation == 0 || map[x, y - 1].Rotation == 180)
                                {
                                    map[x, y].CellType = CellType.Truba_22;
                                    Redraw();
                                    ZAlivka(x+1, y);
                                    break;
                                }
                            }
                            if (type == CellType.Truba_22)
                            {
                                if (map[x, y - 1].Rotation == 90 || map[x, y - 1].Rotation == 180)
                                {
                                    map[x, y].CellType = CellType.Truba_22;
                                    Redraw();
                                    ZAlivka(x + 1, y);
                                    break;
                                }

                            }
                            if (type == CellType.Truba_33)
                            {
                                if (map[x, y - 1].Rotation == 90 || map[x, y - 1].Rotation == 180 || map[x, y - 1].Rotation == 270)
                                {
                                    map[x, y].CellType = CellType.Truba_22;
                                    Redraw();
                                    ZAlivka(x + 1, y);
                                    break;
                                }
                            } type = CellType.kirpih_2;
#endregion  
                        #region map[x + 1, y]
                            if (map[x + 1, y].CellType == type1[0]) type = type1[0];
                            if (map[x + 1, y].CellType == type1[1]) type = type1[1];
                            if (map[x + 1, y].CellType == type1[2]) type = type1[2];
                            if (type == CellType.Truba_11)
                            {
                                if (map[x + 1, y].Rotation == 90 || map[x + 1, y].Rotation == 270)
                                {
                                    map[x, y].CellType = CellType.Truba_22;
                                    Redraw();
                                    ZAlivka(x, y - 1);
                                    break;
                                }
                            }
                            if (type == CellType.Truba_22)
                            {
                                if (map[x + 1, y].Rotation == 180 || map[x + 1, y].Rotation == 270)
                                {
                                    map[x, y].CellType = CellType.Truba_22;
                                    Redraw();
                                    ZAlivka(x, y - 1);
                                    break;
                                }

                            }
                            if (type == CellType.Truba_33)
                            {
                                if (map[x + 1, y].Rotation == 180 || map[x + 1, y].Rotation == 270 || map[x + 1, y].Rotation == 0)
                                {
                                    map[x, y].CellType = CellType.Truba_22;
                                    Redraw();
                                    ZAlivka(x, y - 1);
                                    break;
                                }
                            } type = CellType.kirpih_2;  
                            #endregion                   
                    }

                    if (map[x, y].Rotation == 90)
                    {
                        //Условия что бы не выйти за границы массива
                        #region   map[x, y+1]

                        if (map[x, y + 1].CellType == type1[0]) type = type1[0];
                        if (map[x, y + 1].CellType == type1[1]) type = type1[1];
                        if (map[x, y + 1].CellType == type1[2]) type = type1[2];
                        if (type == CellType.Truba_11)
                        {
                            if (map[x, y + 1].Rotation == 0 || map[x, y + 1].Rotation == 180)
                            {
                                map[x, y].CellType = CellType.Truba_22;
                                Redraw();
                                ZAlivka(x + 1, y);
                                break;
                            }
                        }
                        if (type == CellType.Truba_22)
                        {
                            if (map[x, y + 1].Rotation == 0 || map[x, y + 1].Rotation == 270)
                            {
                                map[x, y].CellType = CellType.Truba_22;
                                Redraw();
                                ZAlivka(x + 1, y);
                                break;
                            }

                        }
                        if (type == CellType.Truba_33)
                        {
                            if (map[x, y + 1].Rotation == 0 || map[x, y + 1].Rotation == 270 || map[x, y + 1].Rotation == 90)
                            {
                                map[x, y].CellType = CellType.Truba_22;
                                Redraw();
                                ZAlivka(x + 1, y);
                                break;
                            }
                        } type = CellType.kirpih_2;
                        #endregion
                        #region map[x + 1, y]
                        if (map[x + 1, y].CellType == type1[0]) type = type1[0];
                        if (map[x + 1, y].CellType == type1[1]) type = type1[1];
                        if (map[x + 1, y].CellType == type1[2]) type = type1[2];
                        if (type == CellType.Truba_11)
                        {
                            if (map[x + 1, y].Rotation == 90 || map[x + 1, y].Rotation == 270)
                            {
                                map[x, y].CellType = CellType.Truba_22;
                                Redraw();
                                ZAlivka(x, y + 1);
                                break;
                            }
                        }
                        if (type == CellType.Truba_22)
                        {
                            if (map[x + 1, y].Rotation == 180 || map[x + 1, y].Rotation == 270)
                            {
                                map[x, y].CellType = CellType.Truba_22;
                                Redraw();
                                ZAlivka(x, y + 1);
                                break;
                            }

                        }
                        if (type == CellType.Truba_33)
                        {
                            if (map[x + 1, y].Rotation == 180 || map[x + 1, y].Rotation == 270 || map[x + 1, y].Rotation == 0)
                            {
                                map[x, y].CellType = CellType.Truba_22;
                                Redraw();
                                ZAlivka(x, y + 1);
                                break;
                            }
                        } type = CellType.kirpih_2;   
                        #endregion                   

                    }
                    if (map[x, y].Rotation == 180) 
                    {
                        //Условия что бы не выйти за границы массива
                        #region   map[x, y+1]

                        if (map[x, y + 1].CellType == type1[0]) type = type1[0];
                        if (map[x, y + 1].CellType == type1[1]) type = type1[1];
                        if (map[x, y + 1].CellType == type1[2]) type = type1[2];
                        if (type == CellType.Truba_11)
                        {
                            if (map[x, y + 1].Rotation == 0 || map[x, y + 1].Rotation == 180)
                            {
                                map[x, y].CellType = CellType.Truba_22;
                                Redraw();
                                ZAlivka(x - 1, y);
                                break;
                            }
                        }
                        if (type == CellType.Truba_22)
                        {
                            if (map[x, y + 1].Rotation == 0 || map[x, y + 1].Rotation == 270)
                            {
                                map[x, y].CellType = CellType.Truba_22;
                                Redraw();
                                ZAlivka(x - 1, y);
                                break;
                            }

                        }
                        if (type == CellType.Truba_33)
                        {
                            if (map[x, y + 1].Rotation == 0 || map[x, y + 1].Rotation == 270 || map[x, y + 1].Rotation == 90)
                            {
                                map[x, y].CellType = CellType.Truba_22;
                                Redraw();
                                ZAlivka(x - 1, y);
                                break;
                            }
                        } type = CellType.kirpih_2;
                        #endregion
                        #region map[x - 1, y]
                        if (map[x - 1, y].CellType == type1[0]) type = type1[0];
                        if (map[x - 1, y].CellType == type1[1]) type = type1[1];
                        if (map[x - 1, y].CellType == type1[2]) type = type1[2];
                        if (type == CellType.Truba_11)
                        {
                            if (map[x - 1, y].Rotation == 90 || map[x - 1, y].Rotation == 270)
                            {
                                map[x, y].CellType = CellType.Truba_22;
                                Redraw();
                                ZAlivka(x, y + 1);
                                break;
                            }
                        }
                        if (type == CellType.Truba_22)
                        {
                            if (map[x - 1, y].Rotation == 0 || map[x - 1, y].Rotation == 90 || map[x - 1, y].Rotation == 180)  //Изменил x + 1
                            {
                                map[x, y].CellType = CellType.Truba_22;
                                Redraw();
                                ZAlivka(x, y + 1);
                                break;
                            }

                        }
                        if (type == CellType.Truba_33)
                        {
                            if (map[x + 1, y].Rotation == 0 || map[x + 1, y].Rotation == 90)
                            {
                                map[x, y].CellType = CellType.Truba_22;
                                Redraw();
                                ZAlivka(x, y + 1);
                                break;
                            }
                        } type = CellType.kirpih_2; 
                        #endregion                   

                    }
                    if (map[x, y].Rotation == 270) 
                    {
                        //Условия что бы не выйти за границы массива
                        #region   map[x, y-1]

                        if (map[x, y - 1].CellType == type1[0]) type = type1[0];
                        if (map[x, y - 1].CellType == type1[1]) type = type1[1];
                        if (map[x, y - 1].CellType == type1[2]) type = type1[2];
                        if (type == CellType.Truba_11)
                        {
                            if (map[x, y - 1].Rotation == 0 || map[x, y - 1].Rotation == 180)
                            {
                                map[x, y].CellType = CellType.Truba_22;
                                Redraw();
                                ZAlivka(x - 1, y);
                                break;
                            }
                        }
                        if (type == CellType.Truba_22)
                        {
                            if (map[x, y - 1].Rotation == 180 || map[x, y - 1].Rotation == 90)
                            {
                                map[x, y].CellType = CellType.Truba_22;
                                Redraw();
                                ZAlivka(x - 1, y);
                                break;
                            }

                        }
                        if (type == CellType.Truba_33)
                        {
                            if (map[x, y - 1].Rotation == 180 || map[x, y - 1].Rotation == 90 || map[x, y - 1].Rotation == 270)
                            {
                                map[x, y].CellType = CellType.Truba_22;
                                Redraw();
                                ZAlivka(x - 1, y);
                                break;
                            }
                        } type = CellType.kirpih_2;
                        #endregion
                        #region map[x - 1, y]
                        if (map[x - 1, y].CellType == type1[0]) type = type1[0];
                        if (map[x - 1, y].CellType == type1[1]) type = type1[1];
                        if (map[x - 1, y].CellType == type1[2]) type = type1[2];
                        if (type == CellType.Truba_11)
                        {
                            if (map[x - 1, y].Rotation == 90 || map[x - 1, y].Rotation == 270)
                            {
                                map[x, y].CellType = CellType.Truba_22;
                                Redraw();
                                ZAlivka(x, y - 1);//y-1
                                break;
                            }
                        }
                        if (type == CellType.Truba_22)
                        {
                            if (map[x - 1, y].Rotation == 0 || map[x - 1, y].Rotation == 90) // изменил на x + 1
                            {
                                map[x, y].CellType = CellType.Truba_22;
                                Redraw();
                                ZAlivka(x, y - 1);
                                break;
                            }

                        }
                        if (type == CellType.Truba_33)
                        {
                            if (map[x - 1, y].Rotation == 0 || map[x - 1, y].Rotation == 90 || map[x - 1, y].Rotation == 180)
                            {
                                map[x, y].CellType = CellType.Truba_22;
                                Redraw();
                                ZAlivka(x, y - 1);
                                break;
                            }
                        } type = CellType.kirpih_2;   
                        #endregion                   
                    }


                    break;

                case CellType.Truba_3:
                    if (map[x, y].Rotation == 0)
                    {
                        //Условия что бы не выйти за границы массива
                        #region   map[x, y-1]

                        if (map[x, y - 1].CellType == type1[0]) type = type1[0];
                        if (map[x, y - 1].CellType == type1[1]) type = type1[1];
                        if (map[x, y - 1].CellType == type1[2]) type = type1[2];
                        if (type == CellType.Truba_11)
                        {
                            if (map[x, y - 1].Rotation == 0 || map[x, y - 1].Rotation == 180)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x + 1, y);
                                ZAlivka(x - 1, y);
                                break;
                            }
                        }
                        if (type == CellType.Truba_22)
                        {
                            if (map[x, y - 1].Rotation == 90 || map[x, y - 1].Rotation == 180)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x + 1, y);
                                ZAlivka(x - 1, y);
                                break;
                            }

                        }
                        if (type == CellType.Truba_33)
                        {
                            if (map[x, y - 1].Rotation == 90 || map[x, y - 1].Rotation == 180 || map[x, y - 1].Rotation == 270)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x + 1, y);
                                ZAlivka(x - 1, y);
                                break;

                            }
                        }
                        type = CellType.kirpih_2; 
                        #endregion
                        #region map[x + 1, y]
                        if (map[x + 1, y].CellType == type1[0]) type = type1[0];
                        if (map[x + 1, y].CellType == type1[1]) type = type1[1];
                        if (map[x + 1, y].CellType == type1[2]) type = type1[2];
                        if (type == CellType.Truba_11)
                        {
                            if (map[x + 1, y].Rotation == 90 || map[x + 1, y].Rotation == 270)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x, y - 1);
                                ZAlivka(x - 1, y);
                                break;
                            }
                        }
                        if (type == CellType.Truba_22)
                        {
                            if (map[x + 1, y].Rotation == 180 || map[x + 1, y].Rotation == 270)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x, y - 1);
                                ZAlivka(x - 1, y);
                                break;
                            }

                        }
                        if (type == CellType.Truba_33) 
                        {
                            if (map[x + 1, y].Rotation == 0 || map[x + 1, y].Rotation == 180 || map[x + 1, y].Rotation == 270)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x, y - 1);
                                ZAlivka(x - 1, y);
                                break;
                            }

                        }
                        type = CellType.kirpih_2; 
                        #endregion
                        #region map[x - 1, y]
                        if (map[x - 1, y].CellType == type1[0]) type = type1[0];
                        if (map[x - 1, y].CellType == type1[1]) type = type1[1];
                        if (map[x - 1, y].CellType == type1[2]) type = type1[2];
                        if (type == CellType.Truba_11)
                        {
                            if (map[x - 1, y].Rotation == 90 || map[x - 1, y].Rotation == 270)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x, y - 1);
                                ZAlivka(x + 1, y);
                                break;
                            }
                        }
                        if (type == CellType.Truba_22)
                        {
                            if (map[x - 1, y].Rotation == 0 || map[x - 1, y].Rotation == 90)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x, y - 1);
                                ZAlivka(x + 1, y);
                                break;
                            }

                        }
                        if (type == CellType.Truba_33)
                        {
                            if (map[x - 1, y].Rotation == 0 || map[x - 1, y].Rotation == 180 || map[x - 1, y].Rotation == 90)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x, y - 1);
                                ZAlivka(x + 1, y);
                                break;
                            }

                        }
                        type = CellType.kirpih_2; 
                        #endregion
                    }
                    if (map[x, y].Rotation == 90)
                    {
                        //Условия что бы не выйти за границы массива
                        #region   map[x, y-1]

                        if (map[x, y - 1].CellType == type1[0]) type = type1[0];
                        if (map[x, y - 1].CellType == type1[1]) type = type1[1];
                        if (map[x, y - 1].CellType == type1[2]) type = type1[2];
                        if (type == CellType.Truba_11)
                        {
                            if (map[x, y - 1].Rotation == 0 || map[x, y - 1].Rotation == 180)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x + 1, y);
                                ZAlivka(x, y + 1);
                                break;
                            }
                        }
                        if (type == CellType.Truba_22)
                        {
                            if (map[x, y - 1].Rotation == 90 || map[x, y - 1].Rotation == 180)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x + 1, y);
                                ZAlivka(x, y + 1);
                                break;
                            }

                        }
                        if (type == CellType.Truba_33)
                        {
                            if (map[x, y - 1].Rotation == 90 || map[x, y - 1].Rotation == 180 || map[x, y - 1].Rotation == 270)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x + 1, y);
                                ZAlivka(x, y + 1);
                                break;

                            }
                        }
                        type = CellType.kirpih_2; 
                        #endregion
                        #region map[x + 1, y]
                        if (map[x + 1, y].CellType == type1[0]) type = type1[0];
                        if (map[x + 1, y].CellType == type1[1]) type = type1[1];
                        if (map[x + 1, y].CellType == type1[2]) type = type1[2];
                        if (type == CellType.Truba_11)
                        {
                            if (map[x + 1, y].Rotation == 90 || map[x + 1, y].Rotation == 270)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x, y - 1);
                                ZAlivka(x, y + 1);
                                break;
                            }
                        }
                        if (type == CellType.Truba_22)
                        {
                            if (map[x + 1, y].Rotation == 180 || map[x + 1, y].Rotation == 270)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x, y - 1);
                                ZAlivka(x, y + 1);
                                break;
                            }

                        }
                        if (type == CellType.Truba_33)
                        {
                            if (map[x + 1, y].Rotation == 0 || map[x + 1, y].Rotation == 180 || map[x + 1, y].Rotation == 270)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x, y - 1);
                                ZAlivka(x, y + 1);
                                break;
                            }

                        }
                        type = CellType.kirpih_2; 
                        #endregion
                        #region   map[x, y+1]

                        if (map[x, y + 1].CellType == type1[0]) type = type1[0];
                        if (map[x, y + 1].CellType == type1[1]) type = type1[1];
                        if (map[x, y + 1].CellType == type1[2]) type = type1[2];
                        if (type == CellType.Truba_11)
                        {
                            if (map[x, y + 1].Rotation == 0 || map[x, y + 1].Rotation == 180)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x + 1, y);
                                ZAlivka(x, y - 1);
                                break;
                            }
                        }
                        if (type == CellType.Truba_22)
                        {
                            if (map[x, y + 1].Rotation == 0 || map[x, y + 1].Rotation == 270)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x + 1, y);
                                ZAlivka(x, y - 1);
                                break;
                            }

                        }
                        if (type == CellType.Truba_33)
                        {
                            if (map[x, y + 1].Rotation == 0 || map[x, y + 1].Rotation == 90 || map[x, y + 1].Rotation == 270)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x + 1, y);
                                ZAlivka(x, y - 1);
                                break;

                            }
                        }
                        type = CellType.kirpih_2; 
                        #endregion
                        
                    }
                    if (map[x, y].Rotation == 180)
                    {
                        //Условия что бы не выйти за границы массива
                        #region   map[x, y+1]

                        if (map[x, y + 1].CellType == type1[0]) type = type1[0];
                        if (map[x, y + 1].CellType == type1[1]) type = type1[1];
                        if (map[x, y + 1].CellType == type1[2]) type = type1[2];
                        if (type == CellType.Truba_11)
                        {
                            if (map[x, y + 1].Rotation == 0 || map[x, y + 1].Rotation == 180)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x + 1, y);
                                ZAlivka(x - 1, y);
                                break;
                            }
                        }
                        if (type == CellType.Truba_22)
                        {
                            if (map[x, y + 1].Rotation == 0 || map[x, y + 1].Rotation == 270)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x + 1, y);
                                ZAlivka(x - 1, y);
                                break;
                            }

                        }
                        if (type == CellType.Truba_33)
                        {
                            if (map[x, y + 1].Rotation == 90 || map[x, y + 1].Rotation == 0 || map[x, y + 1].Rotation == 270)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x + 1, y);
                                ZAlivka(x - 1, y);
                                break;

                            }
                        }
                        type = CellType.kirpih_2; 
                        #endregion
                        #region map[x + 1, y]
                        if (map[x + 1, y].CellType == type1[0]) type = type1[0];
                        if (map[x + 1, y].CellType == type1[1]) type = type1[1];
                        if (map[x + 1, y].CellType == type1[2]) type = type1[2];
                        if (type == CellType.Truba_11)
                        {
                            if (map[x + 1, y].Rotation == 90 || map[x + 1, y].Rotation == 270)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x, y + 1);
                                ZAlivka(x - 1, y);
                                break;
                            }
                        }
                        if (type == CellType.Truba_22)
                        {
                            if (map[x + 1, y].Rotation == 180 || map[x + 1, y].Rotation == 270)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x, y + 1);
                                ZAlivka(x - 1, y);
                                break;
                            }

                        }
                        if (type == CellType.Truba_33)
                        {
                            if (map[x + 1, y].Rotation == 0 || map[x + 1, y].Rotation == 180 || map[x + 1, y].Rotation == 270)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x, y + 1);
                                ZAlivka(x - 1, y);
                                break;
                            }

                        }
                        type = CellType.kirpih_2; 
                        #endregion
                        #region map[x - 1, y]
                        if (map[x - 1, y].CellType == type1[0]) type = type1[0];
                        if (map[x - 1, y].CellType == type1[1]) type = type1[1];
                        if (map[x - 1, y].CellType == type1[2]) type = type1[2];
                        if (type == CellType.Truba_11)
                        {
                            if (map[x - 1, y].Rotation == 90 || map[x - 1, y].Rotation == 270)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x, y + 1);
                                ZAlivka(x + 1, y);
                                break;
                            }
                        }
                        if (type == CellType.Truba_22)
                        {
                            if (map[x - 1, y].Rotation == 0 || map[x - 1, y].Rotation == 90)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x, y + 1);
                                ZAlivka(x + 1, y);
                                break;
                            }

                        }
                        if (type == CellType.Truba_33)
                        {
                            if (map[x - 1, y].Rotation == 0 || map[x - 1, y].Rotation == 90 || map[x - 1, y].Rotation == 180)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x, y + 1);
                                ZAlivka(x + 1, y);
                                break;
                            }

                        }
                        type = CellType.kirpih_2; 
                        #endregion
                    }
                    if (map[x, y].Rotation == 270)
                    {
                        //Условия что бы не выйти за границы массива
                        #region   map[x, y-1]

                        if (map[x, y - 1].CellType == type1[0]) type = type1[0];
                        if (map[x, y - 1].CellType == type1[1]) type = type1[1];
                        if (map[x, y - 1].CellType == type1[2]) type = type1[2];
                        if (type == CellType.Truba_11)
                        {
                            if (map[x, y - 1].Rotation == 0 || map[x, y - 1].Rotation == 180)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x - 1, y);
                                ZAlivka(x, y + 1);
                                break;
                            }
                        }
                        if (type == CellType.Truba_22)
                        {
                            if (map[x, y - 1].Rotation == 90 || map[x, y - 1].Rotation == 180)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x - 1, y);
                                ZAlivka(x, y + 1);
                                break;
                            }

                        }
                        if (type == CellType.Truba_33)
                        {
                            if (map[x, y - 1].Rotation == 90 || map[x, y - 1].Rotation == 180 || map[x, y - 1].Rotation == 270)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x - 1, y);
                                ZAlivka(x, y + 1);
                                break;

                            }
                        }
                        type = CellType.kirpih_2; 
                        #endregion
                        #region map[x - 1, y]
                        if (map[x - 1, y].CellType == type1[0]) type = type1[0];
                        if (map[x - 1, y].CellType == type1[1]) type = type1[1];
                        if (map[x - 1, y].CellType == type1[2]) type = type1[2];
                        if (type == CellType.Truba_11)
                        {
                            if (map[x - 1, y].Rotation == 90 || map[x - 1, y].Rotation == 270)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x, y - 1);
                                ZAlivka(x, y + 1);
                                break;
                            }
                        }
                        if (type == CellType.Truba_22)
                        {
                            if (map[x - 1, y].Rotation == 0 || map[x - 1, y].Rotation == 90)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x, y - 1);
                                ZAlivka(x, y + 1);
                                break;
                            }

                        }
                        if (type == CellType.Truba_33)
                        {
                            if (map[x - 1, y].Rotation == 0 || map[x - 1, y].Rotation == 90 || map[x - 1, y].Rotation == 180)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x, y - 1);
                                ZAlivka(x, y + 1);
                                break;
                            }

                        }
                        type = CellType.kirpih_2; 
                        #endregion
                        #region   map[x, y+1]

                        if (map[x, y + 1].CellType == type1[0]) type = type1[0];
                        if (map[x, y + 1].CellType == type1[1]) type = type1[1];
                        if (map[x, y + 1].CellType == type1[2]) type = type1[2];
                        if (type == CellType.Truba_11)
                        {
                            if (map[x, y + 1].Rotation == 0 || map[x, y + 1].Rotation == 180)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x - 1, y);
                                ZAlivka(x, y - 1);
                                break;
                            }
                        }
                        if (type == CellType.Truba_22)
                        {
                            if (map[x, y + 1].Rotation == 0 || map[x, y + 1].Rotation == 270)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x - 1, y);
                                ZAlivka(x, y - 1);
                                break;
                            }

                        }
                        if (type == CellType.Truba_33)
                        {
                            if (map[x, y + 1].Rotation == 0 || map[x, y + 1].Rotation == 90 || map[x, y + 1].Rotation == 270)
                            {
                                map[x, y].CellType = CellType.Truba_33;
                                Redraw();
                                ZAlivka(x - 1, y);
                                ZAlivka(x, y - 1);
                                break;

                            }
                        }
                        type = CellType.kirpih_2; 
                        #endregion

                    }
                    break;
                
                default:
                    break;
            }  
             

	
        }
        public int Finih()
        {
            int z1;
            z1=map.SizeX-1;
            CellType type=CellType.kirpih_2;
                 if (map[z1, z1].CellType == CellType.Truba_11) type = CellType.Truba_11;
                 if (map[z1, z1].CellType == CellType.Truba_22) type = CellType.Truba_22;
                 if (map[z1, z1].CellType == CellType.Truba_33) type = CellType.Truba_33;
            if (map[z1,z1].CellType==type)
            {
                return 1;
            }
            
            return 0;
        }
    }
}
