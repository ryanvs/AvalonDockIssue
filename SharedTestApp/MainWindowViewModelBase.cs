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
            var item = Items.OfType<T>().FirstOrDefault();
            if (item == null)
            {
                item = (T)Activator.CreateInstance(typeof(T));
                item.Parent = this;
                item.ConductWith(this);
            }
            return ActivateItemAsync(item, CancellationToken.None);
        }

#if CALIBURN_40
        protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
        {
            Trace.TraceInformation($"{GetType().Name}.OnDeactivateAsync: close={close}");
            return base.OnDeactivateAsync(close, cancellationToken);
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            Trace.TraceInformation($"{GetType().Name}.OnInitializeAsync");

            Task.Run(() => StartInitialDocuments());

            return base.OnInitializeAsync(cancellationToken);
        }
#else
        public Task ActivateItemAsync(Screen item, CancellationToken cancellationToken)
        {
            ActivateItem(item);
            return Task.FromResult(0);
        }

        protected override void OnDeactivate(bool close)
        {
            Trace.TraceInformation($"{GetType().Name}.OnDeactivate: close={close}");
            base.OnDeactivate(close);
        }

        protected override void OnInitialize()
        {
            Trace.TraceInformation($"{GetType().Name}.OnInitialize");

            Task.Run(() => StartInitialDocuments());

            base.OnInitialize();
        }
#endif

        protected async Task StartInitialDocuments()
        {
            await Task.Delay(100);
            await ActivateOrCreate<AViewModel>();
            await Task.Delay(100);
            await ActivateOrCreate<BViewModel>();
            await Task.Delay(100);
            await ActivateOrCreate<CViewModel>();
        }
    }
}
