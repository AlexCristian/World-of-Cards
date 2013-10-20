using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using World_of_Cards.Resources;

namespace World_of_Cards.Resources
{
    public class GameManager
    {
        public List<PlayerContainer> players {get;set;}
        public static bool isHost;
        public static int nr=0;
        public static int currentPlayer;
        public delegate void ReceivedMovedEvent(String scope, int playerIndex, int card);
        public delegate void PlayerTurnEventHandler();
        public delegate void GameStarts();
        public static event ReceivedMovedEvent MoveEvent;
        public event PlayerTurnEventHandler PlayerTurnEvent;
        public event GameStarts GameStartsEvent;

        public async void startGame(List<PlayerContainer> players2)
        {
            players = players2;
            isHost = true;
            for (int i = 0; i < players.Count(); i++)
            {   
                int a;
                a = i + 1;
                await players2[i].socket.sendMessage("player-nr", a.ToString());
                await players2[i].socket.sendMessage("player-details-image.", 0 + "." + players[i].profileImage.UriSource);
                await players2[i].socket.sendMessage("player-details-name.", 0 + "." + players[i].playerName);
                for (int j = 0; j < players.Count(); j++)
                {
                     sendPlayerDetails(players[i], j);
                }
            }
            for (int i = 0; i < players.Count(); i++)
            {
                players[i].socket.StringMessage += new SocketManager.StringReceivedEvent(MessageReceivedHandler_Host);
                await players[i].socket.sendMessage("game-starts", "");
            }
            GameStartsEvent();
        }

        private async void  sendPlayerDetails(PlayerContainer recipient, int toSendIndex)
        {
            await recipient.socket.sendMessage("player-details-image.", ((int)toSendIndex+1) + "." + recipient.profileImage.UriSource);
            await recipient.socket.sendMessage("player-details-name.", ((int)toSendIndex+1) + "." + recipient.playerName);
        }

        public async void startGame(PlayerContainer host)
        {
            isHost = false;
            players = new List<PlayerContainer>();
            players.Add(host);
            players[0].socket.StringMessage += new SocketManager.StringReceivedEvent(MessageReceivedHandler_Client);
        }
        private void injectList(List <PlayerContainer> array, int pos, PlayerContainer player)
        {
            while (array.Count() < pos)
            {
                array.Add(null);
            }
            array[pos] = player;
        }

        public void MessageReceivedHandler_Host(String scope, String message, int clientIndex)
        {
            if (scope == "move" || scope == "draw-card" || scope == "change-color")
            {
                for (int i = 0; i < players.Count(); i++)
                {
                    players[i].socket.sendMessage(scope, clientIndex+1 + "." + message);
                }
                MoveEvent(scope, clientIndex, int.Parse(message));
                nextMove();
                players[currentPlayer].socket.sendMessage("prompt-move", "");
            }
        }

        private void MessageReceivedHandler_Client(String scope, String message, int clientIndex)
        {
            if (scope == "move" || scope == "draw-card" || scope == "change-color")
            {
                MoveEvent(scope, int.Parse(message.Substring(0, message.IndexOf("."))), int.Parse(message.Substring(message.IndexOf(".")+1))); // faceti debugging aici
                nextMove();
            }
            else if (scope == "player-details-image")
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    players[int.Parse(message.Substring(0, message.IndexOf(".")))].profileImage = new BitmapImage();
                    players[int.Parse(message.Substring(0, message.IndexOf(".")))].profileImage.UriSource = new Uri(message, UriKind.Absolute);
                });
            }
            else if (scope == "player-details-name")
            {
                players[int.Parse(message.Substring(0, message.IndexOf(".")))].playerName = message.Substring(message.IndexOf(".")+1);
            }
            else if (scope == "prompt-move")
            {
                PlayerTurnEvent();
            }
            else if (scope == "game-start")
            {
                GameStartsEvent();
            }

        }

        private void nextMove()
        {
            currentPlayer++;
            if (currentPlayer < players.Count())
            {
                currentPlayer = 0;
            }
        }
    }
}

