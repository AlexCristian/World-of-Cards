using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Windows.Networking.Proximity;
using World_of_Cards.Container;

namespace World_of_Cards.Resources
{
    class GameFinder
    {
        private bool autoRefreshSwitch;
        private IReadOnlyList<PeerInformation> gamePeers;
        public List<SocketManager> games = new List<SocketManager>();
        public List<String> gameNames = new List<String>();
        public List<BitmapImage> gameImages = new List<BitmapImage>();
        public List<int> gameSizes = new List<int>();
        public int joinedGame = -1;
        private MainPage MainMenu;
        public GameFinder(MainPage parent)
        {
            MainMenu = parent;
            PeerFinder.Start();
        }

        public void Cancel()
        {
            games.Last().StringMessage -= new SocketManager.StringReceivedEvent(StringReceived_Handler);
            games.Last().ByteArrayMessage -= new SocketManager.ByteArrayReceivedEvent(ByteArrayReceived_Handler);
        }

        public async void joinGame(int index)
        {
            joinedGame = index;
            await games[index].sendMessage("string.join-game-request", "");
        }

        public async void leaveGame()
        {
            if (joinedGame > -1)
            {
                await games[joinedGame].sendMessage("string.leave-game-request", "");
                joinedGame = -1;
            }
            else
            {
                throw new NullReferenceException("Leaving but no game joined");
            }
        }

        private async void autoRefresh()
        {
            if (autoRefreshSwitch)
            {
                gamePeers = await PeerFinder.FindAllPeersAsync();
                bool[] visited = new bool[games.Count()];
                foreach (PeerInformation peer in gamePeers)
                {
                    if (gameNames.Contains(peer.DisplayName))
                    {
                        int gameIndex = gameNames.IndexOf(peer.DisplayName);
                        await games[gameIndex].sendMessage("string.game-size-request", "");
                        visited[gameIndex] = true;
                    }
                    else
                    {
                        games.Add(new SocketManager(await PeerFinder.ConnectAsync(peer), games.Count()));
                        games.Last().StringMessage += new SocketManager.StringReceivedEvent(StringReceived_Handler);
                        games.Last().ByteArrayMessage += new SocketManager.ByteArrayReceivedEvent(ByteArrayReceived_Handler);
                        gameNames.Add(peer.DisplayName);
                        gameSizes.Add(0);
                        gameImages.Add(new BitmapImage());
                        await games.Last().sendMessage("string.game-size-request", "");
                        await games.Last().sendMessage("string.profile-request", "");
                        await games.Last().receiveMessage();
                    }
                }
                /*
                for (int i = 0; i < visited.Length; i++)
                {
                    if (!visited[i])
                    {
                        games[i].StringMessage -= new SocketManager.StringReceivedEvent(StringReceived_Handler);
                        games[i].ByteArrayMessage -= new SocketManager.ByteArrayReceivedEvent(ByteArrayReceived_Handler);
                        games.RemoveAt(i);
                        gameNames.RemoveAt(i);
                        gameImages.RemoveAt(i);
                        gameSizes.RemoveAt(i);
                        for (int j = i; i < games.Count(); j++)
                        {
                            games[j].client--;
                        }
                    }
                }
                */
                updateFoundGamesTable();
                await Task.Delay(TimeSpan.FromSeconds(3));
                autoRefresh();
            }
        }

        private void updateFoundGamesTable()
        {
            List<JoinContainer> tempGames = new List<JoinContainer>();
            for (int i = 0; i < games.Count; i++)
            {
                //tempGames.Add(new JoinContainer(gameNames[i], gameSizes[i].ToString(), gameImages[i], games[i], i));
                tempGames.Add(new JoinContainer(gameNames[i], gameSizes[i].ToString(), gameImages[i], games[i], i));
            }
            MainMenu.refreshJoinGameList(tempGames);
        }

        private async void StringReceived_Handler(String scope, String message, int clientIndex)
        {
            if (scope == "string.name-request")
            {
                await games[clientIndex].sendMessage("string.name-response", LiveAPIController.userName);
            }
            else if (scope == "string.profile-request")
            {
                await games[clientIndex].sendMessage("string.profile-response", LiveAPIController.photoURI);
            }
            else if (scope == "string.name-response")
            {
                gameNames[clientIndex] = message;
            }
            else if (scope == "string.game-size-response")
            {
                gameSizes[clientIndex] = int.Parse(message);
            }
            else if (scope == "string.profile-response")
            {
                gameImages[clientIndex] = new BitmapImage(new Uri(message, UriKind.Absolute));
                updateFoundGamesTable();
            }
            else if (scope == "game-begins")
            {
                Cancel();
                games[joinedGame].sendMessage("game-begins", "");
                PlayerContainer HOST = new PlayerContainer (games[joinedGame], gameImages [joinedGame], gameNames [joinedGame]);
                MainPage.G.startGame(HOST);
                MainPage.Next();
            }
            else
            {
                throw new NotImplementedException(scope);
            }
        }
        private void ByteArrayReceived_Handler(String scope, Byte[] message, int clientIndex)
        {
            if (scope == "bytearray.profile-response")
            {
                gameImages[clientIndex] = ImageConverter.ByteArrayToImage(message);
                updateFoundGamesTable();
            }
            throw new NotImplementedException(scope);
        }

        public void startAutoRefresh()
        {
            autoRefreshSwitch = true;
            autoRefresh();
        }

        public void stopAutoRefresh()
        {
            autoRefreshSwitch = false;

        }
    
    }
}
