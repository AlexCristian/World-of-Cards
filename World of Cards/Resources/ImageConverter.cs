using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Windows.Storage.Streams;

namespace World_of_Cards.Resources
{
    public static class ImageConverter
    {
        public static byte[] ImageToByteArray(BitmapImage bitmapImage)
        {
            if (bitmapImage == null)
            {
                return null;
            }
            WriteableBitmap image = new WriteableBitmap(bitmapImage);

            using (MemoryStream stream = new MemoryStream())
            {

                image.SaveJpeg(stream, image.PixelWidth, image.PixelHeight, 0, 100);
                return stream.ToArray();
            }
        }

        public async static Task<byte[]> URIToByteArray(String address)
        {
            if (address == null)
            {
                return null;
            }
            BitmapImage bitmapImage = new BitmapImage(new Uri(address, UriKind.Absolute));
            WriteableBitmap image = new WriteableBitmap(bitmapImage);

            using (MemoryStream stream = new MemoryStream())
            {

                image.SaveJpeg(stream, image.PixelWidth, image.PixelHeight, 0, 100);
                return stream.ToArray();
            }
        }

        public static BitmapImage ByteArrayToImage(byte[] byteArray)
        {
            if (byteArray == null)
            {
                return null;
            }

            BitmapImage bitmapImage = new BitmapImage();

            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                bitmapImage.SetSource(stream);
            }
            return bitmapImage;
        }
    }
}
