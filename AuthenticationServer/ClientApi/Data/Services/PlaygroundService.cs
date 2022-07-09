using BackEndProject.Models;
using ClientApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientApi.Data.Services
{
    public class PlaygroundService : IPlaygroundService
    {
        private readonly AppDbContext _context;

        public PlaygroundService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Playground playground)
        {
            await _context.Playgrounds.AddAsync(playground);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Playgrounds.FirstOrDefaultAsync(n => n.Id==id);
            _context.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Playground>> GetAllAsync()
        {
            var result = await _context.Playgrounds.ToListAsync();   
            return result;
        }

        public async Task<Playground> GetByIdAsync(int id)
        {
            var result = await _context.Playgrounds.FirstOrDefaultAsync(x => x.Id == id); 
            return result;
        }

        public async Task<Playground> UpdateAsync(int id, Playground playground)
        {
            var oldEntry = await _context.Playgrounds.FirstOrDefaultAsync(x => x.Id == id);
            oldEntry.Name = playground.Name;
            oldEntry.Destination = playground.Destination;
            oldEntry.Capacity = playground.Capacity;
            oldEntry.CordinatesX = playground.CordinatesX;
            oldEntry.CordinatesY = playground.CordinatesY;
            _context.Entry(oldEntry).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return playground;
        }
    }
}
