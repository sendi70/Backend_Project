using ClientApi.Models;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Diagnostics;

namespace ClientApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        int i;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            i = 0;
        }

        public IActionResult Index()
        {
            if(i == 0)
            {
                i++;
            Response.Cookies.Append("X-Access-Token", "", new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict, });
            Response.Cookies.Append("X-Username", "", new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
            Response.Cookies.Append("X-Refresh-Token", "", new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
            Response.Cookies.Append("IsAuthenticated", "No", new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict, Expires = DateTime.Now.AddMinutes(1) });
            return View();
            }
            else
            {

            return View();
            }
        }

        public IActionResult Privacy()
        {
            var isAuthenticated = Request.Cookies["IsAuthenticated"];
            if (isAuthenticated == "Yes")
            {
                return View();
            }
            else { 
                return RedirectToAction("Index");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}