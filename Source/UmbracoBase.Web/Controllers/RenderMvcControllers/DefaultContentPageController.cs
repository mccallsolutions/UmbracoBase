namespace UmbracoBase.Web.Controllers.RenderMvcControllers
{
    using System.Web.Mvc;
    using Models.DocumentTypes.WebPages.ContentPages;
    using Services.Contracts;
    using Umbraco.Web.Models;
    using Umbraco.Web.Mvc;

    public partial class DefaultContentPageController : RenderMvcController
    {
        private readonly INodeService _nodeService;

        public DefaultContentPageController(INodeService nodeService)
        {
            _nodeService = nodeService;
        } 

        public override ActionResult Index(RenderModel model)
        {
            var defaultContentPage = _nodeService.GetPage<DefaultContentPage>(CurrentPage.Id);
            return CurrentTemplate(defaultContentPage);
        }
    }
}