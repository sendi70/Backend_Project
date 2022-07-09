using ClientApi.Models;

namespace ClientApi.Data.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
    }
}
