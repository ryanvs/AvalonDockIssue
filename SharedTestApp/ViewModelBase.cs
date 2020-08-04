using Caliburn.Micro;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace CaliburnTestWpfApp.Modules.ViewModels
{
    public abstract class ViewModelBase : Screen
    {
        public ViewModelBase()
        {
            DisplayName = GetType().Name;
        }

        private bool _isDirty;

        public bool IsDirty
        {
            get { return _isDirty; }
            set { Set(ref _isDirty, value); }
        }

        private RelayCommand _closeCommand;

        public RelayCommand CloseCommand
        {
            get
            {
                return _closeCommand
                    ?? (_closeCommand = new RelayCommand(_ => TryCloseAsync(null)));
            }
        }

        public override Task<bool> CanCloseAsync(CancellationToken cancellationToken = default)
        {
            bool close = true;
            if (IsDirty)
            {
                var result = MessageBox.Show("Are you sure you want to close?", DisplayName, MessageBoxButton.YesNo);
                close = result == MessageBoxResult.Yes;
            }
            Trace.TraceInformation($"{GetType().Name}.CanCloseAsync: close={close}");
            return Task.FromResult(close);
        }

        protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
        {
            Trace.TraceInformation($"{GetType().Name}.OnDeactivateAsync: close={close}");
            return base.OnDeactivateAsync(close, cancellationToken);
        }
    }
}
