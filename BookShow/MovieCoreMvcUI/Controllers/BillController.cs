using BookShowEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieCoreMvcUI.Controllers
{
    public class BillController : Controller
    {
        private IConfiguration _configuration;

        public BillController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<BookTicket> bookTicketresult = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "BookTicket/GetBookTickets";
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        bookTicketresult = JsonConvert.DeserializeObject<IEnumerable<BookTicket>>(result);
                    }
                }
            }
            return View(bookTicketresult);
        }        
    }
}
