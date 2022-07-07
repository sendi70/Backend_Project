using ClientApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http.Json;

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
            //string data = JsonConvert.SerializeObject(model);
            //var buffer= Encoding.UTF8.GetBytes(data);
            //var byteContent = new ByteArrayContent(buffer);
            //byteContent.Headers.ContentType = new MediaTypeHeaderValue("aplication/json");
            //StringContent content = new StringContent(data, Encoding.UTF8, "aplication/json");
            //HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/register", byteContent).Result;
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
            return View();
        }
    }
}
