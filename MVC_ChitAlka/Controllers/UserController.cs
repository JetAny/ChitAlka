using DB_ChitAlka.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_ChitAlka.Intrfaces;
using System.Dynamic;

namespace MVC_ChitAlka.Controllers
{
    public class UserController : Controller
    {
        private readonly MyDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly IUserLibraryService _userLibraryService;
        private readonly IDeleteBookService _deleteBookService;

        public UserController(MyDbContext dbContext,
            UserManager<User> userManager,
            IUserLibraryService userLibraryService,
            IDeleteBookService deleteBookService
            )
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _userLibraryService = userLibraryService;
            _deleteBookService = deleteBookService;
        }


        [Authorize]
        [Route("User/UserLib")]
        [HttpGet]
        public async Task<IActionResult> UserLib()
        {
            User currentUser = await GetCurrentUserAsync();

            dynamic mymodel = new ExpandoObject();
            mymodel.Author = _userLibraryService.GetUserLibrary(currentUser);
            return View("UserLib", mymodel);
        }

        [Authorize]
        [Route("User/UserBookDelete")]
        [HttpGet]
        public async Task<IActionResult> UserBookDelete(int BookId)
        {
            User currentUser = await GetCurrentUserAsync();            
            _deleteBookService.UserBookDelete(currentUser, BookId);

            dynamic mymodel = new ExpandoObject();
            mymodel.Author = _userLibraryService.GetUserLibrary(currentUser);
            return View("UserLib", mymodel);

        }


        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
