namespace CourseWork.Views.MenuScreens
{
    public class InteractiveScreen : Avalonia.Controls.UserControl, IInteractiveScreen
    {
        public event IInteractiveScreen.NavigateToScreenHandler? NavigationToScreenRequested;

        public void NavigateToScreen(IInteractiveScreen.CommonLocations screen)
        {
            NavigationToScreenRequested?.Invoke(new IInteractiveScreen.NavigateToScreenArgs(screen));
        }

        public void NavigateToScreen(IInteractiveScreen.CommonLocations screen, object? arg)
        {
            NavigationToScreenRequested?.Invoke(new IInteractiveScreen.NavigateToScreenArgs(screen, arg));
        }
    }
}