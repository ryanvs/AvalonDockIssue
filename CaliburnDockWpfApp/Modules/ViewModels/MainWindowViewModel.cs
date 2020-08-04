using Caliburn.Micro;
using CaliburnTestWpfApp.Modules.Views;

namespace CaliburnTestWpfApp.Modules.ViewModels
{
    public class MainWindowViewModel : MainWindowViewModelBase
    {
        private RelayCommand _showACommand;
        private RelayCommand _showBCommand;
        private RelayCommand _showCCommand;

        public MainWindowViewModel()
        {
            SetShowCommand<AViewModel>(ref _showACommand);
            SetShowCommand<BViewModel>(ref _showBCommand);
            SetShowCommand<CViewModel>(ref _showCCommand);
        }

        private void SetShowCommand<T>(ref RelayCommand command)
            where T : Screen
        {
            command = new RelayCommand(_ => ActivateOrCreate<T>());
        }

        public RelayCommand ShowACommand { get { return _showACommand; } }
        public RelayCommand ShowBCommand { get { return _showBCommand; } }
        public RelayCommand ShowCCommand { get { return _showCCommand; } }
    }
}
