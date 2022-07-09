using BackEndProject.Models;
using ClientApi.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClientApi.Data.Services
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllAsync();
        Task<Event> GetByIdAsync(int id);
        Task AddAsync(Event ev);
        Task<Event> UpdateAsync(int id, Event ev);
        Task DeleteAsync(int id);
        Task<IEnumerable<SelectListItem>> GetPlaygroundsAsync();
        Task<User> GetUserAsync(string username);
        Task<Playground> GetPlaygroundAsync(int id);
    }
}
