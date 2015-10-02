namespace UmbracoBase.Web.Services
{    
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI;
    using Contracts;
    using ExtensionMethods;
    using MapperConfigurations;
    using Models.DocumentTypes;
    using Models.DocumentTypes.SiteData;
    using Models.DocumentTypes.WebPages;
    using Umbraco.Core.Models;
    using Zone.UmbracoMapper;

    public class NodeService : INodeService
    {
        private readonly IUmbracoHelperService _umbracoHelperService;
        private readonly IUmbracoMapper _umbracoMapper;
        private readonly IPublishedContentExtensionService _publishedContentExtensionService;

        public NodeService(
            IUmbracoMapper umbracoMapper, 
            IUmbracoHelperService umbracoHelperService, 
            IPublishedContentExtensionService publishedContentExtensionService)
        {
            _umbracoHelperService = umbracoHelperService;
            _umbracoMapper = umbracoMapper;
            _publishedContentExtensionService = publishedContentExtensionService;

            UmbracoMapperMappings.Configure(_umbracoMapper, _umbracoHelperService);            
        }

        public T GetPage<T>(int pageId) where T : BaseDocumentType, new()
        {
            IPublishedContent publishedContent = _umbracoHelperService.TypedContent(pageId);
            var page = MapPublishedContentToType<T>(publishedContent);

            if (page is BaseWebPage)
            {
                (page as BaseWebPage).FalloverProperties(this);
            }

            return page;
        }

        public IEnumerable<T> GetChildren<T>(int rootId = -1) where T : BaseDocumentType, new()
        {
            IPublishedContent root = rootId == -1
                ? _umbracoHelperService.ContentAtRoot()
                : _umbracoHelperService.TypedContent(rootId);

            if (root == null)
            {
                return new List<T>();
            }

            IEnumerable<IPublishedContent> children = _publishedContentExtensionService.Children(root);

            var childrenList = children as IList<IPublishedContent> ?? children.ToList();

            return !childrenList.Any()
                ? new List<T>()
                : childrenList.Select(MapPublishedContentToType<T>).Where(x => x != null);
        }

        public IEnumerable<T> GetAncestors<T>(int id) where T : BaseDocumentType, new()
        {
            return GetAncestors<T>(new BaseDocumentType {Id = id});
        }

        public IEnumerable<T> GetAncestors<T>(BaseDocumentType baseDocumentType) where T : BaseDocumentType, new()
        {
            if (baseDocumentType == null)
            {
                return null;
            }

            IPublishedContent content = _umbracoHelperService.TypedContent(baseDocumentType.Id);

            IEnumerable<IPublishedContent> ancestors = _publishedContentExtensionService.Ancestors(content);
         
            var ancestorsList = ancestors as IList<IPublishedContent> ?? ancestors.ToList();

            return !ancestorsList.Any()
                ? new List<T>()
                : ancestorsList.Select(MapPublishedContentToType<T>).Where(x => x != null);
        }

        public IEnumerable<Website> GetWebsites()
        {
            IEnumerable<Website> websites = GetChildren<Website>();
            return websites;
        }

        private T MapPublishedContentToType<T>(IPublishedContent publishedContent) where T : BaseDocumentType, new()
        {
            if (publishedContent == null)
            {
                return null;
            }            

            try
            {
                var t = new T();
                _umbracoMapper.Map(publishedContent, t);
                return t;
            }
            catch
            {
                return null;
            }           
        }
    }
}