namespace UmbracoBase.Web.MapperConfigurations
{
    using System.Collections.Generic;
    using Models.DocumentTypes.WebPages;
    using Services.Contracts;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Zone.UmbracoMapper;

    public class UmbracoMapperMappings
    {
        public static IUmbracoHelperService UmbracoHelperService;        

        public static void Configure(IUmbracoMapper umbracoMapper, IUmbracoHelperService umbracoHelperService)
        {
            UmbracoHelperService = umbracoHelperService;
            umbracoMapper.AddCustomMapping(typeof(BaseWebPage).FullName, MapBaseWebPage);
            umbracoMapper.AddCustomMapping(typeof(MediaFile).FullName, MapMediaFile);
            umbracoMapper.AddCustomMapping(typeof(IEnumerable<MediaFile>).FullName, MapMediaFiles);            
        }

        private static object MapBaseWebPage(IUmbracoMapper mapper, IPublishedContent contentToMapFrom, string propertyName, bool recursive)
        {
            var obj = new BaseWebPage();
            mapper.Map(UmbracoHelperService.TypedContent(contentToMapFrom.GetPropertyValue<int>(propertyName)), obj);
            return obj;
        }

        private static object MapMediaFile(IUmbracoMapper mapper, IPublishedContent contentToMapFrom, string propertyName, bool recursive)
        {
            IPublishedContent publishedMediaItem = UmbracoHelperService.TypedMedia(contentToMapFrom.GetPropertyValue<int>(propertyName));            
            return GetMediaFile(mapper, publishedMediaItem);
        }        

        private static object MapMediaFiles(IUmbracoMapper mapper, IPublishedContent contentToMapFrom, string propertyName, bool recursive)
        {
            var mediaFiles = new List<MediaFile>();            

            foreach (var stringId in contentToMapFrom.GetPropertyValue<string>(propertyName).Split(','))
            {
                int id;

                if (!int.TryParse(stringId, out id))
                {
                    continue;
                }

                IPublishedContent publishedMediaItem = UmbracoHelperService.TypedMedia(id);
                MediaFile mediaFile = GetMediaFile(mapper, publishedMediaItem);

                if (mediaFile != null)
                {
                    mediaFiles.Add(mediaFile);
                }
            }

            return mediaFiles;
        }

        private static MediaFile GetMediaFile(IUmbracoMapper mapper, IPublishedContent publishedMediaItem)
        {
            if (publishedMediaItem == null)
            {
                return null;
            }

            var mediaFile = new MediaFile();
            mapper.Map(publishedMediaItem, mediaFile);
            return mediaFile;
        }
    }
}