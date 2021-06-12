namespace CourseWork.Views.MenuScreens
{
    public interface IInteractiveScreen
    {
        delegate void NavigateToScreenHandler(NavigateToScreenArgs args);

        enum CommonLocations
        {
            BookViewScreen
        }

        event NavigateToScreenHandler NavigationToScreenRequested;

        void NavigateToScreen(CommonLocations screen);
        void NavigateToScreen(CommonLocations screen, object? arg);

        public readonly struct NavigateToScreenArgs
        {
            public CommonLocations Location { get; }
            public object? Argument { get; }

            public NavigateToScreenArgs(CommonLocations location)
            {
                Location = location;
                Argument = null;
            }

            public NavigateToScreenArgs(CommonLocations location, object? argument)
            {
                Location = location;
                Argument = argument;
            }
        }
    }
}