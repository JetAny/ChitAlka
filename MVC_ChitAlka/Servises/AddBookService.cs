using DB_ChitAlka;
using DB_ChitAlka.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using MVC_ChitAlka.Intrfaces;

namespace MVC_ChitAlka.Servises
{
    public class AddBookService : IAddBookService
    {
        private readonly MyDbContext _dbContext;

        public AddBookService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private Book book;

        public async Task<Book> AddBook(IFormFile file)
        {
            using (Stream uploadFileStream = file.OpenReadStream())
            {

                string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".fb2"; // имя файла в Темп

                await using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    uploadFileStream.Seek(3, SeekOrigin.Begin);
                    uploadFileStream.CopyTo(fileStream);
                }
                var pars = new Fb2Parser(fileName);
                book = pars.Parse();


                var allBook = _dbContext.Books.Include(x => x.Author).ToList();
                foreach (var bookLib in allBook)
                {
                    if (bookLib.BookTitle == book.BookTitle || bookLib.Author == book.Author)
                    {

                        _dbContext.Books.Remove(book);
                    }
                    else
                    {
                        try
                        {
                            _dbContext.Books.Add(book);
                            _dbContext.SaveChanges();
                        }
                        catch
                        {
                            _dbContext.Books.Remove(book);
                            throw;
                        }
                    }
                }
            }
            return book;
        }
    }
}
