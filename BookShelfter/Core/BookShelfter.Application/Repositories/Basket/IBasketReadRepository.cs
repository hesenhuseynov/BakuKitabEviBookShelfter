using Google.Api.Gax.Rest;

namespace BookShelfter.Application.Repositories.Basket;

public interface IBasketReadRepository:IReadRepository<Domain.Entities.Basket>
{


     Task<Domain.Entities.Basket?> GetByUserIdAsync(string userId);

     Task<Domain.Entities.Basket> GetBasketByUserIdAsync(string userId);




}