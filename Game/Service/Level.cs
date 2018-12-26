using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Model.Enum;
using Game.Model.Interface;
using Game.Model.Level;
using Newtonsoft.Json;

namespace Game.Model.Service
{
    public static class Level
    {
        public static LevelSettings Dowloond(string str)
        {
            var levelSettings = System.IO.File.ReadAllText(str);
            return JsonConvert.DeserializeObject<LevelSettings>(levelSettings);
        }

        static public void SaveAllLevel()
        {
            level1();
            level2();
        }

        static void level1()
        {
            var level = JsonConvert.SerializeObject(new LevelSettings()
            {
                IsLevelHorizontally = false,
                Time = 40,
                SizeMap = new Point() { Y = 6, X = 6 },
                Tybings = new List<TybingOption>
                {
                 new TybingOption(){ Point = new Point(){  Y = 0, X = 0}, SubjectType = SubjectType.Tybing1, SubjectState = SubjectStateType.Entry },
                 new TybingOption(){ Point = new Point(){  Y = 1, X = 0}, SubjectType = SubjectType.Tybing3 },
                 new TybingOption(){ Point = new Point(){  Y = 2, X = 0}, SubjectType = SubjectType.Tybing1 },
                 new TybingOption(){ Point = new Point(){  Y = 3, X = 0}, SubjectType = SubjectType.Tybing2},
                 new TybingOption(){ Point = new Point(){  Y = 3, X = 1}, SubjectType = SubjectType.Tybing1},
                 new TybingOption(){ Point = new Point(){  Y = 3, X = 2}, SubjectType = SubjectType.Tybing1},
                 new TybingOption(){ Point = new Point(){  Y = 3, X = 3}, SubjectType = SubjectType.Tybing2},
                 new TybingOption(){ Point = new Point(){  Y = 4, X = 3}, SubjectType = SubjectType.Tybing2},

                 new TybingOption(){ Point = new Point(){  Y = 4, X = 4}, SubjectType = SubjectType.Tybing1},
                 new TybingOption(){ Point = new Point(){  Y = 4, X = 5}, SubjectType = SubjectType.Tybing3},
                 new TybingOption(){ Point = new Point(){  Y = 1, X = 1}, SubjectType = SubjectType.Tybing1},
                 new TybingOption(){ Point = new Point(){  Y = 1, X = 2}, SubjectType = SubjectType.Tybing2},
                 new TybingOption(){ Point = new Point(){  Y = 2, X = 2}, SubjectType = SubjectType.Tybing2},
                 new TybingOption(){ Point = new Point(){  Y = 2, X = 3}, SubjectType = SubjectType.Tybing1},
                 new TybingOption(){ Point = new Point(){  Y = 2, X = 4}, SubjectType = SubjectType.Tybing1},
                 new TybingOption(){ Point = new Point(){  Y = 2, X = 5}, SubjectType = SubjectType.Tybing2},
                 new TybingOption(){ Point = new Point(){  Y = 3, X = 5}, SubjectType = SubjectType.Tybing1},
                 new TybingOption(){ Point = new Point(){  Y = 5, X = 5}, SubjectType = SubjectType.Tybing1, SubjectState = SubjectStateType.Exit  },
                }
            });

            System.IO.File.WriteAllText("Level_1.json", level);
        }

        static void level2()
        {
            var level = JsonConvert.SerializeObject(new LevelSettings()
            {
                IsLevelHorizontally = true,
                Time = 60,
                SizeMap = new Point() { Y = 6, X = 6 },
                Tybings = new List<TybingOption>
                {
                 new TybingOption(){ Point = new Point(){Y = 4, X = 0}, SubjectType = SubjectType.Tybing1, SubjectState = SubjectStateType.Entry},
                 new TybingOption(){ Point = new Point(){Y = 3, X = 1}, SubjectType = SubjectType.Tybing2},
                 new TybingOption(){ Point = new Point(){Y = 3, X = 2}, SubjectType = SubjectType.Tybing1},
                 new TybingOption(){ Point = new Point(){Y = 3, X = 3}, SubjectType = SubjectType.Tybing2},
                 new TybingOption(){ Point = new Point(){Y = 2, X = 3}, SubjectType = SubjectType.Tybing2},
                 new TybingOption(){ Point = new Point(){Y = 2, X = 2}, SubjectType = SubjectType.Tybing2},
                 new TybingOption(){ Point = new Point(){Y = 1, X = 2}, SubjectType = SubjectType.Tybing3},
                 new TybingOption(){ Point = new Point(){Y = 1, X = 1}, SubjectType = SubjectType.Tybing2},
                 new TybingOption(){ Point = new Point(){Y = 0, X = 1}, SubjectType = SubjectType.Tybing2},
                 new TybingOption(){ Point = new Point(){Y = 0, X = 2}, SubjectType = SubjectType.Tybing1},

                 new TybingOption(){ Point = new Point(){Y = 2, X = 2}, SubjectType = SubjectType.Tybing2},
                 new TybingOption(){ Point = new Point(){Y = 0, X = 3}, SubjectType = SubjectType.Tybing3},
                 new TybingOption(){ Point = new Point(){Y = 1, X = 3}, SubjectType = SubjectType.Tybing2},
                 new TybingOption(){ Point = new Point(){Y = 0, X = 4}, SubjectType = SubjectType.Tybing2},
                 new TybingOption(){ Point = new Point(){Y = 1, X = 4}, SubjectType = SubjectType.Tybing2},
                 new TybingOption(){ Point = new Point(){Y = 4, X = 1}, SubjectType = SubjectType.Tybing2},
                 new TybingOption(){ Point = new Point(){Y = 1, X = 5}, SubjectType = SubjectType.Tybing1, SubjectState = SubjectStateType.Exit},
                }
            });

            System.IO.File.WriteAllText("Level_2.json", level);
        }

    }
}
