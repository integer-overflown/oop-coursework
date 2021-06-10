using System;
using Avalonia;
using Avalonia.ReactiveUI;
using CourseWork.Database;

namespace CourseWork
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        public static void Main(string[] args)
        {
            // it's important to call database setup BEFORE framework initialization
            using var ctx = new BookContext();
            if (ctx.Database.EnsureCreated()) Console.WriteLine("Created database file");
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();
    }
}