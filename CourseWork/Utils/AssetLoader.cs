using System;
using System.IO;
using Avalonia;
using Avalonia.Platform;

namespace CourseWork.Utils
{
    public static class AssetLoader
    {
        private const string RootDirectory = "CourseWork";

        public static Stream GetResourceAsStream(string assetName)
        {
            var assetLoader = AvaloniaLocator.Current.GetService<IAssetLoader>();
            var source = $"avares://{RootDirectory}/Assets/{assetName}";
#if DEBUG
            Console.WriteLine($"AssetLoader: loading {source}");
#endif
            return assetLoader.Open(new Uri(source));
        }
    }
}