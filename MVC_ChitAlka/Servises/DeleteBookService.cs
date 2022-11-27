using DB_ChitAlka.Areas.Identity.Data;
using MVC_ChitAlka.Intrfaces;
using Microsoft.EntityFrameworkCore;

namespace MVC_ChitAlka.Servises
{
    public class DeleteBookService : IDeleteBookService
    {
        private readonly MyDbContext _dbContext;

        public DeleteBookService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void BookDelete(int bookDelete)
        {
            var author = _dbContext.Authors
                                .Include(u => u.Book)
                                .First(p => p.Id == bookDelete);
            var book = _dbContext.Books
                     .Include(u => u.Sections)
                     .First(p => p.Id == bookDelete);

            
            foreach(var section in book.Sections)
            {
                _dbContext.Sections.Remove(section);
            }

            _dbContext.Authors.Remove(author);
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();

        }
    }
}
