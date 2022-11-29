using DB_ChitAlka;
using DB_ChitAlka.Areas.Identity.Data;

namespace MVC_ChitAlka.Intrfaces
{
    public interface IUserLibraryService
    {
         Task<int> GetUserBook(User currentUser,int bookId,bool fl);
        public List<AuthorModel> GetUserLibrary(User user);
    }
}
