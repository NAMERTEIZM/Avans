using Microsoft.AspNetCore.Mvc;

namespace Avans.UI.Areas.Login.Controllers
{
    public class LoginController : Controller
    {
        [Area("Login")]

        public IActionResult Index()
        {
            return View();
        }
    }
}
