namespace UmbracoBase.Tests.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Global;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using Moq;    
    using Umbraco.Core.Models;
    using Web.Models.DocumentTypes;
    using Web.Models.DocumentTypes.SiteData;
    using Web.Models.DocumentTypes.WebPages;
    using Web.Models.DocumentTypes.WebPages.ContentPages;
    using Web.Services;
    using Web.Services.Contracts;
    using Zone.UmbracoMapper;

    [TestClass]
    public class NodeServiceTests : BaseTests
    {
        private const int FirstWebsiteId = 1051;

        [TestMethod]
        public void TestGetPageExists()
        {
            const int id = 1;            
            var umbracoHelperService = new Mock<IUmbracoHelperService>();
            var publishedContentExtensionService = new Mock<IPublishedContentExtensionService>();
            var nodeService = new NodeService(new UmbracoMapper(), umbracoHelperService.Object,
                publishedContentExtensionService.Object);
            
            umbracoHelperService.Setup(x => x.TypedContent(id)).Returns(new MockPublishedContent(id));

            var baseDocumentType = nodeService.GetPage<BaseDocumentType>(id);

            Assert.AreEqual(id, baseDocumentType.Id);
        }

        [TestMethod]
        public void TestGetPageNotExists()
        {
            const int id = 1;
            var umbracoHelperService = new Mock<IUmbracoHelperService>();
            var publishedContentExtensionService = new Mock<IPublishedContentExtensionService>();
            var nodeService = new NodeService(new UmbracoMapper(), umbracoHelperService.Object,
                publishedContentExtensionService.Object);

            umbracoHelperService.Setup(x => x.TypedContent(id)).Returns((IPublishedContent) null);

            var baseDocumentType = nodeService.GetPage<BaseDocumentType>(id);

            Assert.IsNull(baseDocumentType);
        }

        [TestMethod]
        [DeploymentItem(Constants.DefaultDataFile, Constants.DataOutputPath)]
        public void TestGetChildrenFromDataNoRoot()
        {
            var umbracoHelperService = new Mock<IUmbracoHelperService>();
            umbracoHelperService.Setup(x => x.ContentAtRoot()).Returns(new MockPublishedContent(-1));

            var testRepository = new XmlUmbracoConfigRepository(Constants.DefaultDataFile);
            INodeService nodeService = SetupRepositoryNodeService(testRepository, umbracoHelperService.Object);          

            var children = nodeService.GetChildren<BaseDocumentType>().ToList();

            Assert.AreEqual(children[0].Id, FirstWebsiteId);            
        }

        [TestMethod]
        [DeploymentItem(Constants.DefaultDataFile, Constants.DataOutputPath)]
        public void TestGetBaseDocumentTypeChildrenFromDataRoot()
        {
            var testRepository = new XmlUmbracoConfigRepository(Constants.DefaultDataFile);
            Mock<IUmbracoHelperService> umbracoHelperService = SetupRepositoryTypedContent(testRepository, FirstWebsiteId);            
            INodeService nodeService = SetupRepositoryNodeService(testRepository, umbracoHelperService.Object);

            var children = nodeService.GetChildren<BaseDocumentType>(FirstWebsiteId).ToList();

            Assert.AreEqual(children[0].Id, 1058);
            Assert.AreEqual(children[1].Id, 1052);
        }               

        [TestMethod]
        [DeploymentItem(Constants.DefaultDataFile, Constants.DataOutputPath)]
        public void TestGetDefaultContentPageChildrenFromDataRoot()
        {            
            var testRepository = new XmlUmbracoConfigRepository(Constants.DefaultDataFile);
            Mock<IUmbracoHelperService> umbracoHelperService = SetupRepositoryTypedContent(testRepository, FirstWebsiteId);
            INodeService nodeService = SetupRepositoryNodeService(testRepository, umbracoHelperService.Object);

            var children = nodeService.GetChildren<DefaultContentPage>(FirstWebsiteId).ToList();

            Assert.AreEqual(children[0].Id, 1052);
        }

        [TestMethod]      
        public void TestGetWebsitesEmpty()
        {
            var umbracoHelperService = new Mock<IUmbracoHelperService>();
            var publishedContentExtensionService = new Mock<IPublishedContentExtensionService>();
            var nodeService = new NodeService(new UmbracoMapper(), umbracoHelperService.Object,
                publishedContentExtensionService.Object);

            umbracoHelperService.Setup(x => x.TypedContent(It.IsAny<int>()))
                .Returns((IPublishedContent) null);

            var websites = nodeService.GetWebsites().ToList();

            Assert.AreEqual(0, websites.Count);
        }

        [TestMethod]
        [DeploymentItem(Constants.DefaultDataFile, Constants.DataOutputPath)]
        public void TestGetWebsitesMultiple()
        {
            var umbracoHelperService = new Mock<IUmbracoHelperService>();
            umbracoHelperService.Setup(x => x.ContentAtRoot()).Returns(new MockPublishedContent(-1));

            var testRepository = new XmlUmbracoConfigRepository(Constants.DefaultDataFile);
            INodeService nodeService = SetupRepositoryNodeService(testRepository, umbracoHelperService.Object);

            var websites = nodeService.GetWebsites().ToList();

            Assert.AreEqual(1051, websites.First().Id);
        }

        [TestMethod]
        [DeploymentItem(Constants.DefaultDataFile, Constants.DataOutputPath)]
        public void TestGetAncestorsWebsite()
        { 
            var umbracoHelperService = new Mock<IUmbracoHelperService>();
            var currentContent = new BaseWebPage { Id = 1052 };
            var testRepository = new XmlUmbracoConfigRepository(Constants.DefaultDataFile);

            umbracoHelperService.Setup(x => x.TypedContent(currentContent.Id))
                .Returns(testRepository.GetNode(currentContent.Id));

            INodeService nodeService = SetupRepositoryNodeService(testRepository, umbracoHelperService.Object);

            IEnumerable<Website> websites = nodeService.GetAncestors<Website>(currentContent);

            Assert.AreEqual(1051, websites.First().Id);
        }
    }
}
