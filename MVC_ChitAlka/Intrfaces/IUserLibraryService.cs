using DB_ChitAlka;
using DB_ChitAlka.Areas.Identity.Data;

namespace MVC_ChitAlka.Intrfaces
{
    public interface IUserLibraryService
    {
         Section GetUserBook(User currentUser,int bookId,int fl);
        public List<AuthorModel> GetUserLibrary(User user);
    }
}
