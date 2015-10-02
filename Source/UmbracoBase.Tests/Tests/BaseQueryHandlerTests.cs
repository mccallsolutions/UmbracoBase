namespace UmbracoBase.Tests.Tests
{
    using Castle.MicroKernel.Registration;
    using ExtentionMethods;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Web;
    using Web.Services.Contracts;

    [TestClass]
    public class BaseQueryHandlerTests : BaseTests
    {
        public BaseQueryHandlerTests()
        {
            Bootstrapper.BootUp();
        }

        ~BaseQueryHandlerTests()
        {
            Bootstrapper.ShutDown();
        }

        protected void OverrideRegisteredIUmbracoHelperService(IUmbracoHelperService umbracoHelperServiceOverride)
        {
            Bootstrapper.Container.Register(
                Component.For<IUmbracoHelperService>()
                    .Instance(umbracoHelperServiceOverride)
                    .OverridesExistingRegistration());
        }

        protected void OverrideRegisteredINodeService(INodeService nodeService)
        {
            Bootstrapper.Container.Register(
                Component.For<INodeService>()
                    .Instance(nodeService)
                    .OverridesExistingRegistration());
        }
    }
}
