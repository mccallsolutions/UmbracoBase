namespace UmbracoBase.Tests.Contracts
{
    using System.Collections.Generic;
    using Umbraco.Core.Models;

    public interface IPublishedContentRepository
    {
        IEnumerable<IPublishedContent> GetList<T>(string xpath) where T : class;
        IEnumerable<IPublishedContent> Ancestors(IPublishedContent currentContent);
        IEnumerable<IPublishedContent> Children(IPublishedContent currentContent);
        IPublishedContent GetNode(int id);
    }
}
