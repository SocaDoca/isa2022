﻿using MedicApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MedicApp.Database;
using MedicApp.Enums;
using Microsoft.AspNetCore.Authorization;
using MedicApp.Integrations;

namespace MedicApp.Controllers
{
    [ApiController]
    [Route("/api/auth")]
    public class AuthController : ControllerBase
    {
        private IUserIntegration _userIntegration;

        public AuthController(
           IUserIntegration userIntegration)
        {
            _userIntegration = userIntegration;
        }

       [HttpPost("signin")]
        public async Task<IActionResult> SignInAsync([FromBody] SignInRequest signInRequest)
        {       
            var identity = await _userIntegration.SignInAsync(signInRequest);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                });

            return Ok(new Response(true, "Signed in successfully"));
        }

        [Authorize]
        [HttpGet("user")]
        public IActionResult GetUser()
        {
            var appDbContext = new AppDbContext();
            var userClaims = User.Claims.Select(x => new UserClaim(x.Type, x.Value)).ToList();
            return Ok(userClaims);
        }


        [Authorize]
        [HttpGet("signout")]
        public async Task SignOutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public record SignInRequest(string Username, string Password);
        public record Response(bool IsSuccess, string Message);
        public record UserClaim(string Type, string Value);        
 
    }
}