using DB_ChitAlka.Interfases;
using Microsoft.EntityFrameworkCore;
using MVC_ChitAlka.Intrfaces;

namespace MVC_ChitAlka.Servises
{
    public class GetBookService: IGetBookService
    {
        private readonly IDbContext _dbContext;
        public GetBookService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public BookModel GetBook(int bookId)
        {
            var BookDB = _dbContext.Books
                .Include(u => u.Section)
                .ToList();
            ServiceMap _serviceMap = new ServiceMap();
            var getBook = _serviceMap.MapDbBook(BookDB, bookId);

            return getBook;
        }
    }
}
