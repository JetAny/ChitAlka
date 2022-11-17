using DB_ChitAlka.Interfases;
using Microsoft.EntityFrameworkCore;
using MVC_ChitAlka.Intrfaces;

namespace MVC_ChitAlka.Servises
{
    public class GetAllBooksService : IGetAllBooksService
    {
        private readonly IDbContext _dbContext;
        public GetAllBooksService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<AuthorModel> GetAll()
        {
            var allAuthorDB = _dbContext.Authors
                .Include(u => u.BookId)
                .ToList();
            ServiceMap _serviceMap = new ServiceMap();
            var getAllAuthors = _serviceMap.MapDbModel(allAuthorDB);

            return getAllAuthors;
        }
    }
}
