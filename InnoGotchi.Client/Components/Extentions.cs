using InnoGotchi.Client.Views;
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace InnoGotchi.Client.Components
{
    public static class Extentions
    {
        public static byte[] ImageToByte(this BitmapImage imageSource)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(imageSource));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }

            return data;
        }
        public static string ToBase64String(this byte[] bytes) => Convert.ToBase64String(bytes);
        public static string ToBase64String(this BitmapImage imageSource) => imageSource.ImageToByte().ToBase64String();
    }
}
