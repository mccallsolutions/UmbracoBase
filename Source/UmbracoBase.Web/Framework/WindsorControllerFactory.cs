namespace UmbracoBase.Web.Framework
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Castle.Windsor;

    public class WindsorControllerFactory : DefaultControllerFactory
    {
        readonly IWindsorContainer _container;

        public WindsorControllerFactory(IWindsorContainer container)
        {
            _container = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            // This factory allows service interfaces to be resolved in contoller constructors automatically
            if (controllerType != null && _container.Kernel.HasComponent(controllerType))
                return (IController)_container.Resolve(controllerType);

            return base.GetControllerInstance(requestContext, controllerType);
        }

        public override void ReleaseController(IController controller)
        {
            _container.Release(controller);
        }
    }
}