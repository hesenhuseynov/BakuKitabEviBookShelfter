using BookShelfter.Application.Repositories.ProductImageFile;
using BookShelfter.Domain.Entities;
using BookShelfter.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Persistence.Repositories.BookImageFile
{
    public class BookImageFileReadRepository : ReadRepository<ProductImageFile>, IProductImageFileReadRepository
    {
        public BookImageFileReadRepository(BookShelfterDbContext context) : base(context)
        {
        }
    }
}
