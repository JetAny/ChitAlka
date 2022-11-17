using DB_ChitAlka.Interfases;
using Microsoft.EntityFrameworkCore;
using MVC_ChitAlka.Intrfaces;

namespace MVC_ChitAlka.Servises
{
    public class GetSectionBookService: IGetSectionBookService
    {
        private readonly IDbContext _dbContext;
        public GetSectionBookService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<SectionModel> GetSection(int bookId, int sectionID)
        {
            var BookDB = _dbContext.Books
                .Include(u => u.Section)
                .ToList();
            ServiceMap _serviceMap = new ServiceMap();
            var getSection = _serviceMap.MapDbSection(BookDB, bookId, sectionID);
            return getSection;
        }
    }
}
