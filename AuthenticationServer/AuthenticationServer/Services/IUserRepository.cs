using AuthenticationServer.Models;
using System;
using System.Threading.Tasks;

namespace AuthenticationServer.Services
{
    public interface IUserRepository
    {
        Task<User> GetByEmail(string email);
        Task<User> GetByUsername(string username);
        Task<User> GetById(Guid Id);
        Task<User> Create(User user);
    }
}
