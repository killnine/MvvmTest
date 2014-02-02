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

using GalaSoft.MvvmLight;
using Ninject;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Database.Server;
using View.ViewModels;
using View.ViewModels.Administration;

namespace View.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// 
    /// </summary>
    public class ViewModelLocator
    {
        readonly StandardKernel _kernel = new StandardKernel();

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {  
            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view services and models
                _kernel.Bind<IDocumentStore>().ToMethod(context =>
                {
                    NonAdminHttp.EnsureCanListenToWhenInNonAdminContext(8080);
                    var documentStore = new EmbeddableDocumentStore
                    {
                        DataDirectory = "Data",
                        UseEmbeddedHttpServer = true
                    };
                    return documentStore.Initialize();
                }).InSingletonScope();
                _kernel.Bind<MachineViewModel>().ToSelf();
                _kernel.Bind<MachineStatusViewModel>().ToSelf();
                _kernel.Bind<SystemsViewModel>().ToSelf();
            }
            else
            {
                // Create run time view services and models
                _kernel.Bind<IDocumentStore>().ToMethod(context =>
                {
                    NonAdminHttp.EnsureCanListenToWhenInNonAdminContext(8080);
                    var documentStore = new EmbeddableDocumentStore
                    {
                        DataDirectory = "Data",
                        UseEmbeddedHttpServer = true
                    };
                    return documentStore.Initialize();
                }).InSingletonScope();
                _kernel.Bind<MachineViewModel>().ToSelf();
                _kernel.Bind<MachineStatusViewModel>().ToSelf();
                _kernel.Bind<SystemsViewModel>().ToSelf();
            }
        }

        public MachineViewModel MachineViewModel
        {
            get { return _kernel.Get<MachineViewModel>(); }
        }

        public MachineStatusViewModel MachineStatus
        {
            get { return _kernel.Get<MachineStatusViewModel>(); }
        }

        public SystemsViewModel SystemsViewModel
        {
            get { return _kernel.Get<SystemsViewModel>(); }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}