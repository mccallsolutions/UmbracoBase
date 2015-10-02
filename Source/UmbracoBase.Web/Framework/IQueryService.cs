namespace UmbracoBase.Web.Framework
{
    public interface IQueryService
    {
        TResult ExecuteQuery<TResult>(IQuerySpec<TResult> querySpec);
    }
}