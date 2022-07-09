using BackEndProject.Models;
using ClientApi.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ClientApi.Data.Services
{
    public class EventService : IEventService
    {
        private readonly AppDbContext _context;

        public EventService(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Event ev)
        {
            await _context.Events.AddAsync(ev);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Events.FirstOrDefaultAsync(n => n.Id == id);
            _context.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            var result = await _context.Events.Include(e => e.Playground).ToListAsync();
            return result;
        }

        public Task<Event> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Event> UpdateAsync(int id, Event ev)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<SelectListItem>> GetPlaygroundsAsync()
        {
            var result = await _context.Playgrounds.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name

            }).ToListAsync();
            return result;
        }

        public async Task<User> GetUserAsync(string username)
        {
            var result = await _context.Users.FirstOrDefaultAsync(x => x.Name == username);
            return result;
        }
        public async Task<Playground> GetPlaygroundAsync(int id)
        {
            var result = await _context.Playgrounds.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
        public Playground GetPlayground(int id)
        {
            var result = _context.Playgrounds.FirstOrDefault(x => x.Id == id);
            return result;
        }
        //public User GetUserAsync(string username)
        //{
        //    var result =  _context.Users.FirstOrDefault(x=>x.Name == username);
        //    return result;
        //}

    }
}
