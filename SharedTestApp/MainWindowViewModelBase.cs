using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CaliburnTestWpfApp.Modules.ViewModels
{
    public class MainWindowViewModelBase : Conductor<Screen>.Collection.OneActive
    {
        public Task ActivateOrCreate<T>()
            where T : Screen
        {
            var existing = Items.OfType<T>().FirstOrDefault();
            if (existing == null)
            {
                var item = (T)Activator.CreateInstance(typeof(T));
                item.Parent = this;
                item.ConductWith(this);
                return ActivateItemAsync(item, CancellationToken.None);
            }
            return ActivateItemAsync(existing, CancellationToken.None);
        }

        protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
        {
            Trace.TraceInformation($"{GetType().Name}.OnDeactivateAsync: close={close}");
            return base.OnDeactivateAsync(close, cancellationToken);
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            Trace.TraceInformation($"{GetType().Name}.OnInitializeAsync");

            ActivateOrCreate<AViewModel>();
            ActivateOrCreate<BViewModel>();
            ActivateOrCreate<CViewModel>();

            return base.OnInitializeAsync(cancellationToken);
        }
    }
}
