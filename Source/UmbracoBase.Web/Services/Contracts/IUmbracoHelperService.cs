namespace UmbracoBase.Web.Services.Contracts
{
    using System.Collections.Generic;
    using Umbraco.Core.Models;

    public interface IUmbracoHelperService
    {
        IPublishedContent TypedContent(int id);
        IEnumerable<IPublishedContent> TypedContentAtXPath(string xpath);
        IPublishedContent TypedMedia(int id);
        dynamic ContentAtRoot();
    }
}
