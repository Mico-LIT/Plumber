using Game.Model.Enum;
using Game.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Model.Subject
{
    public class Brick : ITybing
    {
        public SubjectStateType SubjectState { get; set; }
        public Brick() { }

        public Brick(int y, int x)
        {
            Point.X = x;
            Point.Y = y;
        }

        public Point Point { get; set; } = new Point();

        public SubjectType TubingType => SubjectType.Brick;

        public int Rotation { get; set; } = 0;

        public Coordinate DirectCoordinate => null;

        public bool IsColor { get; set; }
    }
}
