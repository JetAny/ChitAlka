using DB_ChitAlka;
using DB_ChitAlka.Areas.Identity.Data;
using MVC_ChitAlka.Intrfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using System.Security.Claims;

namespace MVC_ChitAlka.Controllers
{
    public class LoginController : Controller
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
        public LoginController(
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
        [HttpGet]
        public IActionResult AddBook()
        {
            return View();
        }
        [Route("Login/AddBook")]
        [HttpPost]
        public async Task<ActionResult> AddBook(IFormFile file)
        {
            dynamic mymodel = new ExpandoObject();
            mymodel.Book = _getAddBookService.AddBook(file);
            return View("AddBook", mymodel);
        }

        [Authorize]
        [Route("Login/Search")]
        [HttpGet]
        public async Task<IActionResult> Search(string valueBook) //поиск книги
        {
            dynamic mymodel = new ExpandoObject();
            var searchBook = _searchBookService.SearchBook(valueBook);
            if (searchBook != null)
            {
                mymodel.BookV = searchBook.AuthorModel.BookModel;
                mymodel.Error = true;
            } 
            else { mymodel.Error = false; }
            return View("Search",mymodel);
        }

        [Route("Login/Library")]
        [HttpGet]
        public IActionResult Library()
        {
            dynamic mymodel = new ExpandoObject();
            mymodel.Author = _getAllBooksService.GetAll();
            return View("Library", mymodel);

        }

        [Authorize]
        [Route("Login/BookPage")]
        [HttpGet]
        public IActionResult BookPage(int BookId)
        {

            dynamic mymodel = new ExpandoObject();
            mymodel.Book = _getBookService.GetBook(BookId);

            return View("BookPage", mymodel);
        }


        [Authorize]
        [Route("Login/ReadBook")]
        public async Task<IActionResult> CreateUserLib(int bookId, bool fl)
        {
            User currentUserLazi = await GetCurrentUserAsync();
            
            User currentUser = _dbContext.Users.Include(a=>a.Userlibrary).FirstOrDefault(x => x.Id== currentUserLazi.Id);
          
            if(currentUser.Userlibrary.Count == 0  & fl == false)
            {
                return Redirect("~/Login/BookPage?BookId=" + bookId);
            }
            var bookSection = _userLibraryService.GetUserBook(currentUser,bookId,fl);
            if (await bookSection == -1)
            {
                return Redirect("~/Login/BookPage?BookId=" + bookId);
            }
            var section = _getSectionBookService.GetSection(bookId, await bookSection);
            return View("ReadBook", section);
        }



        [Authorize]
        [Route("Login/BookDelite")]
        [HttpGet]
        public IActionResult BookDelite(int BookId)
        {
            _deleteBookService.BookDelete(BookId);
            dynamic mymodel = new ExpandoObject();
            mymodel.Author = _getAllBooksService.GetAll();
            return View("Library", mymodel);

        }
        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);



        [Route("Login/Proub")]
        public async Task<IActionResult> Prorub(int bookId)
        {

            
            User currentUser = await GetCurrentUserAsync();

            var user = await _userManager.FindByIdAsync(currentUser.Id);
            var currentUserLib = currentUser.Userlibrary;

            


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
    }
}
