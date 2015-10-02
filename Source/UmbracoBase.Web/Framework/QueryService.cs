namespace UmbracoBase.Web.Framework
{
    using Castle.Windsor;

    public class QueryService : IQueryService
    {
        private readonly IWindsorContainer _container;

        public QueryService(IWindsorContainer container)
        {
            _container = container;
        }

        public TResult ExecuteQuery<TResult>(IQuerySpec<TResult> querySpec)
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(querySpec.GetType(), typeof(TResult));
            var handler = _container.Resolve(handlerType);
            return (TResult)((dynamic)handler).Handle((dynamic)querySpec);
        }
    }
}