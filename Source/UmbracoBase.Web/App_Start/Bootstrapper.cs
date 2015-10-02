namespace UmbracoBase.Web
{
    using Castle.Windsor;
    using Installers;
    using MapperConfigurations;

    public class Bootstrapper
    {
        private static IWindsorContainer _container;

        public static IWindsorContainer Container
        {
            get { return _container; }             
        }

        public static IWindsorContainer BootUp()
        {
            _container = new WindsorContainer();

            // Dependency injections
            _container.Install(
                new ControllersInstaller(),
                new ServiceInstaller());          

            return _container;
        }

        public static void ShutDown()
        {
            _container.Dispose();
        }
    }
}