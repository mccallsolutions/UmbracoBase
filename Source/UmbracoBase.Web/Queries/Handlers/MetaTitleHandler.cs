namespace UmbracoBase.Web.Queries.Handlers
{    
    using System.Linq;
    using System.Web.Mvc;
    using Framework;
    using Globals;
    using Models.DocumentTypes.SiteData;    
    using Services.Contracts;
    using Specifications;

    public class MetaTitleHandler : IQueryHandler<MetaTitleSpecification, MvcHtmlString>
    {
        private readonly INodeService _nodeService;

        public MetaTitleHandler(INodeService nodeService)
        {
            _nodeService = nodeService;
        }

        public MvcHtmlString Handle(MetaTitleSpecification spec)
        {            
            var website = _nodeService.GetAncestors<Website>(spec.PageId).FirstOrDefault();
            var siteName = website == null ? string.Empty : website.SiteName;

            return
                new MvcHtmlString(string.Format(
                    "{0}{1}{2}",
                    spec.PageTitle,
                    string.IsNullOrEmpty(siteName) || string.IsNullOrEmpty(spec.PageTitle)
                        ? string.Empty
                        : Constants.MetaTitleDelimiter,
                    siteName));
        }
    }
}