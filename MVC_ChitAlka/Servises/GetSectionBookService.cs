
using Microsoft.EntityFrameworkCore;
using MVC_ChitAlka.Intrfaces;
using DB_ChitAlka.Areas.Identity.Data;

namespace MVC_ChitAlka.Servises
{
    public class GetSectionBookService: IGetSectionBookService
    {
        private readonly MyDbContext _dbContext;
        public GetSectionBookService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<SectionModel> GetSection(int bookId, int sectionID)
        {
            var BookDB = _dbContext.Books
                .Include(u => u.Sections)
                .ToList();
            ServiceMap _serviceMap = new ServiceMap();
            var getSection = _serviceMap.MapDbSection(BookDB, bookId, sectionID);
            return getSection;
        }
    }
}
