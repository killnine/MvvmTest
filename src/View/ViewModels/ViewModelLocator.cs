/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:View"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator.NinjectAdapter;
using GalaSoft.MvvmLight;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using View.ViewModels;

namespace View.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// 
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            
            StandardKernel k = new StandardKernel();
            ServiceLocator.SetLocatorProvider(() => new NinjectServiceLocator(k));

            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view services and models
                k.Bind<MachineStatusViewModel>().ToSelf();
                k.Bind<SystemsViewModel>().ToSelf();
            }
            else
            {
                // Create run time view services and models
                k.Bind<MachineStatusViewModel>().ToSelf();
                k.Bind<SystemsViewModel>().ToSelf();
            }
        }

        public MachineStatusViewModel MachineStatus
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MachineStatusViewModel>();
            }
        }

        public SystemsViewModel SystemsViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SystemsViewModel>();
            }
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}