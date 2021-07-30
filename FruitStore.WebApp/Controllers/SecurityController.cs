using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using FruitStore.WebApp.Extensions;
using FruitStore.DataAccess.Services;
namespace FruitStore.WebApp.Controllers
{
    public class SecurityController : Controller
    {
        private IUser _userService;
        private IUserSession _session;

        public SecurityController(IUser userService, IUserSession session)
        {
            this._userService = userService;
            this._session = session;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            if (_session.User != null)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public IActionResult Login(string userName, string password)
        {
            var user = _userService.Login(userName, password);
            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı adınızı veya şifrenizi kontrol ediniz.");
                return View();
            }

            _session.User = user;
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Set<Entity.User>(null);
            return RedirectToAction("Login");
        }
    }
}