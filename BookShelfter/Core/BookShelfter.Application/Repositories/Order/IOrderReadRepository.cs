namespace BookShelfter.Application.Repositories.Order;

public interface IOrderReadRepository:IReadRepository<Domain.Entities.Order>
{

    Task<Domain.Entities.Order?> GetOrderWithDetailsByIdAsync(int orderId);


}