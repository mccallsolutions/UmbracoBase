namespace UmbracoBase.Web.ExtensionMethods
{
    using System.Collections.Generic;
    using System.Linq;
    using Models.DocumentTypes.SiteData;
    using Models.DocumentTypes.WebPages;
    using Services.Contracts;

    public static class FalloverExtensionMethods
    {
        private static INodeService _nodeService;

        public static void FalloverProperties(this BaseWebPage baseWebPage, INodeService nodeService)
        {
            _nodeService = nodeService;

            baseWebPage.PageTitle = baseWebPage.FalloverPageTitle();
            baseWebPage.HeadingTitle = baseWebPage.FalloverHeadingTitle();
            baseWebPage.MenuTitle = baseWebPage.FalloverMenuTitle();
            baseWebPage.MetaAuthor = baseWebPage.FalloverMetaAuthor();
            baseWebPage.MetaKeywords = baseWebPage.FalloverMetaKeywords();
        }

        private static string FalloverPageTitle(this BaseWebPage baseWebPage)
        {
            return string.IsNullOrEmpty(baseWebPage.PageTitle) ? baseWebPage.Name : baseWebPage.PageTitle;
        }

        private static string FalloverHeadingTitle(this BaseWebPage baseWebPage)
        {
            return string.IsNullOrEmpty(baseWebPage.HeadingTitle) ? FalloverPageTitle(baseWebPage) : baseWebPage.HeadingTitle;
        }

        private static string FalloverMenuTitle(this BaseWebPage baseWebPage)
        {
            return string.IsNullOrEmpty(baseWebPage.MenuTitle) ? FalloverHeadingTitle(baseWebPage) : baseWebPage.MenuTitle;
        }

        private static string FalloverMetaAuthor(this BaseWebPage baseWebPage)
        {
            if (baseWebPage == null)
            {
                return string.Empty;
            }

            if (!string.IsNullOrEmpty(baseWebPage.MetaAuthor))
            {
                return baseWebPage.MetaAuthor;
            }

            var website = _nodeService.GetAncestors<Website>(baseWebPage).FirstOrDefault();

            return website == null ? string.Empty : website.SiteName;
        }

        private static string FalloverMetaKeywords(this BaseWebPage baseWebPage)
        {
            if (baseWebPage == null)
            {
                return string.Empty;
            }

            if (!string.IsNullOrEmpty(baseWebPage.MetaKeywords))
            {
                return baseWebPage.MetaKeywords;
            }

            if (string.IsNullOrEmpty(baseWebPage.MetaDescription))
            {
                return string.Empty;
            }

            var wordsToExclude = new List<string>
            {
                "a", "the", "an", "it", "is", "are", "when", "then", "i", "me", "my", "to", "if", "page", "this"
            };

            IEnumerable<string> keywords = baseWebPage.MetaDescription.Split(' ')
                .Select(x => new string(x.Where(c => !char.IsPunctuation(c)).ToArray()).ToLower())       
                .Where(x => !wordsToExclude.Contains(x));

            return string.Join(",", keywords).ToLower();
        }
    }
}