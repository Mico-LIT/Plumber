using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Model.Session
{
    public class Player
    {
        public string Name { get; set; }
        public Dictionary<string, int> BallLevel { get; set; } = new Dictionary<string, int>();
        public int Balls { get { return BallLevel.Sum(x => x.Value); } }
    }
}
