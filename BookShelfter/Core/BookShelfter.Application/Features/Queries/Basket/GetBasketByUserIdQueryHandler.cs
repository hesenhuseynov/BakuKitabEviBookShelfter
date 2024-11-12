using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.DTOs.Basket;
using BookShelfter.Application.DTOs.Book;
using BookShelfter.Application.Repositories.Basket;
using BookShelfter.Domain.Entities;
using MediatR;

namespace BookShelfter.Application.Features.Queries.Basket
{
    public  class GetBasketByUserIdQueryHandler:IRequestHandler<GetBasketByUserIdQueryRequest,GetBasketByUserIdQueryResponse>
    {

        private readonly IBasketReadRepository _basketReadRepository;

        public GetBasketByUserIdQueryHandler(IBasketReadRepository basketReadRepository)
        {
            _basketReadRepository = basketReadRepository;
        }

        public async Task<GetBasketByUserIdQueryResponse> Handle(GetBasketByUserIdQueryRequest request, CancellationToken cancellationToken)
        {
            var basketUser = await _basketReadRepository.GetBasketByUserIdAsync(request.UserId);

            if (basketUser is null)
            {
                return new GetBasketByUserIdQueryResponse
                {
                    Message = "Kullanıcıya ait sepet bulunamadı.",
                    Success = false
                };
            }


            var basketItemsDto = basketUser.BasketItems?.Select(bi => new BasketItemDto()
            {
                BookId = bi.BookId,
                Quantity = bi.Quantity,
                UnitPrice = bi.UnitPrice,
                Book = new BookDto()
                {    
                    BookId = bi.Book.Id,
                    BookName = bi.Book.BookName,
                    AuthorName = bi.Book.AuthorName,
                    Price = bi.Book.Price,
                    Description = bi.Book.Description,
                    CategoryId = bi.Book.CategoryId,
                    IsFeatured = bi.Book.IsFeatured,
                    ImageUrls = bi.Book?.BookImagesFile?.Select(c => c.ImageUrl).ToList() ?? new List<string>()

                }

            }).ToList() ?? new List<BasketItemDto>();







            return new GetBasketByUserIdQueryResponse
            {
                BasketItems = basketItemsDto,
                Success = true,
                Message = "Kullanıcının sepeti başarıyla getirildi."
            };
        }

    }
}
