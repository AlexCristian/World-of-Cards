using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace World_of_Cards.Container
{
    public class HostContainer
    {
        public String PlayerName {get; set;} 
        public BitmapImage PlayerImage { get; set; }

        public HostContainer(String name, BitmapImage playerPhoto)
        {
            PlayerName = name;
            PlayerImage = playerPhoto;
        }

    }
}
