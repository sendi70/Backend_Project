using Microsoft.AspNetCore.Identity;
using System;

namespace AuthenticationServer.Models
{
    public class User : IdentityUser<Guid>
    {
    }
}
