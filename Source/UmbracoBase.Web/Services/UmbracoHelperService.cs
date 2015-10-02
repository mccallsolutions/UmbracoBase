namespace UmbracoBase.Web.Services
{
    using System.Collections.Generic;
    using Contracts;
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class UmbracoHelperService : IUmbracoHelperService
    {
        private readonly UmbracoHelper _umbracoHelper;

        public UmbracoHelperService()
        {
            _umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
        }

        public IPublishedContent TypedContent(int id)
        {
            return _umbracoHelper.TypedContent(id);
        }

        public IEnumerable<IPublishedContent> TypedContentAtXPath(string xpath)
        {
            return _umbracoHelper.TypedContentAtXPath(xpath);
        }

        public IPublishedContent TypedMedia(int id)
        {
            return _umbracoHelper.TypedMedia(id);
        }

        public dynamic ContentAtRoot()
        {
            return _umbracoHelper.ContentAtRoot();
        }
    }
}