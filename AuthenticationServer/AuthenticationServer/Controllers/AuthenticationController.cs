using AuthenticationServer.Models;
using AuthenticationServer.Models.Requests;
using AuthenticationServer.Models.Responses;
using AuthenticationServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthenticationServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly UserManager<User> _userRepository;
        private readonly Authenticator _authenticator;
        private readonly RefreshTokenValidator _refreshTokenValidator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AuthenticationController(UserManager<User> userRepository,
            Authenticator authenticator,
            RefreshTokenValidator refreshTokenValidator,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _authenticator = authenticator;
            _refreshTokenValidator = refreshTokenValidator;
            _refreshTokenRepository = refreshTokenRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (registerRequest.Password != registerRequest.ConfirmPassword)
            {
                return BadRequest();
            }

            User registrationUser = new User()
            {
                Email = registerRequest.Email,
                UserName = registerRequest.Username
            };

            IdentityResult result = await _userRepository.CreateAsync(registrationUser, registerRequest.Password);
            if (!result.Succeeded)
            {
                IdentityErrorDescriber errorDescriber = new IdentityErrorDescriber();
                IdentityError primaryError = result.Errors.FirstOrDefault();

                if (primaryError.Code == nameof(errorDescriber.DuplicateEmail))
                {
                    return Conflict();
                }
                else if (primaryError.Code == nameof(errorDescriber.DuplicateUserName))
                {
                    return Conflict();
                }
            }

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            User user = await _userRepository.FindByNameAsync(loginRequest.Username);
            if (user == null)
            {
                return Unauthorized();
            }

            bool isCorrectPassword = await _userRepository.CheckPasswordAsync(user, loginRequest.Password);
            if (!isCorrectPassword)
            {
                return Unauthorized();
            }

            AuthenticatedUserResponse response = await _authenticator.Authenticate(user);

            return Ok(response);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest refreshRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            bool isValidRefreshToken = _refreshTokenValidator.Validate(refreshRequest.RefreshToken);
            if (!isValidRefreshToken)
            {
                return BadRequest();
            }

            RefreshToken refreshTokenDTO = await _refreshTokenRepository.GetRefreshToken(refreshRequest.RefreshToken);
            if (refreshTokenDTO == null)
            {
                return NotFound();
            }

            await _refreshTokenRepository.Delete(refreshTokenDTO.Id);

            User user = await _userRepository.FindByIdAsync(refreshTokenDTO.UserId.ToString());
            if (user == null)
            {
                return NotFound();
            }

            AuthenticatedUserResponse response = await _authenticator.Authenticate(user);

            return Ok(response);
        }

        [Authorize]
        [HttpDelete("logout")]
        public async Task<IActionResult> Logout()
        {
            string rawUserId = HttpContext.User.FindFirstValue("id");

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            await _refreshTokenRepository.DeleteAll(userId);

            return NoContent();
        }
    }
}
