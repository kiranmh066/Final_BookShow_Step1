using Microsoft.AspNetCore.Mvc;

namespace MovieCoreMvcUI.Controllers
{
    public class UserHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
