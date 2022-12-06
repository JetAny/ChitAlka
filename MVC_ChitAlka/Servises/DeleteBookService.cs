using DB_ChitAlka.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using MVC_ChitAlka.Intrfaces;

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


            //var author = _dbContext.Authors
            //                    .Include(u => u.Book)
            //                    .First(p => p.Id == bookDelete);

            var book = _dbContext.Books.Include(a => a.Author)
                     .Include(u => u.Sections)
                     .First(p => p.Id == bookDelete);

            //var usersDeleteBook = _dbContext.Authors.Include(x => x.Book).First(s => s.Book == book);
            //var librarys = _dbContext.Userlibraries.Include(d => d.Book).First(p => p.Book == book);

            var dsd = _dbContext.Userlibraries.Include(d => d.Book).ToList();

            foreach (var userlibrary in dsd)
            {
                if (userlibrary.Book == book)
                {
                    _dbContext.Userlibraries.Remove(userlibrary);
                }

            }

            foreach (var section in book.Sections)
            {
                _dbContext.Sections.Remove(section);

            }
            _dbContext.Authors.Remove(book.Author);
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }

        public void UserBookDelete(User user, int bookDelete)
        {

            var currentUser = _dbContext
                .Users.Include(a => a.Userlibrary)
                .FirstOrDefault(x => x.Id == user.Id);

            var currentUserLib = currentUser.Userlibrary;

            var allUserLib = _dbContext.Userlibraries
                .Include(a => a.Book)
                .Include(s => s.Book.Author).ToList();

            foreach (var curlib in currentUserLib)
            {
                foreach (var allLib in allUserLib)
                {
                    if (curlib.Id == allLib.Id)
                    {
                        var book = allLib.Book;
                        _dbContext.Userlibraries.Remove(curlib);
                        _dbContext.SaveChanges();
                        return;
                    }
                }
            }
        }
    }
}
