using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using WPF_Andersen.ViewModels;

namespace WPF_Andersen
{
    public class UpdateClientBootstrapper : BootstrapperBase
    {
        //public UpdateClientBootstrapper()
        //{
        //    Initialize();
        //}
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<UpdateViewModel>();
        }
    }
}
