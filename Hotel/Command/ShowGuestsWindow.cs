using System.Windows.Input;
using Hotel.ViewModel;
using Hotel.View;
using System;

namespace Hotel.Command
{
    internal class ShowGuestsWindow : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private MainWindowViewModel mainWindowViewModel;
        
        public ShowGuestsWindow(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            GuestsView view = new GuestsView();
            view.Show();
        }
    }
}

