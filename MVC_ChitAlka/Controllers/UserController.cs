using DB_ChitAlka;
using DB_ChitAlka.Areas.Identity.Data;
using MVC_ChitAlka.Servises;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MVC_ChitAlka.Controllers
{
    public class UserController : Controller
    {
        private readonly MyDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        
        public UserController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            MyDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
          
        }


        [Route("User/CreateUserLib")]
        public async Task<IActionResult> CreateUserLib(int bookId)
        {


            User currentUser = await GetCurrentUserAsync();
            var user = await _userManager.FindByIdAsync(currentUser.Id);
            var currentUserLib = user.Userlibrary;
            
            var name = currentUser.NickName;


            if (currentUserLib == null)
            {
                currentUserLib = new List<Userlibrary>();

            }
            else
            {
                var bookRead = _dbContext.Books
                                      .Include(u => u.Sections)
                                       .First(p => p.Id == bookId);

                foreach (var library in currentUserLib)
                {
                    if (library.Book.Id == bookId)
                    {

                        Section caurrentSection = _dbContext.Sections
                            .Include(x => x.Id == library.CurentSectionId)
                            .First();

                        return RedirectToActionPermanent("ReadBook", "Login", new
                        {
                            BookId = bookId,
                            secId = caurrentSection
                        });
                    }
                }
            }
            var userLib = new Userlibrary();
            var book = _dbContext.Books
                     .Include(u => u.Sections)
                     .First(p => p.Id == bookId);
            var sec = book.Sections[0];

            userLib.Book = book;
            currentUserLib.Add(userLib);
            currentUser.Userlibrary = currentUserLib;

            _dbContext.Users.Update(currentUser);
            _dbContext.SaveChanges();

            return RedirectToActionPermanent("ReadBook", "Login", new { BookId = bookId, secId = sec });
        }

        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

    }
}
