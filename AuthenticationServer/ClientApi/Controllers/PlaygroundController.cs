using BackEndProject.Models;
using ClientApi.Data;
using ClientApi.Data.Services;
using ClientApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClientApi.Controllers
{
    public class PlaygroundController : Controller
    {
        private readonly IPlaygroundService _service;
        public PlaygroundController(IPlaygroundService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAll() {
            var data = await _service.GetAllAsync();
            return View(data);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Destination,CordinatesX,CordinatesY,Capacity")] Playground playground)
        {
            if (!ModelState.IsValid)
            {
                return View(playground);
            }
            _service.AddAsync(playground);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            var data = await _service.GetByIdAsync(id);
            return View(data);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var data = await _service.GetByIdAsync(id);
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Destination,CordinatesX,CordinatesY,Capacity")] Playground playground)
        {
            await _service.UpdateAsync(id, playground);
            return RedirectToAction(nameof(Details), new { id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var playground = await _service.GetByIdAsync(id);
            if (playground == null) return View("NotFound");
            return View(playground);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var playground = await _service.GetByIdAsync(id);
            if (playground == null) return View("NotFound");
            await _service.DeleteAsync(id);
            if (!ModelState.IsValid)
            {
                return View(playground);
            }
            
            return RedirectToAction(nameof(GetAll));
        }
    }
}
