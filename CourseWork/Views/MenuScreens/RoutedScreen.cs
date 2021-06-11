namespace CourseWork.Views.MenuScreens
{
    public class RoutedScreen : Avalonia.Controls.UserControl, IRoutedScreen
    {
        public event IRoutedScreen.ScreenNavigationHandler? NavigatedBack;
        public object? Previous { get; set; }

        public void NavigateBack()
        {
            NavigatedBack?.Invoke(this, new IRoutedScreen.ScreenNavigatedEventArgs(Previous));
        }
    }
}