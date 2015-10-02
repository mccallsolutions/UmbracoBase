namespace UmbracoBase.Web
{
    using System;
    using Umbraco.Web;

    public class Global : UmbracoApplication
    {
        protected override void OnApplicationStarted(object sender, EventArgs e)
        {            
            Bootstrapper.BootUp();
            base.OnApplicationStarting(sender, e);
        }

        protected override void OnApplicationEnd(object sender, EventArgs e)
        {            
            Bootstrapper.ShutDown();
            base.OnApplicationEnd(sender, e);
        }
    }
}