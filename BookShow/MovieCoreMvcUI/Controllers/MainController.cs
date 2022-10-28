using BookShowEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MovieCoreMvcUI.Controllers
{
    public class MainController : Controller
    {
        private IConfiguration _configuration;
        public MainController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }      

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User userInfo)
        {

            //TempData.Keep();
            ViewBag.status = "";

            IEnumerable<User> userresult = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "User/GetUsers";
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        userresult = JsonConvert.DeserializeObject<IEnumerable<User>>(result);
                    }
                }
            }
            int flag = 0;
            foreach(var item in userresult)
            {
                if(item.Email==userInfo.Email && item.Password==userInfo.Password)
                {
                    var k = TempData["userId"] = item.Id;
                    TempData.Keep();
                    flag = 1;
                  return RedirectToAction("Index", "UserHome");
                }

            }
            if (flag == 0)
            {
                ViewBag.status = "Error";
                ViewBag.message = "Wrong credentials!";
            }



            return View();
        }


        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(User userInfo)
        {
            ViewBag.status = "";
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(userInfo), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "User/Register";
                using (var response = await client.PostAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "Register successfully!";

                        System.Threading.Thread.Sleep(3000);
                        //Thread.Sleep(2000);
                        //Task.Delay(2000);
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            return RedirectToAction("Index", "Main");
                        }
                        
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong entries!";
                    }
                }
            }
            return View();
        }
        
    }
}
