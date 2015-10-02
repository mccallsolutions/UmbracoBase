namespace UmbracoBase.Web.Models.DocumentTypes.SiteData
{
    using WebPages;
    using Zone.UmbracoMapper;    

    public class Website : BaseDocumentType
    {
        // Site settings tab
        public string SiteName { get; set; }

        // Generic tab
        [PropertyMapping(SourceProperty = "umbracoRedirect")]
        public BaseWebPage RedirectToNode { get; set; }
    }
}