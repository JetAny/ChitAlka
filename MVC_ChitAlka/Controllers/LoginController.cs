using Microsoft.AspNetCore.Mvc;
using MVC_ChitAlka.Intrfaces;
using System.Dynamic;

namespace MVC_ChitAlka.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAddBookService _getAddBookService;
        private readonly ISearchBookService _searchBookService;
        private readonly IGetAllBooksService _getAllBooksService;
        private readonly IGetBookService _getBookService;
        private readonly IGetSectionBookService _getSectionBookService;
        public LoginController(
            IAddBookService addBookService,
            ISearchBookService searchBookService,
            IGetAllBooksService getAllBooksService,
            IGetBookService getBookService,
            IGetSectionBookService getSectionBookService
            )
        {
            _getAddBookService = addBookService;
            _searchBookService = searchBookService;
            _getAllBooksService = getAllBooksService;
            _getBookService = getBookService;
            _getSectionBookService = getSectionBookService;
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
        public  ActionResult AddBook(IFormFile file)
        {
            dynamic mymodel = new ExpandoObject();
            mymodel.Book = _getAddBookService.AddBook(file);
            return View("AddBook", mymodel);
        }

        [HttpGet]
        public  IActionResult Search(string valueBook) //поиск книги
        {
            dynamic mymodel = new ExpandoObject();
            var searchBook = _searchBookService.SearchBook(valueBook);
            if (searchBook != null)
            {
                mymodel.BookV = searchBook.AuthorModel.BookModel;
                mymodel.Error = true;
            }
            else { mymodel.Error = false; }
            return View(mymodel);
        }
        [Route("Login/Library")]
        [HttpGet]
        public IActionResult Library()
        {
            dynamic mymodel = new ExpandoObject();
            mymodel.Author = _getAllBooksService.GetAll();
            return View("Library", mymodel);

        }

        [Route("Login/BookPage")]
        [HttpGet]
        public IActionResult BookPage(int BookId)
        {
            dynamic mymodel = new ExpandoObject();
            mymodel.Book = _getBookService.GetBook(BookId);

            return View("BookPage", mymodel);
        }

        [Route("Login/ReadBook")]
        [HttpGet]
        public IActionResult ReadBook(int bookId, int secId)
        {
            var section = _getSectionBookService.GetSection(bookId, secId);
            if(section.Count == 0)
            {
                return LocalRedirect("~/Login/BookPage/?BookId="+bookId);
            }
            return View("ReadBook", section);
        }
    }
}
