using Microsoft.EntityFrameworkCore;

namespace AuthenticationServer.Models
{
    public class AuthenticationDbContext : DbContext
    {
        public AuthenticationDbContext( DbContextOptions<AuthenticationDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
