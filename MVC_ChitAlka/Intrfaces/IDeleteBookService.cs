

using DB_ChitAlka.Areas.Identity.Data;

namespace MVC_ChitAlka.Intrfaces
{
    public interface IDeleteBookService
    {
        void BookDelete(int bookDelete);
       void UserBookDelete(User user,int bookDelete);
    }
}
