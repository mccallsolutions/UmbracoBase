namespace UmbracoBase.Tests.ExtentionMethods
{
    using System;
    using Castle.MicroKernel.Registration;

    public static class CastleWindsorExtensionMethods
    {
        public static ComponentRegistration<T> OverridesExistingRegistration<T>(this ComponentRegistration<T> componentRegistration) where T : class
        {
            return componentRegistration
                                .Named(Guid.NewGuid().ToString())
                                .IsDefault();
        }
    }
}
