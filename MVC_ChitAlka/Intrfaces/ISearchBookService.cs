using DB_ChitAlka;

namespace MVC_ChitAlka.Intrfaces
{
    public interface ISearchBookService
    {
        public BookModel SearchBook(string value);
    }
}
