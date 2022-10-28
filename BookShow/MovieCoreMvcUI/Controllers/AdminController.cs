using BookShowEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MovieCoreMvcUI.Controllers
{
    public class AdminController : Controller
    {
        private IConfiguration _configuration;

        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string Email,string Password)
        {

            if(Email=="kiran" && Password=="123")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.status = "Error";
                ViewBag.message = "Wrong credentials!";
            }
            return View();
        }
    }
}
