using Microsoft.AspNetCore.Mvc;

namespace Avans.UI.Areas.Register.Controllers
{
    public class RegisterController : Controller
    {
        [Area("Register")]

        public IActionResult Index()
        {
            return View();
        }
    }
}
