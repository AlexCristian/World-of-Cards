using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace World_of_Cards.Resources
{
    static class  Shuffle
    {
        public static void ShuffleCardss()
        {
            bool[] f2 = new bool[56];
            int[] Rest_of_Cards = new int[53];
            Random random = new Random();
            int[,] p = new int[6,10];
            int i, j, randomNumber, firstPlayer, g = -1;
            bool[] f = new bool[60];

            //Shuffle each player cards
            for (i = 1; i <= DiscoveryBeacon.Number_of_Players; i++)
                for (j = 1; j <= 5; j++)
                {
                    do
                    {
                        randomNumber = random.Next(0, 53);
                    } while (f[randomNumber]);

                    p[i,j] = randomNumber;
                    f[randomNumber] = true;
                }

            //Shuffle first Card
            do
            {
                do
                {
                    randomNumber = random.Next(0, 53);
                } while (f[randomNumber] == true);
            } while ((randomNumber >= 0 && randomNumber <= 3) || (randomNumber >= 4 && randomNumber <= 7) || (randomNumber >= 8 && randomNumber <= 11) || (randomNumber >= 36 && randomNumber <= 39));

           // ID_firstCard = randomNumber;
            f[randomNumber] = true;

            //Shuffle firstPlayer
            firstPlayer = random.Next(0, 2);

            //Shuffle the rest of the cards
            for (i = 0; i <= 52; i++)
                if (f[i] == false)
                    Rest_of_Cards[++g] = i;

            for (i = 0; i < g; i++)
            {
                do
                {
                    randomNumber = random.Next(0, g);
                } while (f2[randomNumber] == true);

                f2[randomNumber] = true;
            }
        }
    }
}
