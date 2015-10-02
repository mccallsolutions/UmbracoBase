namespace UmbracoBase.Tests.Tests
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Umbraco.Core.Models;
    using Web;
    using Web.Framework;
    using Web.Globals;
    using Web.Queries.Specifications;
    using Web.Services;
    using Web.Services.Contracts;
    using Zone.UmbracoMapper;

    [TestClass]
    public class MetaTitleHandlerTests : BaseQueryHandlerTests
    {
        private const string PageTitle = "Page title";
        private const string SiteName = "UmbracoBase Framework";       

        [TestMethod]
        public void TestMetaTitleWithNoWebsite()
        {
            var umbracoHelperService = new Mock<IUmbracoHelperService>();
            var publishedContentExtensionService = new Mock<IPublishedContentExtensionService>();
            var nodeService = new NodeService(new UmbracoMapper(), umbracoHelperService.Object,
                publishedContentExtensionService.Object);

            umbracoHelperService.Setup(x => x.TypedContentAtXPath(It.IsAny<string>()))
                .Returns((IEnumerable<IPublishedContent>)null);
         
            OverrideRegisteredIUmbracoHelperService(umbracoHelperService.Object);
            OverrideRegisteredINodeService(nodeService);

            var queryService = Bootstrapper.Container.Resolve<IQueryService>();

            MvcHtmlString metaTitle = queryService.ExecuteQuery(new MetaTitleSpecification(1052, PageTitle));

            Assert.AreEqual(PageTitle, metaTitle.ToString());
        }

        [TestMethod]
        public void TestMetaTitleWithNoPageTitleAndWebsite()
        {
            var umbracoHelperService = new Mock<IUmbracoHelperService>();
            var publishedContentExtensionService = new Mock<IPublishedContentExtensionService>();
            var nodeService = new NodeService(new UmbracoMapper(), umbracoHelperService.Object,
                publishedContentExtensionService.Object);

            umbracoHelperService.Setup(x => x.TypedContentAtXPath(It.IsAny<string>()))
                .Returns((IEnumerable<IPublishedContent>)null);

            OverrideRegisteredIUmbracoHelperService(umbracoHelperService.Object);
            OverrideRegisteredINodeService(nodeService);

            var queryService = Bootstrapper.Container.Resolve<IQueryService>();

            MvcHtmlString metaTitle = queryService.ExecuteQuery(new MetaTitleSpecification(1052, string.Empty));

            Assert.AreEqual(string.Empty, metaTitle.ToString());
        }

        [TestMethod]
        [DeploymentItem(UmbracoBase.Tests.Global.Constants.DefaultDataFile, UmbracoBase.Tests.Global.Constants.DataOutputPath)]
        public void TestMetaTitleWithPageTitleWebsite()
        {
            const int id = 1052;

            var testRepository = new XmlUmbracoConfigRepository(UmbracoBase.Tests.Global.Constants.DefaultDataFile);
            Mock<IUmbracoHelperService> umbracoHelperService = SetupRepositoryTypedContent(testRepository, id);
            INodeService nodeService = SetupRepositoryNodeService(testRepository, umbracoHelperService.Object);

            OverrideRegisteredIUmbracoHelperService(umbracoHelperService.Object);
            OverrideRegisteredINodeService(nodeService);

            var queryService = Bootstrapper.Container.Resolve<IQueryService>();

            MvcHtmlString metaTitle = queryService.ExecuteQuery(new MetaTitleSpecification(id, PageTitle));

            var expectedMetaTitle = string.Format(
                    "{0}{1}{2}",
                    PageTitle,
                    Constants.MetaTitleDelimiter,
                    SiteName);

            Assert.AreEqual(expectedMetaTitle, metaTitle.ToString());
        }

        [TestMethod]
        [DeploymentItem(UmbracoBase.Tests.Global.Constants.DefaultDataFile, UmbracoBase.Tests.Global.Constants.DataOutputPath)]
        public void TestMetaTitleWithoutPageTitleWithWebsite()
        {
            const int id = 1052;

            var testRepository = new XmlUmbracoConfigRepository(UmbracoBase.Tests.Global.Constants.DefaultDataFile);
            Mock<IUmbracoHelperService> umbracoHelperService = SetupRepositoryTypedContent(testRepository, id);                               
            INodeService nodeService = SetupRepositoryNodeService(testRepository, umbracoHelperService.Object);

            OverrideRegisteredIUmbracoHelperService(umbracoHelperService.Object);
            OverrideRegisteredINodeService(nodeService);

            var queryService = Bootstrapper.Container.Resolve<IQueryService>();

            MvcHtmlString metaTitle = queryService.ExecuteQuery(new MetaTitleSpecification(id, null));

            Assert.AreEqual(SiteName, metaTitle.ToString());
        }
    }
}
