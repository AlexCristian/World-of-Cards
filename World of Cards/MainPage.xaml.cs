using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using World_of_Cards.Resources;
using System.Windows.Media.Animation;
using World_of_Cards.Container;

namespace World_of_Cards
{

    public partial class MainPage : PhoneApplicationPage
    {
        private DiscoveryBeacon hostBeacon;
        private GameFinder joinFinder;
        public PlayerContainer Join;
        public static GameManager G = new GameManager(); 
        // Constructor

        public MainPage()
        {
            InitializeComponent();
            SP.Tap += SinglePlayerTapped;
            LiveAPIController.attemptAutoConnect(this);

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        public delegate void DisposeModalEvent(object sender, EventArgs e);
        public event DisposeModalEvent ModalDisposer;

        public void refreshHostGameList(List<HostContainer> games)
        {
            hostBinding.Items.Clear();
            foreach (HostContainer game in games)
            {
                hostBinding.Items.Add(game);
            }
        }

        public void refreshJoinGameList(List<JoinContainer> players)
        {
            joinBinding.Items.Clear();
            foreach (JoinContainer player in players)
            {
                joinBinding.Items.Add(player);
            }
        }
        
        private void Fade_Background()
        {
            MasksStoryboard.Begin();
        }

        private void Refocus_Menu()
        {
            MasksStoryboard_Reverse.Begin();
        }

        private void BackButton_Click(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Refocus_Menu();
            ModalDisposer(this, e); // Calls event to dispose of modal
        }

        private async void HostGame_Click(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.IsHost = true;
            if (LiveAPIController.isInitialized)
            {
                Fade_Background();
                HostGameModalAnimation.Begin();
                ModalDisposer += new DisposeModalEvent(HostGame_Dispose); //Registers modal with event handler

                hostBeacon = new DiscoveryBeacon(this, LiveAPIController.userName);
                //hostBinding.Items.Add();
            }
            else
            {
                //(Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Login.xaml", UriKind.RelativeOrAbsolute));
                await LiveAPIController.InitializeControllerAsync(this);
            }

        }


        private void HostGame_Dispose(object sender, EventArgs e)
        {
            HostGameModalAnimation_Reverse.Begin();
            ModalDisposer -= new DisposeModalEvent(HostGame_Dispose); //Deregisters event handler

            hostBeacon.Dispose_Beacon();
            hostBeacon = null;
        }

        private async void JoinGame_Click(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.IsHost = false;
            if (LiveAPIController.isInitialized)
            {
                Fade_Background();
                JoinGameModalAnimation.Begin();
                ModalDisposer += new DisposeModalEvent(JoinGame_Dispose); //Registers modal with event handler

                
                joinFinder = new GameFinder(this);
                joinFinder.startAutoRefresh();
                //joinBinding.Items.Add(new JoinContainer("Thomas Jefferson", "2", LiveAPIController.userImage, 2));
            }
            else
            {
                //(Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Login.xaml", UriKind.RelativeOrAbsolute));
                await LiveAPIController.InitializeControllerAsync(this);
            }
        }

        private void JoinGame_Dispose(object sender, EventArgs e)
        {
            JoinGameModalAnimation_Reverse.Begin();
            ModalDisposer -= new DisposeModalEvent(JoinGame_Dispose); //Deregisters event handler
        }

        private void gameSelect_Click(object sender, System.Windows.Input.GestureEventArgs e)
        {
            JoinContainer joinSend = (JoinContainer)((FrameworkElement)sender).DataContext;
  
            if (joinSend != null)
            {
                if (joinFinder.joinedGame != -1)
                {
                    joinFinder.leaveGame();
                }
                joinFinder.joinGame(joinSend.index);
                Looking.Text = "Waiting for " + joinSend.PlayerName;
            }
        }

        /*
         *  Every modal should implement an event handler for disposing of said modal
         */
        private void SinglePlayerTapped(object sender, EventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Solitaire.xaml", UriKind.RelativeOrAbsolute));
        }

       private void StartTapped(object sender, EventArgs e)
        {
            foreach (SocketManager item in hostBeacon.clients)
            {
               item.sendMessage("game-begins","");
            }
           
            
        }
       public  static void Next()
       {
           (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/Game.xaml", UriKind.RelativeOrAbsolute));
       }
    }
}