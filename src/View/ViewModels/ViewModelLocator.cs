using Autofac;
using Autofac.Extras.CommonServiceLocator;
using Microsoft.Practices.ServiceLocation;
using Raven.Client;
using View.ViewModels;
using View.ViewModels.Administration;

namespace View.ViewModel
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterInstance(App.DocumentStore).As<IDocumentStore>().SingleInstance();
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