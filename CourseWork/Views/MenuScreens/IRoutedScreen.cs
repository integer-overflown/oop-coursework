namespace CourseWork.Views.MenuScreens
{
    public interface IRoutedScreen
    {
        delegate void ScreenNavigationHandler(object? sender, ScreenNavigatedEventArgs args);

        public object? Previous { get; set; }

        event ScreenNavigationHandler NavigatedBack;

        void NavigateBack();

        public readonly struct ScreenNavigatedEventArgs
        {
            public object? Destination { get; }

            public ScreenNavigatedEventArgs(object? destination)
            {
                Destination = destination;
            }
        }
    }
}