using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BulkyWeb.Controllers
{
    public class CommentController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CommentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
            {

                return RedirectToAction("Login", "Home");
            }
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            ViewBag.Token = HttpContext.Session.GetString("Token");

            List<ApiComments> comments = new List<ApiComments>();

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://jsonplaceholder.typicode.com/comments");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                comments = JsonConvert.DeserializeObject<List<ApiComments>>(json);
            }

            return View(comments.Take(20).ToList()); // Return top 20 for display
        }

        public async Task<IActionResult> Details(int id)
        {
            ApiComments comment = null;

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://jsonplaceholder.typicode.com/comments/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                comment = JsonConvert.DeserializeObject<ApiComments>(json);
            }

            if (comment == null)
                return NotFound();

            return View(comment);
        }
    }
}
