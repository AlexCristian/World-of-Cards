using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace World_of_Cards.Resources
{
    public class PlayerContainer
    {
        public SocketManager socket {get;set;}
        public BitmapImage profileImage { get; set; }
        public String playerName {get; set;}

        public PlayerContainer(SocketManager s, BitmapImage pp, String pn)
        {
            socket = s;
            profileImage = pp;
            playerName = pn;
        }
        public PlayerContainer()
        {
            socket = null;
            profileImage = null;
            playerName = null;
        }
    }
}
