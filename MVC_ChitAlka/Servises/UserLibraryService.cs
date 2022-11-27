using DB_ChitAlka;
using DB_ChitAlka.Areas.Identity.Data;
using MVC_ChitAlka.Intrfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

        //public ClaimsPrincipal User { get; private set; }

        public async Task<int> GetUserBook(User currentUser, int bookId, bool fl)
        {

            //User currentUser = await _userManager.GetUserAsync(User);


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
                            
                            var sm = st.Id-1;
                            lib.CurentSectionId = lib.CurentSectionId  + 1;
                            section = lib.CurentSectionId;
                            if (section > book1.Sections.Count+sm)
                            {
                                lib.CurentSectionId = st.Id;
                                section = -1;
                            }
                        }
                        else
                        {
                            
                            var sm = st.Id-1;
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

        //private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(User);
    }
}

