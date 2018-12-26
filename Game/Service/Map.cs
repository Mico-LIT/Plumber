using Game.Model.Enum;
using Game.Model.Interface;
using Game.Model.Level;
using Game.Model.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Game.Model.Service
{
    static public class Map
    {
        public delegate void water(int y, int x, bool isReset = false);
        static public water Water { get; set; }

        // Настройки
        static bool OptionsAllConnectedTybing { get; } = true;
        static public bool IsCompletedAllConnectedTybing { get; set; } = true; // флаг (меняется автоматом)
        //

        // Параметры
        static internal int SizeX { get; set; }= 10;
        static internal int SizeY { get; set; } = 10;

        static Point StartTybing { get; set; } = new Point() { Y = 0, X = 0 };
        static Point FinishTybing { get; set; } = new Point() { Y = SizeY - 1, X = SizeX - 1 };

        static public ITybing[,] MapTybings = new ITybing[SizeY, SizeX];
        static public TimeSpan TravelTime { get; set; } = TimeSpan.FromSeconds(10);
        //

        /// <param name="IsLevelHorizontally"> как мы будем проходить уровень?</param>
        static public void InitializeMapTybingRandom(bool IsLevelHorizontally = false, bool defaultTybing = false)
        {
            Random random = new Random();

            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < SizeX; x++)
                {
                    var rand = random.Next(10);
                    if (rand > 8)
                    {
                        MapTybings[y, x] = new Tybing3(y, x);
                        continue;
                    }

                    if (rand > 6)
                    {
                        MapTybings[y, x] = new Tybing4(y, x);
                        continue;
                    }

                    if (rand > 5)
                    {
                        MapTybings[y, x] = new Tybing2(y, x);
                        continue;
                    }

                    if (rand >= 0)
                    {
                        MapTybings[y, x] = new Tybing1(y, x);
                    }
                }
            }

            if (!IsLevelHorizontally)
            {
                for (int x = 0; x < SizeX; x++)
                {
                    MapTybings[0, x] = new Brick(0, x);
                }

                for (int x = 0; x < SizeX; x++)
                {
                    MapTybings[SizeY - 1, x] = new Brick(0, x);
                }
            }
            else
            {
                for (int y = 0; y < SizeY; y++)
                {
                    MapTybings[y, 0] = new Brick(y, 0);
                }

                for (int y = 0; y < SizeY; y++)
                {
                    MapTybings[y, SizeY - 1] = new Brick(y, 0);
                }
            }

            if (defaultTybing)
            {
                if (!IsLevelHorizontally)
                {
                    MapTybings[StartTybing.Y, StartTybing.X] =
                        new Tybing1(StartTybing.Y, StartTybing.X, subjectstate : SubjectStateType.Entry);
                    MapTybings[SizeY - 1, SizeX - 1] = 
                        new Tybing1(SizeY - 1, SizeX - 1, subjectstate: SubjectStateType.Exit);
                }
                else
                {
                    MapTybings[StartTybing.Y, StartTybing.X] = 
                        new Tybing1(StartTybing.Y, StartTybing.X, rotation : 90, subjectstate: SubjectStateType.Entry);
                    MapTybings[SizeY - 1, SizeX - 1] = 
                        new Tybing1(SizeY - 1, SizeX - 1, rotation : 90, subjectstate: SubjectStateType.Exit);
                }
            }
        }

        static public void Start(ITybing tybing = null, int hash = 0)
        {
            if (tybing == null)
            {
                if (MapTybings[StartTybing.Y, StartTybing.X].DirectCoordinate.Y != null)
                    foreach (var Y in MapTybings[0, 0].DirectCoordinate.Y)
                    {
                        if (IsValidDirect(Y, StartTybing.X, MapTybings[StartTybing.Y, StartTybing.X]))
                        {
                            MapTybings[StartTybing.Y, StartTybing.X].IsColor = true;
                            Water(StartTybing.Y, StartTybing.X);
                            Start(MapTybings[Y, StartTybing.X], MapTybings[StartTybing.Y, StartTybing.X].GetHashCode());
                            return;
                        }
                    }

                if (MapTybings[StartTybing.Y, StartTybing.X].DirectCoordinate.X != null)
                    foreach (var X in MapTybings[StartTybing.Y, StartTybing.X].DirectCoordinate.X)
                    {
                        if (IsValidDirect(StartTybing.Y, X, MapTybings[StartTybing.Y, StartTybing.X]))
                        {
                            MapTybings[StartTybing.Y, StartTybing.X].IsColor = true;
                            Water(StartTybing.Y, StartTybing.X);
                            Start(MapTybings[StartTybing.Y, X], MapTybings[StartTybing.Y, StartTybing.X].GetHashCode());
                            return;
                        }
                    }
            }
            else
            {
                // Проверка на соединение и потом идем дальше!
                var curDirectCoor = tybing.DirectCoordinate;

                var curY = tybing.Point.Y;
                var curX = tybing.Point.X;

                //Уперлись в закрушенную трубу + Обработка кирпича
                if (MapTybings[curY, curX].TubingType == Enum.SubjectType.Brick)
                {
                    IsCompletedAllConnectedTybing = false;
                    return;
                }

                if (curDirectCoor?.Y != null)
                    for (int i = 0; i < curDirectCoor.Y.Length; i++)
                    {
                        int nextStapY = curY + curDirectCoor.Y[i];

                        if (IsValidDirect(nextStapY, curX, tybing))
                        {
                            if (hash == MapTybings[nextStapY, curX].GetHashCode())
                            {
                                if (MapTybings[curY, curX].IsColor) return;

                                curDirectCoor.Y[i] = 0;
                                MapTybings[curY, curX].IsColor = true;
                                Water(curY, curX);
                                break;
                            }
                        }
                    }

                if (curDirectCoor?.X != null)
                    for (int i = 0; i < curDirectCoor.X.Length; i++)
                    {
                        int nextStapX = curX + curDirectCoor.X[i];

                        if (IsValidDirect(curY, nextStapX, tybing))
                        {
                            if (hash == MapTybings[curY, nextStapX].GetHashCode())
                            {
                                if (MapTybings[curY, curX].IsColor) return;

                                curDirectCoor.X[i] = 0;
                                MapTybings[curY, curX].IsColor = true;
                                Water(curY, curX);
                                break;
                            }
                        }
                    }

                // сначала проверяем соеденили мы труби ли нет
                // Если труба не закрашена нет соединения! значит соединения не было
                if (!MapTybings[curY, curX].IsColor)
                {
                    IsCompletedAllConnectedTybing = false;
                    return;
                }

                if (curDirectCoor?.Y != null)
                    foreach (var Y in curDirectCoor.Y)
                    {
                        if (Y == 0) continue;

                        int nextStapY = curY + Y;

                        if (IsValidDirect(nextStapY, curX, tybing))
                        {
                            Start(MapTybings[nextStapY, curX], tybing.GetHashCode());
                            continue;
                        }
                    }

                if (curDirectCoor?.X != null)
                    foreach (var X in curDirectCoor.X)
                    {
                        if (X == 0) continue;

                        int nextStapX = curX + X;

                        if (IsValidDirect(curY, nextStapX, tybing))
                        {
                            Start(MapTybings[curY, nextStapX], tybing.GetHashCode());
                            continue;
                        }
                    }
            }
        }

        // загрузка уровня
        static public void Level(int i)
        {
            try
            {
                var levelSettings = Service.Level.Dowloond(string.Format("Level_{0}.json", i));

                SizeX = levelSettings.SizeMap.X;
                SizeY = levelSettings.SizeMap.Y;

                TravelTime = TimeSpan.FromSeconds(levelSettings.Time);

                MapTybings = new ITybing[SizeY, SizeX];

                InitializeMapTybingRandom(levelSettings.IsLevelHorizontally);

                levelSettings.Tybings.InitializeMapTybing(levelSettings.IsLevelHorizontally);
            }
            catch (Exception ex)
            {
                InitializeMapTybingRandom();
                throw ex;
            }
        }

        static void InitializeMapTybing(this List<TybingOption> tybings, bool islevelhorizontally)
        {
            foreach (var item in tybings)
                MapTybings[item.Point.Y, item.Point.X] = createTybing(
                    item.Point.Y,
                    item.Point.X,
                    item.SubjectType,
                    item.SubjectState,
                    islevelhorizontally);
        }

        static ITybing createTybing(int y, int x, SubjectType subjectType, SubjectStateType subjectState, bool islevelhorizontally)
        {
            var rotation = 0;

            if (subjectState == SubjectStateType.Entry)
            {
                StartTybing.X = x;
                StartTybing.Y = y;
            }

            if (subjectState == SubjectStateType.Exit)
            {
                FinishTybing.X = x;
                FinishTybing.Y = y;
            }

            if (islevelhorizontally && ((subjectState == SubjectStateType.Entry) || (subjectState == SubjectStateType.Exit)))
                rotation = 90;

            switch (subjectType)
            {
                case SubjectType.Tybing1:
                    return new Tybing1(y, x, rotation: rotation, subjectstate: subjectState);
                case SubjectType.Tybing2:
                    return new Tybing2(y, x, rotation: rotation, subjectstate: subjectState);
                case SubjectType.Tybing3:
                    return new Tybing3(y, x, rotation: rotation, subjectstate: subjectState);
                case SubjectType.Brick:
                    return new Brick(y, x);
                default:
                    return null;
            }
        }

        static public void Reset()
        {
            IsCompletedAllConnectedTybing = true;

            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < SizeX; x++)
                {
                    MapTybings[y, x].IsColor = false;
                    Water(y, x, true);
                }
            }
        }

        static bool IsValidDirect(int y, int x, ITybing tybing)
        {
            if (x < 0 || y < 0 || x >= SizeX || y >= SizeY)
            {
                if (OptionsAllConnectedTybing && IsCompletedAllConnectedTybing)
                {
                    IsCompletedAllConnectedTybing = IsValidAllConnectedTybing(tybing);
                }

                return false;
            }

            return true;
        }

        static bool IsValidAllConnectedTybing(ITybing tybing)
        {
            if (!((tybing.Point.X == StartTybing.X && tybing.Point.Y == StartTybing.Y) 
                || (tybing.Point.X == FinishTybing.X && tybing.Point.Y == FinishTybing.Y)))
                return false;
            return true;
        }
    }
}
