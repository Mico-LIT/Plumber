using Game.Model.Interface;
using Game.Model.Level;
using Game.Model.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Model.Session
{
    public class SavePoint
    {
        // Игрок
        public Player player{ get; set; }

        // Настройки
        public bool OptionsAllConnectedTybing { get; }
        public bool IsCompletedAllConnectedTybing { get; set; }
        //

        // Параметры
        public int SizeX { get; set; }
        public int SizeY { get; set; }

        public Point StartTybing { get; set; }
        public Point FinishTybing { get; set; }

        public List<Tybing1> Tybing1;
        public List<Tybing2> Tybing2;
        public List<Tybing3> Tybing3;
        public List<Tybing4> Tybing4;
        public List<Brick> Brick;

        public TimeSpan TravelTime { get; set; }
        public TimeSpan Time { get; set; }

        public string NameLevel { get; set; }
        //
    }
}
