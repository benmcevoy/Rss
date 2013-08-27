using System.Configuration;
using System.Web.Mvc;
using Rss.Server.Services;

namespace Rss.Server.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly CookieService _cookieService;

        public LoginController(CookieService cookieService)
        {
            _cookieService = cookieService;
        }


        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName, string password)
        {
            if (userName.ToLowerInvariant() == "ben.mcevoy@gmail.com" &&
                password == ConfigurationManager.AppSettings["rss.password"])
            {
                _cookieService.Create(HttpContext);

                return RedirectToAction("Index", "Home");    
            }

            _cookieService.Remove(HttpContext);
            return RedirectToAction("Index");    
        }
    }
}
