namespace UmbracoBase.Web.Services.Contracts
{
    using System.Collections.Generic;
    using Umbraco.Core.Models;

    public interface IPublishedContentExtensionService
    {
        IEnumerable<IPublishedContent> Ancestors(IPublishedContent currentContent);
        IEnumerable<IPublishedContent> Children(IPublishedContent currentContent);
    }
}
