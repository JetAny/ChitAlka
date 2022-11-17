using DB_ChitAlka;

namespace MVC_ChitAlka.Intrfaces
{
    public interface IAddBookService
    {

       Task<Book> AddBook(IFormFile file);
    }
}
