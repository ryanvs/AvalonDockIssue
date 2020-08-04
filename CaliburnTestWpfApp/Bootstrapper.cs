using Caliburn.Micro;
using CaliburnTestWpfApp.Modules.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace CaliburnTestWpfApp
{
    public class Bootstrapper : Caliburn.Micro.BootstrapperBase
    {
        public Bootstrapper()
        {
            LogManager.GetLog = type => new CaliburnLogger(type);
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            base.OnStartup(sender, e);
            DisplayRootViewFor<MainWindowViewModel>();
        }
    }
}
