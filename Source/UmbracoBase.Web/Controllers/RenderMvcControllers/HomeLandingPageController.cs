namespace UmbracoBase.Web.Controllers.RenderMvcControllers
{
    using System.Web.Mvc;
    using Models.DocumentTypes.WebPages.LandingPages;
    using Services.Contracts;
    using Umbraco.Web.Models;
    using Umbraco.Web.Mvc;

    public partial class HomeLandingPageController : RenderMvcController
    {
        private readonly INodeService _nodeService;

        public HomeLandingPageController(INodeService nodeService)
        {
            _nodeService = nodeService;
        } 

        public override ActionResult Index(RenderModel model)
        {
            var homeLandingPage = _nodeService.GetPage<HomeLandingPage>(CurrentPage.Id);            
            return CurrentTemplate(homeLandingPage);            
        }
    }
}