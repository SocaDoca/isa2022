using MedicApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MedicApp.Database;
using MedicApp.Enums;
using Microsoft.AspNetCore.Authorization;
using MedicApp.Integrations;
using MedicApp.Utils;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace MedicApp.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class AuthController : ControllerBase
    {
        public readonly IUserIntegration _userIntegration;

        public AuthController(IUserIntegration userIntegration)
        {
            _userIntegration = userIntegration;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] SaveUserModel model)
        {
            return Ok(_userIntegration.SaveUser(model));
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            return Ok(_userIntegration.LogIn(model));
        }


    }
}
