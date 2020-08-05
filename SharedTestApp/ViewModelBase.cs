using Caliburn.Micro;
using System;
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
#if CALIBURN_40
                    ?? (_closeCommand = new RelayCommand(_ => TryCloseAsync(null)));
#else
                    ?? (_closeCommand = new RelayCommand(_ => TryClose(null)));
#endif
            }
        }

#if CALIBURN_40
        public override Task<bool> CanCloseAsync(CancellationToken cancellationToken = default)
        {
            bool close = true;
            if (IsDirty)
            {
                var result = MessageBox.Show("Are you sure you want to close?", DisplayName, MessageBoxButton.YesNo, MessageBoxImage.Question);
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
#else
        public override void CanClose(Action<bool> callback)
        {
            bool close = true;
            if (IsDirty)
            {
                var result = MessageBox.Show("Are you sure you want to close?", DisplayName, MessageBoxButton.YesNo, MessageBoxImage.Question);
                close = result == MessageBoxResult.Yes;
            }
            Trace.TraceInformation($"{GetType().Name}.CanClose: close={close}");
            callback(close);
        }

        protected override void OnDeactivate(bool close)
        {
            Trace.TraceInformation($"{GetType().Name}.OnDeactivate: close={close}");
            base.OnDeactivate(close);
        }
#endif
    }
}
