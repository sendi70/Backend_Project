﻿using AuthenticationServer.Models;
using AuthenticationServer.Models.Requests;
using AuthenticationServer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthenticationServer.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public AuthenticationController(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if(registerRequest.Password != registerRequest.ConfirmPassword)
            {
                return BadRequest();
            }
            User existingUserByEmail = await _userRepository.GetByEmail(registerRequest.Email);
            if(existingUserByEmail != null)
            {
                return Conflict();
            }
            User existingUserByUsername = await _userRepository.GetByUsername(registerRequest.Email);
            if(existingUserByUsername != null)
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
    }
}