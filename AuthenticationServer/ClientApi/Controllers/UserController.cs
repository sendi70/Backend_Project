using ClientApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Formatting;

namespace ClientApi.Controllers
{
    public class UserController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:5001/api/Authentication");
        HttpClient client;

        public UserController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(User model)
        {
            var user = new User { 
                Email = model.Email,
                Username = model.Username,
                Password = model.Password,
                ConfirmPassword = model.ConfirmPassword,
            };
            HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/register", user).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            ViewBag.ErrorMessage = response.StatusCode.ToString();
            return View();
        }public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Login model)
        {
            var loginCredentials = new User { 
                Username = model.Username,
                Password = model.Password,
            };
            HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/login", loginCredentials).Result;
            if (response.IsSuccessStatusCode)
            {
                var context = response.Content.ReadAsAsync<AuthenticatedResponse>();

                Response.Cookies.Append("X-Access-Token", context.Result.AccessToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                Response.Cookies.Append("X-Username", loginCredentials.Username, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                Response.Cookies.Append("X-Refresh-Token", context.Result.RefreshToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                return RedirectToAction("Index");
            }
            ViewBag.ErrorMessage = response.StatusCode.ToString();
            return View();
        }
    }
}
