using DB_ChitAlka;
using DB_ChitAlka.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC_ChitAlka.Intrfaces;

namespace MVC_ChitAlka.Servises
{
    public class UserLibraryService : IUserLibraryService
    {
        private readonly MyDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserLibraryService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            MyDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        public Section GetUserBook(User currentUser, int bookId, int fl)
        {
            var currentUserLib = currentUser.Userlibrary;
            Section currentSection = new Section();
            if (currentUserLib.Count == 0)
            {
                currentUserLib = new List<Userlibrary>();
            }
            else
            {
                var userLib1 = new Userlibrary();
                var book1 = _dbContext.Books
                         .Include(u => u.Sections)
                         .First(p => p.Id == bookId);

                var st = book1.Sections[0];
                var d = book1.Sections.Count();
                if ((fl < st.Id) | fl == (d + st.Id))
                {
                    if (fl != -1)
                    {
                        currentSection = null;
                        return currentSection;
                    }
                   
                }
                foreach (var lib in currentUserLib)
                {
                    if (lib.Book == book1)
                    {
                        if (fl != -1)
                        {
                            var sm = fl - st.Id;
                            currentSection = book1.Sections[sm];

                        }
                        else
                        {
                            var sec = lib.CurentSectionId;
                            currentSection = book1.Sections[sec - st.Id];

                        }

                        lib.CurentSectionId = currentSection.Id;

                        try
                        {
                            _dbContext.Userlibraries.Update(lib);
                            _dbContext.SaveChanges();
                        }
                        catch
                        {
                            _dbContext.Users.Remove(currentUser);
                            throw;
                        }

                        return currentSection;
                    }
                }
            }

            var userLib = new Userlibrary();
            var book = _dbContext.Books
                     .Include(u => u.Sections)
                     .First(p => p.Id == bookId);

            currentSection = book.Sections[0];

            userLib.CurentSectionId = currentSection.Id;
            userLib.Book = book;


            currentUserLib.Add(userLib);
            currentUser.Userlibrary = currentUserLib;

            try
            {

                _dbContext.Users.Update(currentUser);
                _dbContext.SaveChanges();
            }
            catch
            {
                _dbContext.Users.Remove(currentUser);
                throw;
            }

            return currentSection;
        }

        public List<AuthorModel> GetUserLibrary(User user)
        {
            var allAuthorDB = new List<Author>();

            var currentUser = _dbContext.Users
                .Include(a => a.Userlibrary)
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
                        var author = book.Author;
                        allAuthorDB.Add(author);
                    }
                }
            }
            ServiceMap _serviceMap = new ServiceMap();
            var getAllAuthors = _serviceMap.MapDbModel(allAuthorDB);

            return getAllAuthors;
        }
    }
}

