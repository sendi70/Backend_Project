using AuthenticationServer.Models;
using AuthenticationServer.Models.Requests;
using AuthenticationServer.Models.Responses;
using AuthenticationServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthenticationServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly Authenticator _authenticator;
        private readonly RefreshTokenValidator _refreshTokenValidator;

        public AuthenticationController(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, IPasswordHasher passwordHasher, Authenticator authenticator, RefreshTokenValidator refreshTokenValidator)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _passwordHasher = passwordHasher;
            _authenticator = authenticator;
            _refreshTokenValidator = refreshTokenValidator;
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
            User existingUserByEmail = await _userRepository.GetByEmail(registerRequest.Email);
            if (existingUserByEmail != null)
            {
                return Conflict();
            }
            User existingUserByUsername = await _userRepository.GetByUsername(registerRequest.Username);
            if (existingUserByUsername != null)
            {
                return Conflict();
            }

            string passwordHash = _passwordHasher.HashPassword(registerRequest.Password);
            User registrationUser = new User()
            {
                Email = registerRequest.Email,
                Username = registerRequest.Username,
                PasswordHash = passwordHash
            };
            await _userRepository.Create(registrationUser);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            User user = await _userRepository.GetByUsername(loginRequest.Username);
            if (user == null)
            {
                return Unauthorized();
            }

            bool isCorectPassword = _passwordHasher.VerifyPassword(loginRequest.Password, user.PasswordHash);
            if (!isCorectPassword)
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
            if(refreshTokenDTO == null)
            {
               return NotFound();
            }
            await _refreshTokenRepository.Delete(refreshTokenDTO.Id);
            User user=await _userRepository.GetById(refreshTokenDTO.UserId);
            if(user == null)
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
            if(!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }
            await _refreshTokenRepository.DeleteAll(userId);
            return NoContent();
        }
    }
}
