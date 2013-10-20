using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls.Primitives;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Input;

namespace World_of_Cards
{

    public partial class Solitaire
    {

        public int _pac;

        private TranslateTransform[] move = new TranslateTransform[54];
        private TranslateTransform[] initialPosition = new TranslateTransform[54];

        private TransformGroup[] imageTransforms = new TransformGroup[54];

        public int[] canvas = new int[54];
        public int[,] r = new int[8, 24];
        public int[] k = new int[8];
        public int[,] F = new int[5, 26];
        public bool[] ev = new bool[54];
        public bool[] f = new bool[54];
        public int[] WhatRow = new int[54];
        public int[] pac = new int[27];
        public Image blank = new Image();
        public DeckImage[] deck = new DeckImage[55]; // !!!
        BitmapImage b = new BitmapImage();





        public void Shuffle()
        {
            Random random = new Random();
            int n, i, j;
            f[52] = false;
            for (i = 1; i <= 7; i++)
            {
                for (j = 1; j <= i; j++)
                {
                    do
                    {
                        n = random.Next(0, 52);
                    } while (f[n] == true);
                    r[i, j] = n;
                    f[n] = true;
                }
                r[i, 0] = i;
            }
        }

        private void Do_this2(int i)
        {
            deck[i].card.ManipulationCompleted += delegate(object sender, ManipulationCompletedEventArgs e) { ImageManipulationCompleted(sender, e); };
            deck[i].card.ManipulationDelta += delegate(object sender, ManipulationDeltaEventArgs e) { ImageManipulationDelta(sender, e); };
            deck[i].card.ManipulationStarted += delegate(object sender, ManipulationStartedEventArgs e) { ImageManipulationStarted(sender, e); };
        }


        public Solitaire()
        {
            Random random = new Random();
            int i, j, n;
            b = World_of_Cards.Resources.MyConverter.My_Converter(52);

            blank.Source = b;
            blank.Width = 70;
            blank.Height = 100;
            for (i = 0; i <= 52; i++)
            {
                deck[i] = new DeckImage();
                b = World_of_Cards.Resources.MyConverter.My_Converter(i);
                deck[i].card.Source = b;
                deck[i].card.Height = 100;
                deck[i].card.Width = 70;
            }

            InitializeComponent();
            Shuffle();

            b = World_of_Cards.Resources.MyConverter.My_Converter(52);
            for (i = 1; i <= 7; i++)
            {
                k[i] = -3;
                for (j = 1; j <= r[i, 0]; j++)
                {
                    k[i] += 3;
                    imageTransforms[r[i, j]] = new TransformGroup();
                    move[r[i, j]] = new TranslateTransform();
                    imageTransforms[r[i, j]].Children.Add(move[r[i, j]]);
                    initialPosition[r[i, j]] = new TranslateTransform();
                    deck[r[i, j]].ii = i;
                    deck[r[i, j]].jj = j;
                    deck[r[i, j]].card.RenderTransform = imageTransforms[r[i, j]];
                    deck[r[i, j]].card.Tag = deck[r[i, j]];


                    WhatRow[r[i, j]] = i;
                    switch (i)
                    {
                        case 1:
                            {
                                Canvas.SetTop(deck[r[i, j]].card, k[i]);
                                if (j != 1)
                                    canvas[r[i, j]] = 3;
                                else canvas[r[i, j]] = 0;

                                if (j < r[i, 0])
                                    deck[r[i, j]].card.Source = b;
                                else
                                {
                                    ev[r[i, j]] = true;
                                    deck[r[i, j]].card.ManipulationCompleted += delegate(object sender, ManipulationCompletedEventArgs e) { ImageManipulationCompleted(sender, e); };
                                    deck[r[i, j]].card.ManipulationDelta += delegate(object sender, ManipulationDeltaEventArgs e) { ImageManipulationDelta(sender, e); };
                                    deck[r[i, j]].card.ManipulationStarted += delegate(object sender, ManipulationStartedEventArgs e) { ImageManipulationStarted(sender, e); };
                                }
                                Row1.Children.Add(deck[r[i, j]].card);
                                break;
                            }
                        case 2:
                            {
                                Canvas.SetTop(deck[r[i, j]].card, k[i]);
                                if (j != 1)
                                    canvas[r[i, j]] = 3;
                                else canvas[r[i, j]] = 0;
                                if (j < r[i, 0])
                                    deck[r[i, j]].card.Source = b;
                                else
                                {
                                    ev[r[i, j]] = true;
                                    deck[r[i, j]].card.ManipulationCompleted += delegate(object sender, ManipulationCompletedEventArgs e) { ImageManipulationCompleted(sender, e); };
                                    deck[r[i, j]].card.ManipulationDelta += delegate(object sender, ManipulationDeltaEventArgs e) { ImageManipulationDelta(sender, e); };
                                    deck[r[i, j]].card.ManipulationStarted += delegate(object sender, ManipulationStartedEventArgs e) { ImageManipulationStarted(sender, e); };
                                }
                                Row2.Children.Add(deck[r[i, j]].card);
                                break;
                            }
                        case 3:
                            {
                                Canvas.SetTop(deck[r[i, j]].card, k[i]);
                                if (j != 1)
                                    canvas[r[i, j]] = 3;
                                else canvas[r[i, j]] = 0;
                                if (j < r[i, 0])
                                    deck[r[i, j]].card.Source = b;
                                else
                                {
                                    ev[r[i, j]] = true;
                                    deck[r[i, j]].card.ManipulationCompleted += delegate(object sender, ManipulationCompletedEventArgs e) { ImageManipulationCompleted(sender, e); };
                                    deck[r[i, j]].card.ManipulationDelta += delegate(object sender, ManipulationDeltaEventArgs e) { ImageManipulationDelta(sender, e); };
                                    deck[r[i, j]].card.ManipulationStarted += delegate(object sender, ManipulationStartedEventArgs e) { ImageManipulationStarted(sender, e); };
                                }
                                Row3.Children.Add(deck[r[i, j]].card);
                                break;
                            }
                        case 4:
                            {
                                Canvas.SetTop(deck[r[i, j]].card, k[i]);
                                if (j != 1)
                                    canvas[r[i, j]] = 3;
                                else canvas[r[i, j]] = 0;
                                if (j < r[i, 0])
                                    deck[r[i, j]].card.Source = b;
                                else
                                {
                                    ev[r[i, j]] = true;
                                    deck[r[i, j]].card.ManipulationCompleted += delegate(object sender, ManipulationCompletedEventArgs e) { ImageManipulationCompleted(sender, e); };
                                    deck[r[i, j]].card.ManipulationDelta += delegate(object sender, ManipulationDeltaEventArgs e) { ImageManipulationDelta(sender, e); };
                                    deck[r[i, j]].card.ManipulationStarted += delegate(object sender, ManipulationStartedEventArgs e) { ImageManipulationStarted(sender, e); };
                                }
                                Row4.Children.Add(deck[r[i, j]].card);
                                break;
                            }
                        case 5:
                            {
                                Canvas.SetTop(deck[r[i, j]].card, k[i]);
                                if (j != 1)
                                    canvas[r[i, j]] = 3;
                                else canvas[r[i, j]] = 0;
                                if (j < r[i, 0])
                                    deck[r[i, j]].card.Source = b;
                                else
                                {
                                    ev[r[i, j]] = true;
                                    deck[r[i, j]].card.ManipulationCompleted += delegate(object sender, ManipulationCompletedEventArgs e) { ImageManipulationCompleted(sender, e); };
                                    deck[r[i, j]].card.ManipulationDelta += delegate(object sender, ManipulationDeltaEventArgs e) { ImageManipulationDelta(sender, e); };
                                    deck[r[i, j]].card.ManipulationStarted += delegate(object sender, ManipulationStartedEventArgs e) { ImageManipulationStarted(sender, e); };
                                }
                                Row5.Children.Add(deck[r[i, j]].card);
                                break;
                            }
                        case 6:
                            {
                                Canvas.SetTop(deck[r[i, j]].card, k[i]);
                                if (j != 1)
                                    canvas[r[i, j]] = 3;
                                else canvas[r[i, j]] = 0;
                                if (j < r[i, 0])
                                    deck[r[i, j]].card.Source = b;
                                else
                                {
                                    ev[r[i, j]] = true;
                                    deck[r[i, j]].card.ManipulationCompleted += delegate(object sender, ManipulationCompletedEventArgs e) { ImageManipulationCompleted(sender, e); };
                                    deck[r[i, j]].card.ManipulationDelta += delegate(object sender, ManipulationDeltaEventArgs e) { ImageManipulationDelta(sender, e); };
                                    deck[r[i, j]].card.ManipulationStarted += delegate(object sender, ManipulationStartedEventArgs e) { ImageManipulationStarted(sender, e); };
                                }
                                Row6.Children.Add(deck[r[i, j]].card);
                                break;
                            }
                        case 7:
                            {
                                Canvas.SetTop(deck[r[i, j]].card, k[i]);
                                if (j != 1)
                                    canvas[r[i, j]] = 3;
                                else canvas[r[i, j]] = 0;
                                if (j < r[i, 0])
                                    deck[r[i, j]].card.Source = b;
                                else
                                {

                                    ev[r[i, j]] = true;
                                    deck[r[i, j]].card.ManipulationCompleted += delegate(object sender, ManipulationCompletedEventArgs e) { ImageManipulationCompleted(sender, e); };
                                    deck[r[i, j]].card.ManipulationDelta += delegate(object sender, ManipulationDeltaEventArgs e) { ImageManipulationDelta(sender, e); };
                                    deck[r[i, j]].card.ManipulationStarted += delegate(object sender, ManipulationStartedEventArgs e) { ImageManipulationStarted(sender, e); };
                                }
                                Row7.Children.Add(deck[r[i, j]].card);
                                break;
                            }
                    }
                }
            }
            Pac.Children.Add(blank);
            for (i = 0; i <= 23; i++)
            {
                do
                {
                    n = random.Next(0, 52);
                } while (f[n] == true);
                f[n] = true;
                deck[n].card.Source = b;
                deck[n].ii = 0;
                deck[n].jj = pac[0] + 1;
                WhatRow[n] = 0;
                pac[++pac[0]] = n;
                deck[n].card.Tag = deck[n];
                imageTransforms[n] = new TransformGroup();
                move[n] = new TranslateTransform();
                imageTransforms[n].Children.Add(move[n]);
                initialPosition[n] = new TranslateTransform();
                deck[n].card.RenderTransform = imageTransforms[n];
                Do_this2(n);
                ev[n] = true;

            }

            _pac = pac[0];

        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            int i, j;

            if (_pac != 0)
            {
                b = World_of_Cards.Resources.MyConverter.My_Converter(pac[_pac]);
                deck[pac[_pac]].card.Source = b;
                Canvas.SetTop(deck[pac[_pac]].card, 0);
                Canvas.SetLeft(deck[pac[_pac]].card, 0);

                F0.Children.Add(deck[pac[_pac]].card);
                deck[pac[_pac]].jj = F[0, 0] + 1;
                F[0, ++F[0, 0]] = pac[_pac];

                _pac--;
            }
            else
            {
                for (i = 1; i <= pac[0]; i++)
                    if (!(F0.Children.Contains(deck[pac[i]].card)))
                    {
                        pac[i] = pac[i + 1];
                        for (j = i + 1; j <= pac[0]; j++)
                            pac[j] = pac[j + 1];
                        i--;
                        pac[0]--;
                    }
                for (i = 1; i <= pac[0]; i++)
                    if (F0.Children.Contains(deck[pac[i]].card))
                    {

                        b = World_of_Cards.Resources.MyConverter.My_Converter(52);
                        deck[pac[i]].card.Source = b;
                        F0.Children.Remove(deck[pac[i]].card);
                        --F[0, 0];

                    }

                _pac = pac[0];
            }
        }
        private void ImageManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            int d1, d2, i, inv;
            var d = (DeckImage)((FrameworkElement)sender).Tag;
            d1 = d.ii; d2 = d.jj;
            inv = -d1;
            // this.testfield.Text = "Starting!";
            if (d1 >= 1)
            {
                for (i = d2; i <= r[d1, 0]; i++)
                {
                    initialPosition[r[d1, i]].X = move[r[d1, i]].X;
                    initialPosition[r[d1, i]].Y = move[r[d1, i]].Y;
                }
            }
            else
            {
                initialPosition[F[inv, F[inv, 0]]].X = move[F[inv, F[inv, 0]]].X;
                initialPosition[F[inv, F[inv, 0]]].Y = move[F[inv, F[inv, 0]]].Y;
            }

        }
        private void Do_this(int d1, int d2)
        {
            deck[r[d1, d2]].card.ManipulationCompleted += delegate(object sender, ManipulationCompletedEventArgs e) { ImageManipulationCompleted(sender, e); };
            deck[r[d1, d2]].card.ManipulationDelta += delegate(object sender, ManipulationDeltaEventArgs e) { ImageManipulationDelta(sender, e); };
            deck[r[d1, d2]].card.ManipulationStarted += delegate(object sender, ManipulationStartedEventArgs e) { ImageManipulationStarted(sender, e); };
        }
        private void ImageManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            int x1, x2, d1, d2, i, v, j, inv, lim;
            bool flag = true;
            var d = (DeckImage)((FrameworkElement)sender).Tag;
            int val;
            d1 = d.ii;
            inv = -d1;
            d2 = d.jj;
            if (d1 >= 1)
                val = r[d1, d2];
            else val = F[inv, d2];

            v = WhatRow[val];

            x1 = x2 = 0;
            lim = v;
            if (d1 == 0)
                lim = 2;
            if (d1 == -1)
                lim = 4;
            if (d1 == -2)
                lim = 5;
            if (d1 == -3)
                lim = 6;
            if (d1 == -4)
                lim = 7;


            // this.testfield.Text = "Completed!";
            if ((d1 >= 1 && move[r[d1, d2]].Y >= -500 && move[r[d1, d2]].Y <= -89 && r[d1, 0] == d2) || (d1 <= 0 && F[inv, 0] == d2 && move[F[inv, d2]].Y >= -47 && move[F[inv, d2]].Y <= 47))
            {
                switch (v)
                {
                    case -4: { x1 = -336; x2 = -274; break; }
                    case -3: { x1 = -234; x2 = -172; break; }
                    case -2: { x1 = -132; x2 = -72; break; }
                    case -1: { x1 = 72; x2 = 134; break; }
                    case 0: { x1 = 172; x2 = 234; break; }
                    case 1: { x1 = 274; x2 = 336; break; }
                    case 2: { x1 = 172; x2 = 234; break; }
                    case 3: { x1 = 70; x2 = 132; break; }
                    case 4: { x1 = -31; x2 = 31; break; }
                    case 5: { x1 = -133; x2 = -78; break; }
                    case 6: { x1 = -235; x2 = -173; break; }
                    case 7: { x1 = -337; x2 = -275; break; };
                }
                for (i = 1; i <= 4 && flag == true; i++)
                {
                    if ((v <= 0 && i != inv) || v >= 0)
                    {
                        if (move[val].X >= x1 && move[val].X <= x2 && flag == true)
                            if ((F[i, 0] != 0 && ((F[i, F[i, 0]] / 4 + 1) == val / 4 && F[i, F[i, 0]] % 4 == val % 4)) || (F[i, 0] == 0 && val >= 0 && val <= 3))
                            {
                                flag = false;
                                move[val].X = initialPosition[val].X;
                                move[val].Y = initialPosition[val].Y;
                                deck[val].ii = -i;
                                deck[val].jj = F[i, 0] + 1;
                                canvas[val] = 0;
                                Canvas.SetTop(deck[val].card, 0);
                                switch (v)
                                {
                                    case -4: { F0.Children.Remove(deck[val].card); break; }
                                    case -3: { F0.Children.Remove(deck[val].card); break; }
                                    case -2: { F0.Children.Remove(deck[val].card); break; }
                                    case -1: { F1.Children.Remove(deck[val].card); break; }
                                    case 0: { F0.Children.Remove(deck[val].card); break; }
                                    case 1: { Row1.Children.Remove(deck[r[d1, d2]].card); k[1] = k[1] - canvas[r[d1, i]]; break; }
                                    case 2: { Row2.Children.Remove(deck[r[d1, d2]].card); k[2] = k[2] - canvas[r[d1, i]]; break; }
                                    case 3: { Row3.Children.Remove(deck[r[d1, d2]].card); k[3] = k[3] - canvas[r[d1, i]]; break; }
                                    case 4: { Row4.Children.Remove(deck[r[d1, d2]].card); k[4] = k[4] - canvas[r[d1, i]]; break; }
                                    case 5: { Row5.Children.Remove(deck[r[d1, d2]].card); k[5] = k[5] - canvas[r[d1, i]]; break; }
                                    case 6: { Row6.Children.Remove(deck[r[d1, d2]].card); k[6] = k[6] - canvas[r[d1, i]]; break; }
                                    case 7: { Row7.Children.Remove(deck[r[d1, d2]].card); k[7] = k[7] - canvas[r[d1, i]]; break; }

                                }
                                switch (i)
                                {
                                    case 1: { F1.Children.Add(deck[val].card); break; }
                                    case 2: { F2.Children.Add(deck[val].card); break; }
                                    case 3: { F3.Children.Add(deck[val].card); break; }
                                    case 4: { F4.Children.Add(deck[val].card); break; }
                                }
                                F[i, 0]++;
                                F[i, F[i, 0]] = val;
                                WhatRow[val] = -i;
                                if (d1 >= 1)
                                    r[d1, 0]--;
                                else
                                {
                                    F[inv, 0]--;


                                }
                            }
                    }
                    x1 = x2 + 40;
                    x2 = x1 + 62;
                }
            }
            else
            {
                switch (v)
                {
                    case -4: { x1 = -642; x2 = -580; break; }
                    case -3: { x1 = -540; x2 = -478; break; }
                    case -2: { x1 = -438; x2 = -376; break; }
                    case -1: { x1 = -336; x2 = -272; break; }
                    case 0: { x1 = -132; x2 = -70; break; }
                    case 1: { x1 = 70; x2 = 132; break; }
                    case 2: { x1 = -132; x2 = -70; break; }
                    case 3: { x1 = -234; x2 = -172; break; }
                    case 4: { x1 = -336; x2 = -274; break; }
                    case 5: { x1 = -438; x2 = -376; break; }
                    case 6: { x1 = -540; x2 = -478; break; }
                    case 7: { x1 = -642; x2 = -580; break; };
                }
                for (j = 1; j < lim && flag == true; j++)
                {
                    if (move[val].X >= x1 && move[val].X <= x2 && flag == true)
                        if ((r[j, 0] != 0 && ((r[j, r[j, 0]] / 4 - 1) == val / 4 && (r[j, r[j, 0]] % 4 % 2) != (val % 4 % 2))) || (r[j, 0] == 0 && val >= 48 && val <= 51))
                        {
                            flag = false;
                            if (d1 >= 1)
                            {
                                for (i = d2; i <= r[d1, 0]; i++)
                                {
                                    move[r[d1, i]].X = initialPosition[r[d1, i]].X;
                                    move[r[d1, i]].Y = initialPosition[r[d1, i]].Y;
                                    k[j] += 20;
                                    deck[r[d1, i]].ii = j;
                                    deck[r[d1, i]].jj = r[j, 0] + 1;
                                    switch (v)
                                    {
                                        case 1: { Row1.Children.Remove(deck[r[d1, i]].card); k[1] = k[1] - canvas[r[d1, i]]; break; }
                                        case 2: { Row2.Children.Remove(deck[r[d1, i]].card); k[2] = k[2] - canvas[r[d1, i]]; break; }
                                        case 3: { Row3.Children.Remove(deck[r[d1, i]].card); k[3] = k[3] - canvas[r[d1, i]]; break; }
                                        case 4: { Row4.Children.Remove(deck[r[d1, i]].card); k[4] = k[4] - canvas[r[d1, i]]; break; }
                                        case 5: { Row5.Children.Remove(deck[r[d1, i]].card); k[5] = k[5] - canvas[r[d1, i]]; break; }
                                        case 6: { Row6.Children.Remove(deck[r[d1, i]].card); k[6] = k[6] - canvas[r[d1, i]]; break; }
                                        case 7: { Row7.Children.Remove(deck[r[d1, i]].card); k[7] = k[7] - canvas[r[d1, i]]; break; }

                                    }
                                    if (r[j, 0] == 0)
                                    {
                                        Canvas.SetTop(deck[r[d1, i]].card, 0);
                                        k[j] = 0;
                                    }
                                    else
                                        Canvas.SetTop(deck[r[d1, i]].card, k[j]);
                                    canvas[val] = 20;
                                    switch (j)
                                    {
                                        case 1: { Row1.Children.Add(deck[r[d1, i]].card); break; }
                                        case 2: { Row2.Children.Add(deck[r[d1, i]].card); break; }
                                        case 3: { Row3.Children.Add(deck[r[d1, i]].card); break; }
                                        case 4: { Row4.Children.Add(deck[r[d1, i]].card); break; }
                                        case 5: { Row5.Children.Add(deck[r[d1, i]].card); break; }
                                        case 6: { Row6.Children.Add(deck[r[d1, i]].card); break; }
                                        case 7: { Row7.Children.Add(deck[r[d1, i]].card); break; }

                                    }
                                    r[j, 0]++;
                                    r[j, r[j, 0]] = r[v, i];

                                    WhatRow[r[d1, i]] = j;
                                }
                                r[v, 0] = d2 - 1;
                            }
                            else
                            {
                                move[val].X = initialPosition[val].X;
                                move[val].Y = initialPosition[val].Y;
                                k[j] += 20;
                                deck[val].ii = j;
                                deck[val].jj = r[j, 0] + 1;
                                switch (v)
                                {
                                    case 0: { F0.Children.Remove(deck[val].card); break; }
                                    case -1: { F1.Children.Remove(deck[val].card); break; }
                                    case -2: { F2.Children.Remove(deck[val].card); break; }
                                    case -3: { F3.Children.Remove(deck[val].card); break; }
                                    case -4: { F4.Children.Remove(deck[val].card); break; }
                                }
                                if (r[j, 0] == 0)
                                {
                                    Canvas.SetTop(deck[val].card, 0);
                                    k[j] = 0;
                                }
                                else
                                    Canvas.SetTop(deck[val].card, k[j]);
                                canvas[val] = 20;
                                switch (j)
                                {
                                    case 1: { Row1.Children.Add(deck[val].card); break; }
                                    case 2: { Row2.Children.Add(deck[val].card); break; }
                                    case 3: { Row3.Children.Add(deck[val].card); break; }
                                    case 4: { Row4.Children.Add(deck[val].card); break; }
                                    case 5: { Row5.Children.Add(deck[val].card); break; }
                                    case 6: { Row6.Children.Add(deck[val].card); break; }
                                    case 7: { Row7.Children.Add(deck[val].card); break; }

                                }
                                r[j, 0]++;
                                r[j, r[j, 0]] = val;

                                WhatRow[val] = j;
                                F[inv, 0]--;
                            }
                        }
                    x1 = x2 + 40;
                    x2 = x1 + 62;
                }
                x1 = 70;
                x2 = 132;
                for (j = lim + 1; j <= 7 && flag == true; j++)
                {
                    if (move[val].X >= x1 && move[val].X <= x2 && flag == true)
                        if ((r[j, 0] != 0 && ((r[j, r[j, 0]] / 4 - 1) == val / 4 && (r[j, r[j, 0]] % 4 % 2) != (val % 4 % 2))) || (r[j, 0] == 0 && val >= 48 && val <= 51))
                        {
                            flag = false;
                            if (d1 >= 1)
                            {
                                for (i = d2; i <= r[d1, 0]; i++)
                                {
                                    move[r[d1, i]].X = initialPosition[r[d1, i]].X;
                                    move[r[d1, i]].Y = initialPosition[r[d1, i]].Y;
                                    k[j] += 20;
                                    deck[r[d1, i]].ii = j;
                                    deck[r[d1, i]].jj = r[j, 0] + 1;
                                    switch (v)
                                    {
                                        case 1: { Row1.Children.Remove(deck[r[d1, i]].card); k[1] = k[1] - canvas[r[d1, i]]; break; }
                                        case 2: { Row2.Children.Remove(deck[r[d1, i]].card); k[2] = k[2] - canvas[r[d1, i]]; break; }
                                        case 3: { Row3.Children.Remove(deck[r[d1, i]].card); k[3] = k[3] - canvas[r[d1, i]]; break; }
                                        case 4: { Row4.Children.Remove(deck[r[d1, i]].card); k[4] = k[4] - canvas[r[d1, i]]; break; }
                                        case 5: { Row5.Children.Remove(deck[r[d1, i]].card); k[5] = k[5] - canvas[r[d1, i]]; break; }
                                        case 6: { Row6.Children.Remove(deck[r[d1, i]].card); k[6] = k[6] - canvas[r[d1, i]]; break; }
                                        case 7: { Row7.Children.Remove(deck[r[d1, i]].card); k[7] = k[7] - canvas[r[d1, i]]; break; }

                                    }
                                    if (r[j, 0] == 0)
                                    {
                                        Canvas.SetTop(deck[r[d1, i]].card, 0);
                                        k[j] = 0;
                                    }
                                    else
                                        Canvas.SetTop(deck[r[d1, i]].card, k[j]);
                                    canvas[val] = 20;
                                    switch (j)
                                    {
                                        case 1: { Row1.Children.Add(deck[r[d1, i]].card); break; }
                                        case 2: { Row2.Children.Add(deck[r[d1, i]].card); break; }
                                        case 3: { Row3.Children.Add(deck[r[d1, i]].card); break; }
                                        case 4: { Row4.Children.Add(deck[r[d1, i]].card); break; }
                                        case 5: { Row5.Children.Add(deck[r[d1, i]].card); break; }
                                        case 6: { Row6.Children.Add(deck[r[d1, i]].card); break; }
                                        case 7: { Row7.Children.Add(deck[r[d1, i]].card); break; }
                                    }
                                    r[j, 0]++;
                                    r[j, r[j, 0]] = r[v, i];

                                    WhatRow[r[d1, i]] = j;
                                }
                                r[v, 0] = d2 - 1;
                            }
                            else
                            {
                                move[val].X = initialPosition[val].X;
                                move[val].Y = initialPosition[val].Y;
                                k[j] += 20;
                                deck[val].ii = j;
                                deck[val].jj = r[j, 0] + 1;
                                switch (v)
                                {
                                    case 0: { F0.Children.Remove(deck[val].card); break; }
                                    case -1: { F1.Children.Remove(deck[val].card); break; }
                                    case -2: { F2.Children.Remove(deck[val].card); break; }
                                    case -3: { F3.Children.Remove(deck[val].card); break; }
                                    case -4: { F4.Children.Remove(deck[val].card); break; }
                                }
                                if (r[j, 0] == 0)
                                {
                                    Canvas.SetTop(deck[val].card, 0);
                                    k[j] = 0;
                                }
                                else

                                    Canvas.SetTop(deck[val].card, k[j]);
                                canvas[val] = 20;
                                switch (j)
                                {
                                    case 1: { Row1.Children.Add(deck[val].card); break; }
                                    case 2: { Row2.Children.Add(deck[val].card); break; }
                                    case 3: { Row3.Children.Add(deck[val].card); break; }
                                    case 4: { Row4.Children.Add(deck[val].card); break; }
                                    case 5: { Row5.Children.Add(deck[val].card); break; }
                                    case 6: { Row6.Children.Add(deck[val].card); break; }
                                    case 7: { Row7.Children.Add(deck[val].card); break; }

                                }
                                r[j, 0]++;
                                r[j, r[j, 0]] = val;

                                WhatRow[val] = j;
                                F[inv, 0]--;
                            }
                        }
                    x1 = x2 + 40;
                    x2 = x1 + 62;
                }
            }
            if (flag == true)
            {
                x1 = -28;
                x2 = 34;
                j = lim;
                if (move[val].X >= x1 && move[val].X <= x2 && flag == true)
                    if ((r[j, 0] != 0 && ((r[j, r[j, 0]] / 4 - 1) == val / 4 && (r[j, r[j, 0]] % 4 % 2) != (val % 4 % 2))) || (r[j, 0] == 0 && val >= 48 && val <= 51))
                    {
                        flag = false;
                        move[val].X = initialPosition[val].X;
                        move[val].Y = initialPosition[val].Y;
                        k[j] += 20;
                        deck[val].ii = j;
                        deck[val].jj = r[j, 0] + 1;
                        switch (v)
                        {
                            case 0: { F0.Children.Remove(deck[val].card); break; }
                            case -1: { F1.Children.Remove(deck[val].card); break; }
                            case -2: { F2.Children.Remove(deck[val].card); break; }
                            case -3: { F3.Children.Remove(deck[val].card); break; }
                            case -4: { F4.Children.Remove(deck[val].card); break; }
                        }
                        if (r[j, 0] == 0)
                        {
                            Canvas.SetTop(deck[val].card, 0);
                            k[j] = 0;
                        }
                        else

                            Canvas.SetTop(deck[val].card, k[j]);
                        canvas[val] = 20;
                        switch (j)
                        {
                            case 1: { Row1.Children.Add(deck[val].card); break; }
                            case 2: { Row2.Children.Add(deck[val].card); break; }
                            case 3: { Row3.Children.Add(deck[val].card); break; }
                            case 4: { Row4.Children.Add(deck[val].card); break; }
                            case 5: { Row5.Children.Add(deck[val].card); break; }
                            case 6: { Row6.Children.Add(deck[val].card); break; }
                            case 7: { Row7.Children.Add(deck[val].card); break; }

                        }
                        r[j, 0]++;
                        r[j, r[j, 0]] = val;

                        WhatRow[val] = j;
                        F[inv, 0]--;
                    }


            }
            if (flag == true)
            {
                if (d1 >= 1)
                {
                    for (i = d2; i <= r[d1, 0]; i++)
                    {
                        move[r[d1, i]].X = initialPosition[r[d1, i]].X;
                        move[r[d1, i]].Y = initialPosition[r[d1, i]].Y;
                    }
                }
                else
                {
                    move[val].X = initialPosition[val].X;
                    move[val].Y = initialPosition[val].Y;
                }
            }
            else
            {
                if (d1 >= 1)
                {
                    if (r[d1, 0] != 0)
                    {
                        if (ev[r[d1, r[d1, 0]]] == false)
                        {
                            b = World_of_Cards.Resources.MyConverter.My_Converter(r[d1, r[d1, 0]]);
                            deck[r[d1, r[d1, 0]]].card.Source = b;
                            ev[r[d1, r[d1, 0]]] = true;
                            Do_this(d1, r[d1, 0]);
                        }
                    }
                }
            }

        }

        private void ImageManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            var d = (DeckImage)((FrameworkElement)sender).Tag;
            int d1, d2, i, inv;
            d1 = d.ii;
            d2 = d.jj;
            inv = -d1;
            //  this.testfield.Text = "Moving!";
            if (d1 >= 1)
            {
                for (i = d2; i <= r[d1, 0]; i++)
                {
                    move[r[d1, i]].X += e.DeltaManipulation.Translation.X;
                    move[r[d1, i]].Y += e.DeltaManipulation.Translation.Y;
                }
            }
            else
            {
                move[F[inv, F[inv, 0]]].X += e.DeltaManipulation.Translation.X;
                move[F[inv, F[inv, 0]]].Y += e.DeltaManipulation.Translation.Y;
            }
        }
    }
}