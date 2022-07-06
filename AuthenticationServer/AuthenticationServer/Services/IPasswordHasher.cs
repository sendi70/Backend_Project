namespace AuthenticationServer.Services
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
    }
}
