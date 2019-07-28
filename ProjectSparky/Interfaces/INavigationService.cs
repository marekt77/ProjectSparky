using System;
using Windows.UI.Xaml.Controls;

namespace ProjectSparky.Interfaces
{
    public interface INavigationService
    {
        void Navigate(Type sourcePage);
        void Navigate(Frame targetFrame, Type sourcePage);
        void Navigate(Type sourcePage, object parameter);
        void GoBack();
    }
}
