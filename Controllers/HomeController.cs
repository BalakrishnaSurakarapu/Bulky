using System.Diagnostics;
using BulkyWeb.Models;
using BulkyWeb.Service;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TokenService _tokenService;
        private readonly UserStoreService _userStore;


        public HomeController(ILogger<HomeController> logger, TokenService tokenService, UserStoreService userStore)
        {
            _logger = logger;
            _tokenService = tokenService;
            _userStore = userStore;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Register model)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(model.Role))
            {
                ViewBag.Error = "Invalid input.";
                return View(model);
            }

            _userStore.AddUser(model);
            TempData["Message"] = "Registration successful. Please log in.";
            return RedirectToAction("Login");
        }


        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.Users = _userStore.GetUsersByRole("User");
            ViewBag.Admins = _userStore.GetUsersByRole("Admin");
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login model)
        {
            var user = _userStore.GetUser(model.Username, model.Password);

            if (user == null)
            {
                ViewBag.Error = "Invalid credentials.";
                ViewBag.Users = _userStore.GetUsersByRole("User");
                ViewBag.Admins = _userStore.GetUsersByRole("Admin");
                return View(model);
            }

            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
                return RedirectToAction("Login");

            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            ViewBag.Token = HttpContext.Session.GetString("Token");

            return View();
        }
        [HttpGet]
        public IActionResult Save()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Save(Apiuser model, IFormFile FileUpload)
        {
            if (!ModelState.IsValid)
                return View("Index", model);

            // Save uploaded file
            if (FileUpload != null && FileUpload.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var originalFileName = Path.GetFileName(FileUpload.FileName);
                var uniqueFileName = Guid.NewGuid() + "_" + originalFileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    FileUpload.CopyTo(stream);
                }

                TempData["FileName"] = uniqueFileName;
            }
            var user = new Apiuser
            {
                Name = model.Name,
                Email = model.Email,
                Mobile = model.Mobile,
                Dob = model.Dob
            };


            // Store model values in TempData (serialize it)
            TempData["UserData"] = System.Text.Json.JsonSerializer.Serialize(user);

            return RedirectToAction("Preview");
        }

        [HttpGet]
        public IActionResult Preview()
        {
            if (TempData["UserData"] is string jsonData)
            {
                var model = System.Text.Json.JsonSerializer.Deserialize<Apiuser>(jsonData);
                ViewBag.FileName = TempData["FileName"]?.ToString();
                return View(model);
            }

            // If TempData is missing, redirect back to form
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
