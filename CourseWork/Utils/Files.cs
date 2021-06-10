using System.IO;
using Avalonia.Media.Imaging;

namespace CourseWork.Utils
{
    public static class Files
    {
        public static Bitmap ReadBitmap(string fileName)
        {
            using var stream = File.Open(fileName, FileMode.Open);
            return new Bitmap(stream);
        }
    }
}