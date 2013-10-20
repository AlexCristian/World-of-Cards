using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using World_of_Cards.Resources;

namespace World_of_Cards.Container
{
    public class JoinContainer
    {
        public String PlayerName {get; set;} 
        public String GameSize {get; set;}
        public BitmapImage PlayerImage { get; set; }
        public SocketManager socket { get; set; }
        public int index { get; set; }

        public JoinContainer(String name, String numberPlayers, BitmapImage playerPhoto, SocketManager sock, int nr)
        {
            PlayerName = name;
            GameSize = numberPlayers;
            socket = sock;
            PlayerImage = playerPhoto;
            index = nr;
        }


    }
}
