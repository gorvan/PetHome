namespace PetHome.Shared.Core.Abstractions
{
    public interface IQueryHandler<TResponse, in TQuery> where TQuery : IQuery
    {
        public Task<TResponse> Execute(TQuery query, CancellationToken token);
    }
}
