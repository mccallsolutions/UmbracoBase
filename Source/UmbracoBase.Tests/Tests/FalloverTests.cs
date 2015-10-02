namespace UmbracoBase.Tests.Tests
{
    using Data;
    using Global;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Web.Models.DocumentTypes.WebPages.ContentPages;
    using Web.Services.Contracts;

    [TestClass]
    public class FalloverTests : BaseTests
    {   
        [TestMethod]
        [DeploymentItem(Constants.DefaultDataFile, Constants.DataOutputPath)]
        public void TestMetaAuthorFalloverToMetaAuthor()
        {
            var testRepository = new XmlUmbracoConfigRepository(Constants.DefaultDataFile);
            Mock<IUmbracoHelperService> umbracoHelperService = SetupRepositoryTypedContent(testRepository, 1052);
            INodeService nodeService = SetupRepositoryNodeService(testRepository, umbracoHelperService.Object);

            var defaultContentPage = nodeService.GetPage<DefaultContentPage>(1052);

            Assert.AreEqual("UmbracoBase", defaultContentPage.MetaAuthor);
        }

        [TestMethod]
        [DeploymentItem(Constants.DefaultDataFile, Constants.DataOutputPath)]
        public void TestMetaAuthorFalloverToSiteName()
        {
            var testRepository = new XmlUmbracoConfigRepository(Constants.DefaultDataFile);
            Mock<IUmbracoHelperService> umbracoHelperService = SetupRepositoryTypedContent(testRepository, 1053);
            INodeService nodeService = SetupRepositoryNodeService(testRepository, umbracoHelperService.Object);

            var defaultContentPage = nodeService.GetPage<DefaultContentPage>(1053);

            Assert.AreEqual("UmbracoBase Framework", defaultContentPage.MetaAuthor);
        }

        [TestMethod]
        [DeploymentItem(Constants.DefaultDataFile, Constants.DataOutputPath)]
        public void TestMetaKeywordsFalloverToKeywords()
        {
            var testRepository = new XmlUmbracoConfigRepository(Constants.DefaultDataFile);
            Mock<IUmbracoHelperService> umbracoHelperService = SetupRepositoryTypedContent(testRepository, 1052);
            INodeService nodeService = SetupRepositoryNodeService(testRepository, umbracoHelperService.Object);

            var defaultContentPage = nodeService.GetPage<DefaultContentPage>(1052);

            Assert.AreEqual("Content, default, page", defaultContentPage.MetaKeywords);
        }

        [TestMethod]
        [DeploymentItem(Constants.DefaultDataFile, Constants.DataOutputPath)]
        public void TestMetaKeywordsFalloverToMetaDescription()
        {
            var testRepository = new XmlUmbracoConfigRepository(Constants.DefaultDataFile);
            Mock<IUmbracoHelperService> umbracoHelperService = SetupRepositoryTypedContent(testRepository, 1053);
            INodeService nodeService = SetupRepositoryNodeService(testRepository, umbracoHelperService.Object);

            var defaultContentPage = nodeService.GetPage<DefaultContentPage>(1053);

            Assert.AreEqual("default,content", defaultContentPage.MetaKeywords);
        }

        [TestMethod]
        [DeploymentItem(Constants.DefaultDataFile, Constants.DataOutputPath)]
        public void TestPageTitleFalloverToPageTitle()
        {
            var testRepository = new XmlUmbracoConfigRepository(Constants.DefaultDataFile);
            Mock<IUmbracoHelperService> umbracoHelperService = SetupRepositoryTypedContent(testRepository, 1052);
            INodeService nodeService = SetupRepositoryNodeService(testRepository, umbracoHelperService.Object);

            var defaultContentPage = nodeService.GetPage<DefaultContentPage>(1052);

            Assert.AreEqual("Page title", defaultContentPage.PageTitle);
        }

        [TestMethod]
        [DeploymentItem(Constants.DefaultDataFile, Constants.DataOutputPath)]
        public void TestPageTitleFalloverToPageName()
        {
            var testRepository = new XmlUmbracoConfigRepository(Constants.DefaultDataFile);
            Mock<IUmbracoHelperService> umbracoHelperService = SetupRepositoryTypedContent(testRepository, 1053);
            INodeService nodeService = SetupRepositoryNodeService(testRepository, umbracoHelperService.Object);

            var defaultContentPage = nodeService.GetPage<DefaultContentPage>(1053);

            Assert.AreEqual("Content Page", defaultContentPage.PageTitle);
        }

        [TestMethod]
        [DeploymentItem(Constants.DefaultDataFile, Constants.DataOutputPath)]
        public void TestHeadingTitleFalloverToHeadingTitle()
        {
            var testRepository = new XmlUmbracoConfigRepository(Constants.DefaultDataFile);
            Mock<IUmbracoHelperService> umbracoHelperService = SetupRepositoryTypedContent(testRepository, 1052);
            INodeService nodeService = SetupRepositoryNodeService(testRepository, umbracoHelperService.Object);

            var defaultContentPage = nodeService.GetPage<DefaultContentPage>(1052);

            Assert.AreEqual("Heading title", defaultContentPage.HeadingTitle);
        }

        [TestMethod]
        [DeploymentItem(Constants.DefaultDataFile, Constants.DataOutputPath)]
        public void TestHeadingTitleFalloverToPageTitle()
        {
            var testRepository = new XmlUmbracoConfigRepository(Constants.DefaultDataFile);
            Mock<IUmbracoHelperService> umbracoHelperService = SetupRepositoryTypedContent(testRepository, 1054);
            INodeService nodeService = SetupRepositoryNodeService(testRepository, umbracoHelperService.Object);

            var defaultContentPage = nodeService.GetPage<DefaultContentPage>(1054);

            Assert.AreEqual("Page title", defaultContentPage.HeadingTitle);
        }

        [TestMethod]
        [DeploymentItem(Constants.DefaultDataFile, Constants.DataOutputPath)]
        public void TestMenuTitleFalloverToMenuTitle()
        {
            var testRepository = new XmlUmbracoConfigRepository(Constants.DefaultDataFile);
            Mock<IUmbracoHelperService> umbracoHelperService = SetupRepositoryTypedContent(testRepository, 1052);
            INodeService nodeService = SetupRepositoryNodeService(testRepository, umbracoHelperService.Object);

            var defaultContentPage = nodeService.GetPage<DefaultContentPage>(1052);

            Assert.AreEqual("Menu title", defaultContentPage.MenuTitle);
        }

        [TestMethod]
        [DeploymentItem(Constants.DefaultDataFile, Constants.DataOutputPath)]
        public void TestMenuTitleFalloverToHeadingTitle()
        {
            var testRepository = new XmlUmbracoConfigRepository(Constants.DefaultDataFile);
            Mock<IUmbracoHelperService> umbracoHelperService = SetupRepositoryTypedContent(testRepository, 1055);
            INodeService nodeService = SetupRepositoryNodeService(testRepository, umbracoHelperService.Object);

            var defaultContentPage = nodeService.GetPage<DefaultContentPage>(1055);

            Assert.AreEqual("Heading title", defaultContentPage.MenuTitle);
        }
    }
}
