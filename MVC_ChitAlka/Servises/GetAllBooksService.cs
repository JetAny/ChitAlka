
using Microsoft.EntityFrameworkCore;
using MVC_ChitAlka.Intrfaces;
using DB_ChitAlka.Areas.Identity.Data;

namespace MVC_ChitAlka.Servises
{
    public class GetAllBooksService : IGetAllBooksService
    {
        private readonly MyDbContext _dbContext;
        public GetAllBooksService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<AuthorModel> GetAll()
        {
            var allAuthorDB = _dbContext.Authors
                .Include(u => u.Book)
                .ToList();
            ServiceMap _serviceMap = new ServiceMap();
            var getAllAuthors = _serviceMap.MapDbModel(allAuthorDB);

            return getAllAuthors;
        }
    }
}
