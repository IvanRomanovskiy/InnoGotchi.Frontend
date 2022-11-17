using InnoGotchi.Client.Views;
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace InnoGotchi.Client.Components
{
    public static class Extentions
    {
        public static byte[] ImageToByte(this BitmapFrame imageSource)
        {
            if (imageSource == null) return new byte[0];
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
        public static BitmapFrame ByteToImage(this byte[] bytes)
        {
            MemoryStream stream = new MemoryStream(bytes);
            if (stream.Length == 0) return Properties.Resources.DefaultAvatar.ByteToImage();
            var img = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            return img ?? Properties.Resources.DefaultAvatar.ByteToImage();
        }
        public static string ToBase64String(this byte[] bytes) => Convert.ToBase64String(bytes);
        public static string ToBase64String(this BitmapFrame imageSource) => imageSource.ImageToByte().ToBase64String();
        public static byte[] ToByteFromBase64(this string baseStr) => Convert.FromBase64String(baseStr);
        public static BitmapFrame ToImageFromBase64(this string baseStr) => baseStr.ToByteFromBase64().ByteToImage();
    }
}
