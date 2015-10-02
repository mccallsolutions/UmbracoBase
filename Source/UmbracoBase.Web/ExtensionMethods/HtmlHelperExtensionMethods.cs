namespace UmbracoBase.Web.ExtensionMethods
{
    using System;
    using System.Web.Mvc;
    using ClientDependency.Core;
    using ClientDependency.Core.Mvc;

    public static class HtmlHelperExtensionMethods
    {
        public static HtmlHelper RequiresJs(this HtmlHelper htmlHelper, string filePath, string providerName)
        {
            if (filePath == null)
            {
                throw new ArgumentException("File path can't be null");
            }

            if (providerName == null)
            {
                throw new ArgumentException("Provider name can't be null");
            }

            return htmlHelper.RequiresJs(new JavascriptFile(filePath) {ForceProvider = providerName});
        }
    }
}