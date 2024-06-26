namespace InventoryManagement.CQRS.Queries.Interfaces
{
    public interface IQueryHandler<TRequest, TResponse> where TRequest : IQuery<TResponse>
    {
        Task<TResponse> Handle(TRequest request);
    }
}
