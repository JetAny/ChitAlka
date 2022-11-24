using DB_ChitAlka;
using DB_ChitAlka.Interfases;
using Microsoft.EntityFrameworkCore;
using MVC_ChitAlka.Intrfaces;
using System.Linq;

namespace MVC_ChitAlka.Servises
{
    public class DeleteBookService : IDeleteBookService
    {
        private readonly IDbContext _dbContext;

        public DeleteBookService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void BookDelete(int bookDelete)
        {
            var BookDB = _dbContext.Books   
                    .Include(u => u.Section)
                    .ToList();
            //var book=BookDB(bookDelete);

            var book = _dbContext.Books
                    .First(p => p.Id == bookDelete);
            var author=book.Author;
           
            //var autor=_dbContext.Books.First(e=>e.Author.Book== book);

            //var countSection = _dbContext.Sections.ToList();
            //foreach (var section in book.Section)
            //{
            //    if (section.BookId == bookDelete)
            //    {
            //        _dbContext.Sections.Remove(section);

            //    }
            //}
           //var sections=_dbContext.Sections.First(p=>p.Id==book.Id);
            _dbContext.Books.Remove(book);

            _dbContext.SaveChanges();
        }
    }
}
