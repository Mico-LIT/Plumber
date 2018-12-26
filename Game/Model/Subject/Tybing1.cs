using Game.Model.Enum;
using Game.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Model.Subject
{
    public class Tybing1 : ITybing
    {
        public SubjectStateType SubjectState { get; set; }
        public bool IsColor { get; set; }
        public Point Point { get; set; } = new Point();

        public SubjectType TubingType
        {
            get
            {
                return SubjectType.Tybing1;
            }
        }

        public Tybing1() { }
        public Tybing1(int y, int x, int rotation = 0, SubjectStateType subjectstate = SubjectStateType.Normal)
        {
            Point.X = x;
            Point.Y = y;
            this.SubjectState = subjectstate;
            this.rotation = rotation;
        }

        int rotation = 0;

        public int Rotation
        {
            get { return rotation; }
            set
            {
                if (value == 1)
                {
                    rotation += 90;
                    if (rotation > 270) rotation = 0;
                }
                else { rotation = value; }
            }
        }

        public Coordinate DirectCoordinate
        {
            get
            {
                if (rotation == 0 || rotation == 180)
                {
                    return new Coordinate() { Y = new int[] { -1, 1 }};
                }
                else
                {
                    return new Coordinate() { X = new int[] { -1, 1 } };
                }
            }
        }
    }
}
