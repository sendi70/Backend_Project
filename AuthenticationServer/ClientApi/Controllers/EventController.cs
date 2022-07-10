using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClientApi.Data;
using ClientApi.Models;
using BackEndProject.Models;
using ClientApi.Data.Services;
using X.PagedList;

namespace ClientApi.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _service;

        public EventController(IEventService service)
        {
            _service = service;
        }

        // GET: Events
        public async Task<IActionResult> Index(string sortOrder, string currentFilter,string searchString, int? page)
        {
            ViewBag.SortOrder = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var data = await _service.GetAllAsync();
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            switch (sortOrder)
            {
                case "name_desc":
                    data = data.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    data = data.OrderBy(s => s.StartTime);
                    break;
                case "date_desc":
                    data = data.OrderByDescending(s => s.StartTime);
                    break;
                default:
                    data = data.OrderBy(s => s.Name);
                    break;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                data = data.Where(s => s.Name.Contains(searchString));
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(data.ToPagedList(pageNumber,pageSize));
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.PlaygroundId = await _service.GetPlaygroundsAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StartTime,EndTime,PlaygroundId")] Event ev)
        {
            ev.Organiser = await _service.GetUserAsync(Request.Cookies["X-Username"]);
            ev.Playground = await _service.GetPlaygroundAsync(ev.PlaygroundId);
            ModelState.ClearValidationState(nameof(Event));
            _service.AddAsync(ev);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var data = await _service.GetByIdAsync(id);
            ViewBag.PlaygroundId = await _service.GetPlaygroundsAsync();
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Destination,CordinatesX,CordinatesY,Capacity")] Event ev)
        {
            var data = await _service.GetByIdAsync(id);
            var currUser = await _service.GetUserAsync(Request.Cookies["X-Username"]);
            if (data.Organiser == currUser)
            {
            await _service.UpdateAsync(id, ev);
            return RedirectToAction(nameof(Details), new { id });
            }
            return View("Not authorized");
        }
        public async Task<IActionResult> Details(int id)
        {
            var data = await _service.GetByIdAsync(id);
            ViewBag.Playground = await _service.GetPlaygroundAsync(data.PlaygroundId);
            return View(data);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _service.GetByIdAsync(id);
            ViewBag.Playground = await _service.GetPlaygroundAsync(data.PlaygroundId);
            return View(data);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var data = await _service.GetByIdAsync(id);
            var currUser = await _service.GetUserAsync(Request.Cookies["X-Username"]);
            if (data.Organiser == currUser)
            {
                var ev = await _service.GetByIdAsync(id);
                if (ev == null) return View("NotFound");
                await _service.DeleteAsync(id);
                if (!ModelState.IsValid)
                {
                    return View(ev);
                }
            return RedirectToAction(nameof(Index));
            }
            return View("Not allowed");
        }
    }
}
