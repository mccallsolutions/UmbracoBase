namespace UmbracoBase.Web.Services
{
    using System.Collections.Generic;
    using Contracts;
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class PublishedContentExtensionService : IPublishedContentExtensionService
    {
        public IEnumerable<IPublishedContent> Ancestors(IPublishedContent currentContent)
        {
            return currentContent.Ancestors();
        }

        public IEnumerable<IPublishedContent> Children(IPublishedContent currentContent)
        {
            return currentContent.Children;
        }
    }
}