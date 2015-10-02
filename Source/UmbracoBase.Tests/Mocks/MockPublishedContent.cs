namespace UmbracoBase.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Xml.Linq;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;

    public class MockPublishedContent : IPublishedContent
    {
        public MockPublishedContent(int id)
        {
            Id = id;
            Name = "TestName";
            Path = string.Empty;
        }

        public MockPublishedContent(XElement element)
        {
            Id = int.Parse(element.Attribute("id").Value);
            Name = element.Attribute("nodeName").Value;
            DocumentTypeAlias = element.Attribute("nodeTypeAlias").Value;
            Path = element.Attribute("path").Value;

            if (!element.HasElements)
            {
                return;
            }

            Properties = new Collection<IPublishedProperty>();
            foreach (XElement property in element.Elements())
            {
                Properties.Add(new MockPublishedProperty(property.Name.ToString(), property.Value));
            }
        } 

        public IEnumerable<IPublishedContent> ContentSet { get; private set; }
        public PublishedContentType ContentType { get; private set; }
        public int Id { get; private set; }
        public int TemplateId { get; private set; }
        public int SortOrder { get; private set; }
        public string Name { get; private set; }
        public string UrlName { get; private set; }
        public string DocumentTypeAlias { get; private set; }
        public int DocumentTypeId { get; private set; }
        public string WriterName { get; private set; }
        public string CreatorName { get; private set; }
        public int WriterId { get; private set; }
        public int CreatorId { get; private set; }
        public string Path { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime UpdateDate { get; private set; }
        public Guid Version { get; private set; }
        public int Level { get; private set; }
        public string Url { get; private set; }
        public PublishedItemType ItemType { get; private set; }
        public bool IsDraft { get; private set; }
        public IPublishedContent Parent { get; private set; }
        public IEnumerable<IPublishedContent> Children { get; private set; }
        public ICollection<IPublishedProperty> Properties { get; private set; }

        public object this[string alias]
        {
            get { throw new NotImplementedException(); }
        }

        public int GetIndex()
        {
            throw new NotImplementedException();
        }

        public IPublishedProperty GetProperty(string alias)
        {
            return GetProperty(alias, false);
        }

        public IPublishedProperty GetProperty(string alias, bool recurse)
        {
            return Properties.FirstOrDefault(x => x.PropertyTypeAlias == alias);
        }
    }
}
