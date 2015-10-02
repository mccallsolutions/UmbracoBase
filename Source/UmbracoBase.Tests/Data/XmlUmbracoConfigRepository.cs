namespace UmbracoBase.Tests.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.XPath;
    using Contracts;
    using Mocks;
    using Umbraco.Core.Models;

    public class XmlUmbracoConfigRepository : IPublishedContentRepository
    {
        private readonly XDocument _document;

        public XmlUmbracoConfigRepository(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("filePath", "Can't be null or empty");
            }

            if (!System.IO.File.Exists(filePath))
            {
                throw new FileNotFoundException("Can't find file " + filePath, filePath);
            }

            _document = XDocument.Load(filePath);
        }

        public IEnumerable<IPublishedContent> GetList<T>(string xpath) where T : class
        {
            if (string.IsNullOrEmpty(xpath))
            {
                throw new ArgumentNullException("xpath", "Can't be null or empty");
            }

            return _document.XPathSelectElements(xpath).Select(x => new MockPublishedContent(x));            
        }

        public IEnumerable<IPublishedContent> Ancestors(IPublishedContent currentContent)
        {
            if (currentContent == null)
            {
                return null;
            }

            var element = GetElement(currentContent.Id);

            return element == null
                ? null
                : element.Ancestors()
                    .Where(x => x.Attribute("id").Value != "-1")
                    .Select(x => new MockPublishedContent(x));
        }

        public IEnumerable<IPublishedContent> Children(IPublishedContent currentContent)
        {
            if (currentContent == null)
            {
                return null;
            }

            var root = GetElement(currentContent.Id);

            return root == null
                ? (_document.Root != null ? ElementsToMockPublishedContents(_document.Root.Elements()) : null)
                : ElementsToMockPublishedContents(root.Elements());
        }

        public IPublishedContent GetNode(int id)
        {
            var element = GetElement(id);
            return element == null ? null : new MockPublishedContent(element);
        }

        private XElement GetElement(int id)
        {
            string xpath = string.Format("//*[@isDoc and @id = '{0}']", id);
            return _document.XPathSelectElements(xpath).FirstOrDefault();
        }

        private IEnumerable<MockPublishedContent> ElementsToMockPublishedContents(IEnumerable<XElement> elements)
        {
            const string parentAttribute = "parentID";

            if (elements == null)
            {
                return null;
            }

            return elements.Where(x =>
                x.Attribute(parentAttribute) != null &&
                !string.IsNullOrEmpty(x.Attribute(parentAttribute).Value))
                .Select(x => new MockPublishedContent(x));
        }
    }
}
