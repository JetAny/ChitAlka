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

        public async Task<int> GetUserBook(User currentUser, int bookId, bool fl)
        {
            //User currentUser = await _userManager.GetUserAsync(User)
            //var user = await _userManager.FindByIdAsync(currentUser.Id);
            var currentUserLib = currentUser.Userlibrary;

            if (currentUserLib.Count == 0)
            {
                currentUserLib = new List<Userlibrary>();
            }
            else
            {
                int section;
                var userLib1 = new Userlibrary();
                var book1 = _dbContext.Books
                         .Include(u => u.Sections)
                         .First(p => p.Id == bookId);
                var st = book1.Sections[0];

                foreach (var lib in currentUserLib)
                {
                    if (lib.Book == book1)
                    {
                        if (fl == true)
                        {
                            var sm = st.Id - 1;
                            lib.CurentSectionId = lib.CurentSectionId + 1;
                            section = lib.CurentSectionId;
                            if (section > book1.Sections.Count + sm)
                            {
                                lib.CurentSectionId = st.Id;
                                section = -1;
                            }
                        }
                        else
                        {
                            var sm = st.Id - 1;
                            lib.CurentSectionId = lib.CurentSectionId - 1;
                            section = lib.CurentSectionId;
                            if (section == sm)
                            {
                                section = -1;
                            }
                        }
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
                        return section;
                    }
                }
            }

            var userLib = new Userlibrary();
            var book = _dbContext.Books
                     .Include(u => u.Sections)
                     .First(p => p.Id == bookId);
            var sec = book.Sections[0];
            var section1 = sec.Id;
            userLib.CurentSectionId = sec.Id;
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

            return section1;
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

