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
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using MedicApp.Utils.AppSettings;


namespace MedicApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        public readonly IUserIntegration _userIntegration;
        public readonly IOptions<SecretSettings> _secretSettings;
        public IUrlHelper _urlHelper;
        public readonly IEmailUtils _emailUtils;
        public readonly IOptions<EmailSettings> _emailSettings;

        public AuthController(IUserIntegration userIntegration, IUrlHelper urlHelper, IOptions<SecretSettings> secretSettings, IEmailUtils emailUtils, IOptions<EmailSettings> emailSettingsModel)
        {
            _userIntegration = userIntegration;
            _secretSettings = secretSettings;
            _urlHelper = urlHelper;
            _emailUtils = emailUtils;
            _emailSettings = emailSettingsModel;

        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] RegisterRequest model)
        {
            var newUser = _userIntegration.Register(model);


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secretSettings.Value.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", newUser.Id.ToString()),
                    new Claim(ClaimTypes.Role, newUser.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                //SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encrypterToken = tokenHandler.WriteToken(token);


            var confirmLink = _urlHelper.Action(new Microsoft.AspNetCore.Mvc.Routing.UrlActionContext
            {
                Action = nameof(VerifyUser),
                Controller = "Auth",
                Values = new
                {
                    token = encrypterToken,
                    email = newUser.Email,
                    userId = newUser.Id,
                },
                Protocol = "http",
                Host = "localhost:3200"
            });
            

            var subject = "Password verification";
            _emailUtils.SendMail(confirmLink, subject, newUser.Email, _emailSettings.Value.SenderAddress);
            return Ok();
        }

        [HttpPost("verify-user")]
        public bool VerifyUser(string token , string email , Guid userId)
        {
            var verifyParams = new VerifyParams
            {
                Token = token,
                UserEmail = email,
                UserId = userId
            };
            return _userIntegration.VerifyUser(verifyParams);
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            return Ok(_userIntegration.LogIn(model));
        }




    }
}
