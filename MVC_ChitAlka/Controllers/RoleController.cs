using DB_ChitAlka.Areas.Identity.Data;
using DB_ChitAlka.Entitys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;


namespace MVC_ChitAlka.Controllers
{
    public class RoleController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<User> _userManager;
        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }


        [Route("Role/Index")]
        public IActionResult Index()  //список ролей
        {

            return View(_roleManager.Roles);
        }


        [HttpGet]
        [Authorize(Policy  = "RequireAdministratorRole")]
        public IActionResult Create() => View();

        [HttpPost]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Create([Required] string name)   //создание новой роли (Name) в списке ролей
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    return View();
            }
            return View(name);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        [Route("/Role/DeleteRole")]

        public async Task<IActionResult> Delete(string delRole)  //удаление роли по id, из списка ролей
        {
            IdentityRole role = await _roleManager.FindByIdAsync(delRole);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                {

                }
                Errors(result);
            }
            else
                ModelState.AddModelError("", "No role found");
            return View("Index", _roleManager.Roles);
        }

        [HttpGet]
        [Authorize(Policy = "RequireAdministratorRole")]
        [Route("/UserList")]
        public async Task<IActionResult> UserList(RoleEdit listRole)  //список пользователей
        {
            var dict = new Dictionary<User, IList<string>>();
            dynamic mymodel = new ExpandoObject();
            mymodel.AllRoles = _roleManager.Roles.ToList();

            foreach (User userList in _userManager.Users.ToList())
            {
                var userRole = await _userManager.GetRolesAsync(userList);

                dict.Add(userList, userRole);
            }
            mymodel.Users = dict;
            return View(mymodel);
        }


        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        [Route("/AddRoleToUser")]
        public async Task<IActionResult> AddRoleToUser(string userId, string roleId) //добавление роли user
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role != null)
            {
                var user = await _userManager.FindByIdAsync(userId);
                var result = await _userManager.AddToRoleAsync(user, role.Name);//добавление роли user
                if (!result.Succeeded)
                {
                    Errors(result);
                }
            }
            return RedirectToAction("UserList");

        }

        //[HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        [Route("/Role/DeleteRoleUser")]

        public async Task<IActionResult> Delete(string delRole, string userId)  //удаление роли по id user, из списка ролей
        {
            var user = await _userManager.FindByIdAsync(userId);
            //IdentityRole role = await _roleManager.FindByNameAsync(delRole);
            if (delRole != null)
            {
                var result = await _userManager.RemoveFromRoleAsync(user, delRole);
                if (!result.Succeeded)
                {
                    Errors(result);
                }
            }
            return RedirectToAction("UserList");
        }


        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}

