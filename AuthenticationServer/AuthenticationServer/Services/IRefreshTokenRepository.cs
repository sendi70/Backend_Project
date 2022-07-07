using AuthenticationServer.Models;
using System;
using System.Threading.Tasks;

namespace AuthenticationServer.Services
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetRefreshToken(string token);
        Task Create(RefreshToken refreshToken);
        Task Delete(Guid id);
        Task DeleteAll(Guid userId);
    }
}
