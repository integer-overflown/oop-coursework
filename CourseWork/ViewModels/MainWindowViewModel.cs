using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;

namespace CourseWork.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private int _currentScreenIndex;

        public int CurrentScreenIndex
        {
            get => _currentScreenIndex;
            set => this.RaiseAndSetIfChanged(ref _currentScreenIndex, value);
        }
    }
}