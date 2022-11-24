using Microsoft.AspNetCore.Mvc;

namespace MVC_ChitAlka.Controllers
{
    public class UserController : Controller
    {
        public IActionResult InputLogin()
        {
            return View();
        }
    }
}
