namespace BookShelfter.Application.Repositories.Basket;

public interface IBasketWriteRepository:IWriteRepository<Domain.Entities.Basket>
{

    Task<bool> RemoveItemFromBasketAsync(string userId, int bookId);


    Task<bool> ClearAllBasketAsync(string userId);




}