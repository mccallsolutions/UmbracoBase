namespace UmbracoBase.Tests.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper.Mappers;
    using Contracts;
    using Moq;
    using Umbraco.Core.Models;
    using Web.Services;
    using Web.Services.Contracts;
    using Zone.UmbracoMapper;

    public class BaseTests
    {
        public BaseTests()
        {
            // http://stackoverflow.com/questions/18447148/automapper-3-0-this-type-is-not-supported-on-this-platform-imapperregistry
            var useless = new ListSourceMapper();          
        }

        protected INodeService SetupRepositoryNodeService(IPublishedContentRepository repository, IUmbracoHelperService umbracoHelperService)
        {
            var publishedContentExtensionService = new Mock<IPublishedContentExtensionService>();

            IEnumerable<IPublishedContent> children = null;
            publishedContentExtensionService.Setup(x => x.Children(It.IsAny<IPublishedContent>()))
                .Callback((IPublishedContent publishedContent) => children = repository.Children(publishedContent))
                .Returns(() => children.Select(x => x));

            IEnumerable<IPublishedContent> ancestors = null;
            publishedContentExtensionService.Setup(x => x.Ancestors(It.IsAny<IPublishedContent>()))
                .Callback((IPublishedContent publishedContent) => ancestors = repository.Ancestors(publishedContent))
                .Returns(() => ancestors.Select(x => x));

            return new NodeService(new UmbracoMapper(), umbracoHelperService,
                publishedContentExtensionService.Object);
        }

        protected Mock<IUmbracoHelperService> SetupRepositoryTypedContent(IPublishedContentRepository repository, int pageId)
        {
            var umbracoHelperService = new Mock<IUmbracoHelperService>();

            umbracoHelperService.Setup(x => x.TypedContent(pageId))
                .Returns(repository.GetNode(pageId));

            return umbracoHelperService;
        }
    }
}
