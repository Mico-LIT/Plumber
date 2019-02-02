using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Model.Interface;
using Game.Model.Service;
using Game.Model.Session;
using Game.Model.Subject;
using Newtonsoft.Json;

namespace Game.Service
{
    public static class Session
    {
        public static bool SaveS { get; set; }

        public static Player _Player = new Player();

        //

        public static bool IsNameLiderBords(string name)
        {
            _Player.Name = name;

            if (System.IO.File.Exists("BorderLiders.json"))
            {
                var text = System.IO.File.ReadAllText("BorderLiders.json");

                var player = JsonConvert.DeserializeObject<List<Player>>(text);

                return player.Exists(x => x.Name == name);
            }

            return false;
        }

        /// <summary>
        /// за каждый уровень начисляется мах 10 баллов
        /// </summary>
        public static void WriteRecord(string nameLevel, int travelTime, int currentTime)
        {
            double ticToBall = (10.0 / (travelTime + 10)); // колл секунд 1 балл
            int fullTime = travelTime - currentTime;

            double ball = Math.Ceiling(10 - (fullTime * ticToBall)); // балл

            if (ball > 10) ball = 10;

            if (_Player.BallLevel.ContainsKey(nameLevel))
            {
                if (ball > _Player.BallLevel[nameLevel]) _Player.BallLevel[nameLevel] = (int)ball;
            }
            else
            {
                _Player.BallLevel.Add(nameLevel, (int)ball);
            }
        }

        public static List<Player> GetBorderLiders()
        {
            if (System.IO.File.Exists("BorderLiders.json"))
            {
                var text = System.IO.File.ReadAllText("BorderLiders.json");

                return JsonConvert.DeserializeObject<List<Player>>(text);
            }
            else
            {
                return null;
            }
        }

        public static void SetBorderLiders()
        {
            var balls = _Player;

            //if (_Player.Balls <= 0) return;

            if (!string.IsNullOrWhiteSpace(_Player.Name))

                if (System.IO.File.Exists("BorderLiders.json"))
                {
                    var text = System.IO.File.ReadAllText("BorderLiders.json");

                    var borderliders = JsonConvert.DeserializeObject<List<Player>>(text);

                    var item = borderliders.FirstOrDefault(x => x.Name.ToUpper() == _Player.Name.ToUpper());
                    if (item != null)
                    {

                        if (item.Balls < _Player.Balls)
                        {
                            item.BallLevel = _Player.BallLevel;
                        }
                    }
                    else
                    {
                        borderliders.Add(_Player);
                    }

                    System.IO.File.WriteAllText("BorderLiders.json",
                        JsonConvert.SerializeObject(borderliders.OrderByDescending(x => x.Balls).ToList()));

                }
                else
                {
                    List<Player> players = new List<Player>();
                    players.Add(_Player);

                    System.IO.File.WriteAllText("BorderLiders.json", JsonConvert.SerializeObject(players));
                }

        }

        internal static void CorrectBorderLiders()
        {
            if (!string.IsNullOrWhiteSpace(_Player.Name))

                if (System.IO.File.Exists("BorderLiders.json"))
                {
                    var text = System.IO.File.ReadAllText("BorderLiders.json");

                    var borderliders = JsonConvert.DeserializeObject<List<Player>>(text);

                    var item = borderliders.FirstOrDefault(x => x.Name.ToUpper() == _Player.Name.ToUpper());

                    borderliders.Remove(item);

                    System.IO.File.WriteAllText("BorderLiders.json",
                        JsonConvert.SerializeObject(borderliders.OrderByDescending(x => x.Balls).ToList()));

                }
        }

        internal static void SavePoin()
        {
            //сохранить достижения игрока

            List<Tybing1> Tybing1 = new List<Tybing1>();
            List<Tybing2> Tybing2 = new List<Tybing2>();
            List<Tybing3> Tybing3 = new List<Tybing3>();
            List<Tybing4> Tybing4 = new List<Tybing4>();
            List<Brick> Brick = new List<Brick>();

            for (int y = 0; y < Map.SizeY; y++)
            {
                for (int x = 0; x < Map.SizeX; x++)
                {
                    var _tybing = Map.MapTybings[y, x];

                    if (_tybing is Tybing1)
                    {
                        Tybing1.Add((Tybing1)Map.MapTybings[y, x]);
                    }
                    if (_tybing is Tybing2)
                    {
                        Tybing2.Add((Tybing2)Map.MapTybings[y, x]);
                    }
                    if (_tybing is Tybing3)
                    {
                        Tybing3.Add((Tybing3)Map.MapTybings[y, x]);
                    }
                    if (_tybing is Tybing4)
                    {
                        Tybing4.Add((Tybing4)Map.MapTybings[y, x]);
                    }
                    if (_tybing is Brick)
                    {
                        Brick.Add((Brick)Map.MapTybings[y, x]);
                    }

                }
            }


            SavePoint savePoint = new SavePoint()
            {
                player = _Player,
                FinishTybing = Map.FinishTybing,
                IsCompletedAllConnectedTybing = Map.IsCompletedAllConnectedTybing,
                Tybing1 = Tybing1,
                Tybing2 = Tybing2,
                Tybing3 = Tybing3,
                Tybing4 = Tybing4,
                Brick = Brick,
                NameLevel = Map.NameLevel,
                SizeX = Map.SizeX,
                SizeY = Map.SizeY,
                StartTybing = Map.StartTybing,
                Time = Map.Time,
                TravelTime = Map.TravelTime
            };

            System.IO.File.WriteAllText("SavePoint.json", JsonConvert.SerializeObject(savePoint));
        }

        internal static void GetSavePoin()
        {
            var savepoint = JsonConvert.DeserializeObject<SavePoint>(System.IO.File.ReadAllText("SavePoint.json"));

            if (savepoint == null) return;

            ITybing[,] tybings = new ITybing[savepoint.SizeY, savepoint.SizeX];

            for (int y = 0; y < savepoint.SizeY; y++)
            {
                for (int x = 0; x < savepoint.SizeX; x++)
                {
                    var t1 = savepoint.Tybing1.FirstOrDefault(g => g.Point.X == x && g.Point.Y == y);
                    if (t1 != null)
                    {
                        tybings[y, x] = t1; continue;
                    }
                    var t2 = savepoint.Tybing2.FirstOrDefault(g => g.Point.X == x && g.Point.Y == y);
                    if (t2 != null)
                    {
                        tybings[y, x] = t2; continue;
                    }
                    var t3 = savepoint.Tybing3.FirstOrDefault(g => g.Point.X == x && g.Point.Y == y);
                    if (t3 != null)
                    {
                        tybings[y, x] = t3; continue;
                    }
                    var t4 = savepoint.Tybing4.FirstOrDefault(g => g.Point.X == x && g.Point.Y == y);
                    if (t4 != null)
                    {
                        tybings[y, x] = t4; continue;
                    }
                    var b = savepoint.Brick.FirstOrDefault(g => g.Point.X == x && g.Point.Y == y);
                    if (b != null)
                    {
                        tybings[y, x] = b; continue;
                    }

                }

            }

            Map.FinishTybing = savepoint.FinishTybing;
            Map.IsCompletedAllConnectedTybing = savepoint.IsCompletedAllConnectedTybing;
            Map.NameLevel = savepoint.NameLevel;
            Map.SizeX = savepoint.SizeX;
            Map.SizeY = savepoint.SizeY;
            Map.StartTybing = savepoint.StartTybing;
            Map.Time = savepoint.Time;
            Map.TravelTime = savepoint.TravelTime;
            Map.MapTybings = tybings;

            _Player = savepoint.player;
        }

        internal static bool IsSession()
        {
            return System.IO.File.Exists("SavePoint.json");
        }

        internal static void DeleteSession()
        {
            if (System.IO.File.Exists("SavePoint.json") && !SaveS)
            {
                System.IO.File.Delete("SavePoint.json");
            }
        }
    }
}
