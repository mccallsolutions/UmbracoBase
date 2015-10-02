namespace UmbracoBase.Web.Queries.Specifications
{
    using System.Web.Mvc;
    using Framework;

    public class MetaTitleSpecification : IQuerySpec<MvcHtmlString>
    {
        public int PageId { get; set; }
        public string PageTitle { get; set; }

        public MetaTitleSpecification(int pageId, string pageTitle)
        {
            PageId = pageId;
            PageTitle = pageTitle;
        }
    }
}