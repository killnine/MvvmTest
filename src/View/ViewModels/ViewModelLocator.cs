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

using Autofac;
using Autofac.Extras.CommonServiceLocator;
using Microsoft.Practices.ServiceLocation;
using Raven.Client;
using Raven.Client.Embedded;
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
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        static ViewModelLocator()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterInstance(new EmbeddableDocumentStore()
                {
                    DataDirectory = "Data",
                    UseEmbeddedHttpServer = true
                }).As<IDocumentStore>().SingleInstance();

            containerBuilder.RegisterType<MachineViewModel>();
            containerBuilder.RegisterType<MachineStatusViewModel>();
            containerBuilder.RegisterType<SystemsViewModel>();

            var container = containerBuilder.Build();
            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));
        }

        public MachineViewModel MachineViewModel
        {
            get { return ServiceLocator.Current.GetInstance<MachineViewModel>(); }
        }

        public MachineStatusViewModel MachineStatus
        {
            get { return ServiceLocator.Current.GetInstance<MachineStatusViewModel>(); }
        }

        public SystemsViewModel SystemsViewModel
        {
            get { return ServiceLocator.Current.GetInstance<SystemsViewModel>(); }
        }
    }
}