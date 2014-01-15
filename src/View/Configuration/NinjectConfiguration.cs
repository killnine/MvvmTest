using CommonServiceLocator.NinjectAdapter;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using Ninject.Extensions.Conventions;

namespace View.Configuration
{
    public class NinjectConfiguration
    {
        public void Setup()
        {
            var ninjectKernel = new StandardKernel();

            /*ninjectKernel.Scan( kernel =>
                                    {
                                        kernel.FromAssemblyContaining<NinjectConfiguration>();

                                        // If you would like to scan more assemblies uncomment the line below and 
                                        //  add in the correct object references.
                                        //kernel.FromAssemblyContaining<IFoo>();

                                        kernel.BindWithDefaultConventions();
                                    } );

            var ninjectServiceLocator = new NinjectServiceLocator(ninjectKernel);

            ServiceLocator.SetLocatorProvider( () => ninjectServiceLocator );*/


            // NOTE
            // once you have this code added you will need to make a call to 
            // new NinjectConfiguration().Setup(); in order to have the setup called.
        }
    }
}
