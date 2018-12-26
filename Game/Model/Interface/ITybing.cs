using Game.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Model.Interface
{
    public interface ITybing
    {
        bool IsColor { get; set; }
        int Rotation { get; set; }

        SubjectStateType SubjectState { get; set; }
        Point Point { get; set; }
        SubjectType TubingType { get; }
        Coordinate DirectCoordinate { get;}
    }
}
