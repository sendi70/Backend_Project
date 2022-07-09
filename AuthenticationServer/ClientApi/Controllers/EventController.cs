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
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
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
        //// GET: Events/Create
        //public IActionResult Create()
        //{
        //    ViewData["PlaygroundId"] = new SelectList(_service.Events, "Id", "Id");
        //    return View();
        //}

        //// POST: Events/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Name,StartTime,EndTime,PlaygroundId")] Event @event)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(@event);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["PlaygroundId"] = new SelectList(_context.Events, "Id", "Id", @event.PlaygroundId);
        //    return View(@event);
        //}
        // GET: Events/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.Events == null)
        //    {
        //        return NotFound();
        //    }

        //    var @event = await _context.Events
        //        .Include(@ => @.Playground)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (@event == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(@event);
        //}


        //// GET: Events/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Events == null)
        //    {
        //        return NotFound();
        //    }

        //    var @event = await _context.Events.FindAsync(id);
        //    if (@event == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["PlaygroundId"] = new SelectList(_context.Playgrounds, "Id", "Id", @event.PlaygroundId);
        //    return View(@event);
        //}

        //// POST: Events/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartTime,EndTime,PlaygroundId")] Event @event)
        //{
        //    if (id != @event.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(@event);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!EventExists(@event.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["PlaygroundId"] = new SelectList(_context.Playgrounds, "Id", "Id", @event.PlaygroundId);
        //    return View(@event);
        //}

        //// GET: Events/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Events == null)
        //    {
        //        return NotFound();
        //    }

        //    var @event = await _context.Events
        //        .Include(@ => @.Playground)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (@event == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(@event);
        //}

        //// POST: Events/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Events == null)
        //    {
        //        return Problem("Entity set 'AppDbContext.Events'  is null.");
        //    }
        //    var @event = await _context.Events.FindAsync(id);
        //    if (@event != null)
        //    {
        //        _context.Events.Remove(@event);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool EventExists(int id)
        //{
        //  return (_context.Events?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
