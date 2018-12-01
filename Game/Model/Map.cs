using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Game.Model
{
    public class Map
    {
        public int SizeX;
        public int SizeY;

        MapCell[,] Cells;

        MapCell defaultMapCell = new MapCell()
        {
            CellType = CellType.kirpih_2
        };

        public MapCell this[int x, int y]  
        {                                   
            get
            {
                if (x < 0 || y < 0 || x >= SizeX || y >= SizeY)  
                    return defaultMapCell;
                return Cells[x, y];    
            }
        }

        Random rand = new Random();

        int Level(string str) //Размер карты
        {int i=6;           
            StreamReader fs = new StreamReader(str);
            while (true)
            {                
                string temp = fs.ReadLine();
                if (temp == null) return i+1;
                i = int.Parse(temp.Substring(0, 2));        
            }            
        }
        
       
        public Map(string LEV)                //конструктор уровней
        {
            SizeX = Level(LEV);
            SizeY = SizeX;
            Cells = new MapCell[SizeX, SizeY];
          
            Zag_Object();

            Set_Trumpet(0, 0, 20);                    
            for (int x = 0; x < SizeX; x++)
                for (int y = 0; y < SizeY; y++)
                {
                    if (rand.Next(500) > 445) Set_Trumpet(x, y, 5);
                }
            if (SizeX == 6) Level(1,"level_1.txt"); //Загрузка уровней
            if (SizeX == 10) Level(2,"level_2.txt");
            if (SizeX == 12) Level(3,"level_3.txt");
             
        }

        void Set_Trumpet(int x, int y, int size)      
        {
            if (size == 0) return;
            if (this[x, y].CellType != CellType.Truba_3) return;
            else
            {
                Cells[x, y].CellType = CellType.Truba_1; 
                if (rand.Next(50) < 35) Cells[x, y].CellType = CellType.Truba_2;
                if (rand.Next(50) > 20) Set_Trumpet(x + 1, y, size - 1);
                if (rand.Next(50) > 20) Set_Trumpet(x - 1, y, size - 1);
                if (rand.Next(50) > 20) Set_Trumpet(x, y + 1, size - 1); 
                if (rand.Next(50) > 20) Set_Trumpet(x, y - 1, size - 1);
            }
        }
        void Zag_Object()
        {
            for (int y = 0; y < SizeX; y++)
            {
                if (y == 0)
                {
                    for (int x = 0; x < SizeY; x++)            //Загружаем Кирпичи  Вверх
                    {
                        MapCell cell = new MapCell();
                        cell.CellType = CellType.kirpih_2;
                        Cells[x, y] = cell;
                    }
                }
                else
                {
                    if (y == SizeY - 1)
                    {
                        for (int x = 0; x < SizeY; x++)            //Загружаем Кирпичи Вниз
                        {
                            MapCell cell = new MapCell();
                            cell.CellType = CellType.kirpih_2;
                            Cells[x, y] = cell;
                        }
                    }
                    else
                        for (int x = 0; x < SizeY; x++)          //Загружаем трубы
                        {
                            MapCell cell = new MapCell();
                            cell.CellType = CellType.Truba_3;
                            Cells[x, y] = cell;
                        }
                }
            }
            
            MapCell celll1 = new MapCell();
            celll1.CellType = CellType.Truba_11;
            Cells[0, 0] = celll1;
            MapCell celll = new MapCell();
            celll.CellType = CellType.Truba_1;
            Cells[SizeX - 1, SizeY - 1] = celll;
            
        }
       void Level(int i,string str)      //Загрузка уровней
        {
           int x,y,t;
           StreamReader fs3 = new StreamReader(@str);                 
                 while (true)
                        {
                        
                    string temp = fs3.ReadLine();
                    if(temp == null) break;
                    x = int.Parse(temp.Substring(0, 2));
                    y = int.Parse(temp.Substring(2, 3));
                    t = int.Parse(temp.Substring(21));
                    if (t == 1) {Cells[x, y].CellType = CellType.Truba_1;}
                    if (t == 2) {Cells[x, y].CellType = CellType.Truba_2;}
                    if (t == 3) {Cells[x, y].CellType = CellType.Truba_3;}
                        
                        }

        }

    }
}
