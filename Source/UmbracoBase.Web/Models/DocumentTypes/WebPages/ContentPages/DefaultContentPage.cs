namespace UmbracoBase.Web.Models.DocumentTypes.WebPages.ContentPages
{
    using Zone.UmbracoMapper;

    public class DefaultContentPage : BaseWebPage
    {
        // Contents tab
        public MediaFile MainImage { get; set; }
        public string BodyText { get; set; }
    }
}