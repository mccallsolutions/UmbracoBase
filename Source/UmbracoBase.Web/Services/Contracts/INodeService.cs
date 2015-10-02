namespace UmbracoBase.Web.Services.Contracts
{
    using System.Collections.Generic;
    using Models.DocumentTypes;
    using Models.DocumentTypes.SiteData;

    public interface INodeService
    {
        T GetPage<T>(int pageId) where T : BaseDocumentType, new();
        IEnumerable<T> GetChildren<T>(int rootId = -1) where T : BaseDocumentType, new();
        IEnumerable<T> GetAncestors<T>(int id) where T : BaseDocumentType, new();
        IEnumerable<T> GetAncestors<T>(BaseDocumentType baseDocumentType) where T : BaseDocumentType, new();
        IEnumerable<Website> GetWebsites();
    }
}