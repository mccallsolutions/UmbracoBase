namespace UmbracoBase.Web.Framework
{
    public interface IQueryHandler<in TQuery, out TResult> where TQuery : IQuerySpec<TResult>
    {
        TResult Handle(TQuery query);
    }
}