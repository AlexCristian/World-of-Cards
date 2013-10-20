using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Windows.Networking.Proximity;
using Windows.Networking.Sockets;
using World_of_Cards.Container;

namespace World_of_Cards.Resources
{
    public class DiscoveryBeacon
    {
        static public int Number_of_Players;
        public List<SocketManager> clients = new List<SocketManager>();
        public List<String> playersNames = new List<String>();
        public List<BitmapImage> playersImages = new List<BitmapImage>();
        public List<int> joinedPlayerIndexes = new List<int>();
        public List<PlayerContainer> Players = new List<PlayerContainer>();
        private MainPage MainMenu;

        public void Cancel()
        {
            PeerFinder.ConnectionRequested -= PeerFinder_ConnectionRequested;
            clients.Last().StringMessage -= new SocketManager.StringReceivedEvent(StringReceived_Handler);
            clients.Last().ByteArrayMessage -= new SocketManager.ByteArrayReceivedEvent(ByteArrayReceived_Handler);
        }
    
        public DiscoveryBeacon(MainPage parent, String beaconIdentifier)
        {
            PeerFinder.ConnectionRequested += PeerFinder_ConnectionRequested;

            //Initialize beacon
            PeerFinder.DisplayName = beaconIdentifier;
            PeerFinder.Start();
            MainMenu = parent;
        }

        public  void Dispose_Beacon()
        {
            //Notify clients that this game has become unavailable
            PeerFinder.Stop();
            //Stops broadcasting game
        }

        private async void PeerFinder_ConnectionRequested(object sender, ConnectionRequestedEventArgs args)
        {
            StreamSocket tempSocket;
            tempSocket = await PeerFinder.ConnectAsync(args.PeerInformation);
            clients.Add(new SocketManager(tempSocket, clients.Count));
            playersNames.Add(args.PeerInformation.DisplayName);
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                playersImages.Add(new BitmapImage());
            });
            clients.Last().StringMessage += new SocketManager.StringReceivedEvent(StringReceived_Handler);
            clients.Last().ByteArrayMessage += new SocketManager.ByteArrayReceivedEvent(ByteArrayReceived_Handler);
            await clients.Last().sendMessage("string.name-request", "");
            await clients.Last().sendMessage("string.profile-request", "");
            await clients.Last().receiveMessage();
        }
        private async void StringReceived_Handler(String scope, String message, int clientIndex)
        {
            if (scope == "string.name-request")
            {
                await clients[clientIndex].sendMessage("string.name-response", LiveAPIController.userName);
            }
            else if (scope == "string.profile-request")
            {
                await clients[clientIndex].sendMessage("string.profile-response", LiveAPIController.photoURI);
            }
            else if (scope == "string.join-game-request")
            {
                joinedPlayerIndexes.Add(clientIndex);
                refreshMainMenu();
            }
            else if (scope == "string.leave-game-request")
            {
                joinedPlayerIndexes.Remove(clientIndex);
                refreshMainMenu();
            }
            else if (scope == "string.game-size-request")
            {
                await clients[clientIndex].sendMessage("string.game-size-response", joinedPlayerIndexes.Count().ToString());
            }
            else if (scope == "string.name-response")
            {
                playersNames[clientIndex] = message;
            }
            else if (scope == "string.profile-response")
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    playersImages[clientIndex].UriSource = new Uri(message, UriKind.Absolute);
                });
                refreshMainMenu();
            }
           else if (scope == "game-begins")
            {
                Cancel();
                foreach (int item in joinedPlayerIndexes)
                {
                   Players.Add (new PlayerContainer (clients[item],playersImages[item],playersNames[item]));
                }
                MainPage.G.startGame(Players);
                MainPage.Next();
            }
            else
            {
                throw new NotImplementedException(scope);
            }
        }

        public void refreshMainMenu() //CAND SCHIMBI SA-MI ZICI (MAPA)
        {
            List<HostContainer> players = new List<HostContainer>();
            for (int i = 0; i < joinedPlayerIndexes.Count(); i++)
            {
                players.Add(new HostContainer(playersNames[joinedPlayerIndexes[i]], playersImages[joinedPlayerIndexes[i]]));
            }
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                MainMenu.refreshHostGameList(players);
            });
            Number_of_Players = players.Count();
        }
        private void ByteArrayReceived_Handler(String scope, Byte[] message, int clientIndex)
        {
            if (scope == "bytearray.profile-response")
            {
                playersImages[clientIndex] = ImageConverter.ByteArrayToImage(message);
            }
            throw new NotImplementedException(scope);
        }
      
    }
}
