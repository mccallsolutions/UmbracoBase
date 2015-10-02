namespace UmbracoBase.Web.Controllers.SurfaceControllers
{
    using System.Web.Mvc;
    using Framework;
    using Models.DocumentTypes.WebPages;
    using Queries.Specifications;
    using Services.Contracts;
    using Umbraco.Web.Mvc;

    public partial class MetaController : SurfaceController
    {
        private readonly IQueryService _queryService;
        private readonly INodeService _nodeService;

        public MetaController(IQueryService queryService, INodeService nodeService)
        {
            _queryService = queryService;
            _nodeService = nodeService;
        }        

        [ChildActionOnly]
        public virtual ActionResult MetaTitle(string pageTitle)
        {
            MvcHtmlString metaTitle = _queryService.ExecuteQuery(new MetaTitleSpecification(CurrentPage.Id, pageTitle));
            return PartialView(Views._MetaTitle, metaTitle);
        }

        [ChildActionOnly]
        public virtual ActionResult MetaElements()
        {
            var baseWebPage = _nodeService.GetPage<BaseWebPage>(CurrentPage.Id);
            return PartialView(Views._MetaElements, baseWebPage);
        }
    }
}