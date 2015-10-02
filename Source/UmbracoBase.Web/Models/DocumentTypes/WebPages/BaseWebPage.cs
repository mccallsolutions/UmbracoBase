namespace UmbracoBase.Web.Models.DocumentTypes.WebPages
{
    public class BaseWebPage : BaseDocumentType
    {
        // Contents tab
        public string PageTitle { get; set; }
        public string HeadingTitle { get; set; }
        public string MenuTitle { get; set; }

        // SEO tab
        public string MetaAuthor { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }

        // Generic tab
        public bool HideInNavigation { get; set; }
        public BaseWebPage RedirectToNode { get; set; }
        public bool ExcludeFromSearch { get; set; }
        public string UrlAlias { get; set; }
    }
}