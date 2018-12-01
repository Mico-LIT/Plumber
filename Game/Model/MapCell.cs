using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Model
{
    /// <summary>
    /// Ячейка карты
    /// </summary>
    public class MapCell
    {
        /// <summary>
        /// Тип клетки
        /// </summary>
        public CellType CellType;

        public int Rotation;

        public int x1;
        public int x2;
        public int x3;
        public int x4;
    }
}
