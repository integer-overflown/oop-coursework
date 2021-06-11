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

        public readonly struct NavigateToScreenArgs
        {
            public CommonLocations Location { get; }

            public NavigateToScreenArgs(CommonLocations location)
            {
                Location = location;
            }
        }
    }
}