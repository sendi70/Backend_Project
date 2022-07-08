using ClientApi.Models;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Diagnostics;

namespace ClientApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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