using Microsoft.AspNetCore.Mvc;

namespace MovieCoreMvcUI.Controllers
{
    public class TrailerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
