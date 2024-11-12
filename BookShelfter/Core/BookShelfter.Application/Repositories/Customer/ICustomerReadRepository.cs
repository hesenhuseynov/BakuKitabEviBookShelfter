namespace BookShelfter.Application.Repositories.Customer;

public interface ICustomerReadRepository:IReadRepository<Domain.Entities.Customer>
{
    Task<Domain.Entities.Customer?> GetCustomerByAppUserIdAsync(string appUserId);
}