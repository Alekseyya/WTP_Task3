﻿using System.Windows;
using Caliburn.Micro;

namespace WPF_Andersen
{
    public class ClientBootstrapper: BootstrapperBase
    {
        public ClientBootstrapper()
        {
            Initialize();
        }
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ClientViewModel>();
        }
    }
}
