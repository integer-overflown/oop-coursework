using System.IO;
using Avalonia.Media.Imaging;

namespace CourseWork.Database
{
    public static class Conversion
    {
        public static byte[] FromBitmapToByteArray(IBitmap bitmap)
        {
            var outputStream = new MemoryStream();
            bitmap.Save(outputStream);
            return outputStream.GetBuffer();
        }
        
        public static IBitmap FromByteArrayToBitmap(byte[] bitmap)
        {
            var inputStream = new MemoryStream(bitmap);
            return new Bitmap(inputStream);
        }
    }
}