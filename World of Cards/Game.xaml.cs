using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Threading;
using World_of_Cards.Resources;
using Windows;

namespace World_of_Cards
{
    public partial class Game : PhoneApplicationPage
    {
        public int[] deck = new int[53];
        public int _time;
        public int _cards = 0;
        public int _deck;
        public DispatcherTimer _timer;
        public int[] cards = new int[53];
        public int ID_firstcard;


        private void CountdownTimerStep(object sender, EventArgs e)
        {
            if (_time > 0)
            {
                _time--;
                this.TIMER.Text = _time + " ";
            }
            else
                _timer.Stop();
        }

        private void StartTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Tick += new EventHandler(CountdownTimerStep);
            _time = 30;
            _timer.Start();
        }

        private void StopTimer()
        {
            _timer.Stop();
        }

        

        //Check Current Move
        public bool checkMove(int ID)
        {
            if (ID % 4 == ID_firstcard % 4)
                return true;
            if (ID / 4 == ID_firstcard / 4)
                return true;
            return false;
        }

        public Game()
        {
           InitializeComponent();
          
           this.icPlayer1.Items.Add(MainPage.G.players[0]);
           this.icPlayer2.Items.Add(MainPage.G.players[1]);
        }
    }
}