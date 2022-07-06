using System.ComponentModel.DataAnnotations;

namespace AuthenticationServer.Models.Requests
{
    public class RefreshRequest
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
