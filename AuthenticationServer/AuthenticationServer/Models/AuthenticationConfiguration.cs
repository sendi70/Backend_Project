namespace AuthenticationServer.Models
{
    public class AuthenticationConfiguration
    {
        public string AccesTokenSecret { get; set; }
        public int AccessTokenExpirationMinutes { get; set; }
        public string Issuer { get; set; };
        public string Audience { get; set; };
    }
}
