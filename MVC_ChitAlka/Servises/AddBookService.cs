using DB_ChitAlka;
using DB_ChitAlka.Interfases;
using MVC_ChitAlka.Intrfaces;

namespace MVC_ChitAlka.Servises
{
    public class AddBookService : IAddBookService
    {
        private readonly IDbContext _dbContext;

        public AddBookService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private Book book;

        public async Task<Book> AddBook(IFormFile file)
        {
            using (Stream uploadFileStream = file.OpenReadStream())
            {

                string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".fb2"; // имя файла в Темп

                //string fileName = "D:/Books/BooksFb2/tst/test3.fb2";
                //var a = uploadFileStream.ReadByte();

                await using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    uploadFileStream.Seek(3, SeekOrigin.Begin);
                    uploadFileStream.CopyTo(fileStream);
                }
                var pars = new Fb2Parser(fileName);
                book = pars.Parse();
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
            return book;
        }
    }
}
