using BackEndProject.Models;
using ClientApi.Models;

namespace ClientApi.Data.Services
{
    public interface IPlaygroundService
    {
        Task<IEnumerable<Playground>> GetAllAsync();
        Task<Playground> GetByIdAsync(int id);
        Task AddAsync(Playground playground);
        Task<Playground> UpdateAsync(int id,Playground playground);
        Task DeleteAsync(int id);
    }
}
