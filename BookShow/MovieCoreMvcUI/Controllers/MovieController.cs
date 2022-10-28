using BookShowEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MovieCoreMvcUI.Controllers
{
    public class MovieController : Controller
    {
        private IConfiguration _configuration;

        public MovieController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Movie> movieresult = null;
            using (HttpClient client = new HttpClient())
            {
                //StringContent content = new StringContent(JsonConvert.SerializeObject(movie), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Movie/GetMovies";

                /*var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJNb3ZpZUFwcENvcmVBUElBdXRoQWNjZXNzVG9rZW4iLCJqdGkiOiI2ZGYzZDdlYS03OTUyLTRlZmMtOGRkOC1iZDhjYWU2ZDc4ZDciLCJpYXQiOiIxMC8xNy8yMDIyIDExOjMwOjAxIEFNIiwiSWQiOiI2IiwiTmFtZSI6InN0cmluZyIsIkVtYWlsIjoic3RyaW5nIiwiUGFzc3dvcmQiOiJzdHJpbmciLCJleHAiOjE2NjYwOTI2MDEsImlzcyI6Ik1vdmllQXBwQ29yZUFQSVNlcnZlciIsImF1ZCI6Ik1vdmllQXBwQ29yZUFQSUNsaWVudCJ9.Dp6_iSg-pTb4a7v76H46mSnpxlC4dSOY7RhtrULveKI\r\n";
                string endPoint = _configuration["WebApiBaseUrl"] + "Movie/GetMovies";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);*/

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        movieresult = JsonConvert.DeserializeObject<IEnumerable<Movie>>(result);
                    }                    
                }
            }
            return View(movieresult);
        }
        public IActionResult MovieEntry()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> MovieEntry(Movie movie)
        {
            ViewBag.status = "";
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(movie),Encoding.UTF8,"application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Movie/AddMovie";
                using (var response = await client.PostAsync(endPoint, content))
                {
                    if(response.StatusCode==System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "Movie deatils Saved Successfully";
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong Entries....!!!  :)";
                    }
                }
            }

            return View();
        }

        public async Task<IActionResult> EditMovie(int movieID)
        {

            Movie movie = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Movie/GetMovieById?movieId=" + movieID;
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        movie = JsonConvert.DeserializeObject<Movie>(result);
                    }
                }
            }

            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> EditMovie(Movie movie)
        {
            ViewBag.status = "";
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(movie), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Movie/UpdateMovie";
                using (var response = await client.PutAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "Movie deatils Saved Successfully";
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong Entries....!!!  :)";
                    }
                }
            }
            return View();
        }

        /*List<SelectListItem> language = new List<SelectListItem>();*/

        public async Task<IActionResult> DeleteMovie(int MovieId)
        {
            Movie movie = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Movie/GetMovieById?movieId=" + MovieId;
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        movie = JsonConvert.DeserializeObject<Movie>(result);
                    }
                }
            }

            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMovie(Movie movie)
        {
            ViewBag.status = "";
            using (HttpClient client = new HttpClient())
            {

                string endpoint = _configuration["WebApiBaseUrl"] + "Movie/DeleteMovie?moviId=" + movie.Id;
                using (var response = await client.DeleteAsync(endpoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "Movie detaisl deleted succesfully";
                    }
                    else
                    {
                        ViewBag.staus = "Error";
                        ViewBag.message = "Wrong Entries";
                    }
                }
            }
            return View();
        }


        public async Task<IActionResult> ShowMovies()
        {
            IEnumerable<Movie> movieresult = null;
            using (HttpClient client = new HttpClient())
            {
                var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJNb3ZpZUFwcENvcmVBUElBdXRoQWNjZXNzVG9rZW4iLCJqdGkiOiI2ZGYzZDdlYS03OTUyLTRlZmMtOGRkOC1iZDhjYWU2ZDc4ZDciLCJpYXQiOiIxMC8xNy8yMDIyIDExOjMwOjAxIEFNIiwiSWQiOiI2IiwiTmFtZSI6InN0cmluZyIsIkVtYWlsIjoic3RyaW5nIiwiUGFzc3dvcmQiOiJzdHJpbmciLCJleHAiOjE2NjYwOTI2MDEsImlzcyI6Ik1vdmllQXBwQ29yZUFQSVNlcnZlciIsImF1ZCI6Ik1vdmllQXBwQ29yZUFQSUNsaWVudCJ9.Dp6_iSg-pTb4a7v76H46mSnpxlC4dSOY7RhtrULveKI\r\n";
                string endPoint = _configuration["WebApiBaseUrl"] + "Movie/GetMovies";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        movieresult = JsonConvert.DeserializeObject<IEnumerable<Movie>>(result);
                    }
                }
            }
            return View(movieresult);
        }
    }
}
