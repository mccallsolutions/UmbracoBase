namespace UmbracoBase.Tests.Mocks
{
    using Umbraco.Core.Models;

    public class MockPublishedProperty : IPublishedProperty
    {
        public MockPublishedProperty(string propertyTypeAlias, object value)
        {
            PropertyTypeAlias = propertyTypeAlias;
            Value = value;
        }

        public string PropertyTypeAlias { get; private set; }
        public bool HasValue { get; private set; }
        public object DataValue { get; private set; }
        public object Value { get; private set; }
        public object XPathValue { get; private set; }
    }
}
