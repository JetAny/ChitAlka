
using Microsoft.EntityFrameworkCore;
using MVC_ChitAlka.Intrfaces;
using DB_ChitAlka.Areas.Identity.Data;


namespace MVC_ChitAlka.Servises
{
    public class GetBookService: IGetBookService
    {
        private readonly MyDbContext _dbContext;
        public GetBookService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public BookModel GetBook(int bookId)
        {
            var BookDB = _dbContext.Books
                .Include(u => u.Sections)
                .ToList();
            ServiceMap _serviceMap = new ServiceMap();
            var getBook = _serviceMap.MapDbBook(BookDB, bookId);

            return getBook;
        }
    }
}
