using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CaliburnTestWpfApp.Modules.ViewModels
{
    public class MainWindowViewModelBase : Conductor<Screen>.Collection.OneActive
    {
        private Screen _activeLayoutItem;

        public Screen ActiveLayoutItem
        {
            get { return _activeLayoutItem; }
            set
            {
                if (Set(ref _activeLayoutItem, value))
                {
                    ActivateItemAsync(value, CancellationToken.None);
                }
            }
        }

        protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
        {
            Trace.TraceInformation($"{GetType().Name}.OnDeactivateAsync: close={close}");
            return base.OnDeactivateAsync(close, cancellationToken);
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            Trace.TraceInformation($"{GetType().Name}.OnInitializeAsync");

            ActivateItemAsync(new AViewModel());
            ActivateItemAsync(new BViewModel());
            ActivateItemAsync(new CViewModel());

            return base.OnInitializeAsync(cancellationToken);
        }
    }
}
