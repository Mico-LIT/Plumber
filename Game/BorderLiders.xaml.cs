using Game.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Game
{
    /// <summary>
    /// Interaction logic for BorderLiders.xaml
    /// </summary>
    public partial class BorderLiders : Window
    {
        public int _Balls { get; set; }
        public string _Name  { get; set; }

        public BorderLiders()
        {
            InitializeComponent();
            this._Balls = Session._Player.Balls;
            this._Name = Session._Player.Name;

            CurrentState.Text = string.Format("Текущее состояние Player:{0} Balls:{1}",_Name, _Balls);

            var players = Session.GetBorderLiders();

            if (players == null) return;

            if (players.Count != 0)
            {
                int i = 1;

                players.Add(Session._Player);
                players = players.OrderByDescending(x => x.Balls).ToList();

                foreach (var item in players)
                {
                    if (item.Name == _Name && item.Balls == _Balls)
                    {
                        Players.Items.Add(new ListBoxItem()
                        {
                            Content = string.Format("{0}) {1} {2} баллов", i++, item.Name, item.Balls),
                            Background = new SolidColorBrush(Color.FromRgb(129, 227, 139))
                        });
                    }
                    else
                    Players.Items.Add(new ListBoxItem() {
                        Content = string.Format("{0}) {1} {2} баллов", i++, item.Name, item.Balls)
                    });
                }

            }

        }
    }
}
