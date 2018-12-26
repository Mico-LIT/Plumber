using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Model.Level
{
    public class LevelSettings
    {
        public bool IsLevelHorizontally { get; set; }
        public Point SizeMap { get; set; }
        public int Time { get; set; }
        public List<TybingOption> Tybings { get; set; }
    }
}
