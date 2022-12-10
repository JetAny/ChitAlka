using DB_ChitAlka.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_ChitAlka.Intrfaces;
using System.Dynamic;

namespace MVC_ChitAlka.Controllers
{

    public class LibraryController : Controller
    {
        private readonly MyDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly IAddBookService _getAddBookService;
        private readonly ISearchBookService _searchBookService;
        private readonly IGetAllBooksService _getAllBooksService;
        private readonly IGetBookService _getBookService;
        private readonly IGetSectionBookService _getSectionBookService;
        private readonly IDeleteBookService _deleteBookService;
        private readonly IUserLibraryService _userLibraryService;

        public LibraryController(
            MyDbContext dbContext,
            UserManager<User> userManager,
            IAddBookService addBookService,
            ISearchBookService searchBookService,
            IGetAllBooksService getAllBooksService,
            IGetBookService getBookService,
            IGetSectionBookService getSectionBookService,
            IDeleteBookService deleteBookService,
            IUserLibraryService userLibraryService
            )
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _getAddBookService = addBookService;
            _searchBookService = searchBookService;
            _getAllBooksService = getAllBooksService;
            _getBookService = getBookService;
            _getSectionBookService = getSectionBookService;
            _deleteBookService = deleteBookService;
            _userLibraryService = userLibraryService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        [HttpGet]
        public IActionResult AddBook()
        {
            return View();
        }
        [Authorize]
        [Route("Library/AddBook")]
        [HttpPost]
        public async Task<ActionResult> AddBook(IFormFile file)
        {
            dynamic mymodel = new ExpandoObject();
            mymodel.Book = _getAddBookService.AddBook(file);
            return View("AddBook", mymodel);
        }

        [Authorize]
        [Route("Library/Search")]
        [HttpGet]
        public async Task<IActionResult> Search(string valueBook)
        {
            dynamic mymodel = new ExpandoObject();
            var searchBook = _searchBookService.SearchBook(valueBook);
            if (searchBook != null)
            {
                mymodel.BookV = searchBook.AuthorModel.BookModel;
                mymodel.Error = true;
            }
            else { mymodel.Error = false; }
            return View("Search", mymodel);
        }

        [Authorize]
        [Route("Library")]
        [HttpGet]
        public IActionResult Library()
        {
            dynamic mymodel = new ExpandoObject();
            mymodel.Author = _getAllBooksService.GetAll();
            return View("Library", mymodel);
        }

        [Authorize]
        [Route("Library/BookPage")]
        [HttpGet]
        public IActionResult BookPage(int BookId)
        {
            dynamic mymodel = new ExpandoObject();
            mymodel.Book = _getBookService.GetBook(BookId);

            return View("BookPage", mymodel);
        }

        [Authorize]
        [Route("Library/ReadBook")]
        public async Task<IActionResult> CreateUserLib(int bookId, int fl)
        {
            User currentUserLazi = await GetCurrentUserAsync();

            User currentUser = _dbContext.Users.Include(a => a.Userlibrary).FirstOrDefault(x => x.Id == currentUserLazi.Id);
            var bookSection = _userLibraryService.GetUserBook(currentUser, bookId, fl);

            if (bookSection == null)
            {
                return Redirect("~/Library/BookPage?BookId=" + bookId);
            }

            dynamic mymodel = new ExpandoObject();
            mymodel.Section = bookSection;
            mymodel.BookId = bookId;

            return View("ReadBook", mymodel);
         }

        [Authorize]
        [Route("Library/BookDelete")]
        [HttpGet]
        public IActionResult BookDelete(int BookId)
        {
            _deleteBookService.BookDelete(BookId);
            dynamic mymodel = new ExpandoObject();
            mymodel.Author = _getAllBooksService.GetAll();
            return View("Library", mymodel);

        }

        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
