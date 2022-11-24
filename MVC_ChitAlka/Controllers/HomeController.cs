using Microsoft.AspNetCore.Mvc;
using MVC_ChitAlka.Intrfaces;
using MVC_ChitAlka.Models;
using System.Diagnostics;
using System.Dynamic;

namespace MVC_ChitAlka.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGetAllBooksService _getAllBooksService;
        private readonly ISearchBookService _searchBookService;
        public HomeController(ILogger<HomeController> logger,
            IGetAllBooksService getAllBooksService,
            ISearchBookService searchBookService)
        {
            _logger = logger;
            _getAllBooksService = getAllBooksService;
            _searchBookService = searchBookService;
        }

        public IActionResult Index()
        {
            dynamic mymodel = new ExpandoObject();
            mymodel.isAuthor = true;
            return View(mymodel);
        }
        public IActionResult GetAllBooks()
        {
             dynamic mymodel = new ExpandoObject();
            mymodel.Author = _getAllBooksService.GetAll();
            return View("GetAllBooks", mymodel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}