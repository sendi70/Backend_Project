using ClientApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Formatting;
using ClientApi.Data.Services;

namespace ClientApi.Controllers
{
    public class UserController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:5001/api/Authentication");
        HttpClient client;

        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            _service = service;
        }

        public IActionResult Index()
        {

            return View();
        }
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(UserDto model)
        {
            var user = new UserDto { 
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
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Login model)
        {
            var loginCredentials = new UserDto { 
                Username = model.Username,
                Password = model.Password,
            };
            HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/login", loginCredentials).Result;
            if (response.IsSuccessStatusCode)
            {
                var context = response.Content.ReadAsAsync<AuthenticatedResponse>();
                Response.Cookies.Append("X-Access-Token", context.Result.AccessToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict, });
                Response.Cookies.Append("X-Username", loginCredentials.Username, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                Response.Cookies.Append("X-Refresh-Token", context.Result.RefreshToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                Response.Cookies.Append("IsAuthenticated", "Yes", new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict, Expires = DateTime.Now.AddMinutes(15) });
                var user = _service.GetByNameAsync(loginCredentials.Username);
                if (user.Result != null)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction(nameof(CreateUserProfil));
            }
            ViewBag.ErrorMessage = response.StatusCode.ToString();
            return View("Bad authorization string");

        }
        public IActionResult CreateUserProfil()
        {
            return View();
        } 
        [HttpPost]
        public IActionResult CreateUserProfil([Bind("Email,Age,City")] User user)
        {
            user.Name = Request.Cookies["X-Username"];
            _service.AddAsync(user);
            return View(nameof(Index));

        }
        public async Task<IActionResult> LogoutAsync()
        {
            var req = new
            {
                refreshToken = Request.Cookies["X-Refresh-Token"],
            };
            client.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", Request.Cookies["X-Access-Token"]);
            string reqString = JsonConvert.SerializeObject(req);
            var httpMessage = new HttpRequestMessage(HttpMethod.Delete, baseAddress + "/logout")
            {
                Content = new StringContent(reqString, Encoding.UTF8, "application/json")
            };
            var response = await client.SendAsync(httpMessage);
           // HttpResponseMessage response = client.DeleteAsJsonAsync(baseAddress + "/logout", req).Result;
            if (response.IsSuccessStatusCode)
            {
                Response.Cookies.Append("X-Access-Token", "", new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict, });
                Response.Cookies.Append("X-Username", "", new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                Response.Cookies.Append("X-Refresh-Token", "", new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                Response.Cookies.Append("IsAuthenticated", "No", new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict, Expires = DateTime.Now.AddMinutes(1) });
                return RedirectToAction(nameof(Index),"Home");
            }
            return View("Bad request");
        }
    }
}
