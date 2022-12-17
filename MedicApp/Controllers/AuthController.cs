using MedicApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace MedicApp.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        public static DbAccount account = new DbAccount();
        public AuthController()
        {

        }

        //[HttpPost("register")]
        //public async Task<ActionResult<DbAccount>> Register(AccountDto request)
        //{
        //    //CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        //    account.PasswordHash = passwordHash;
        //    account.PasswordSalt = passwordSalt;
        //    account.Username = request.Username;
        //    account.Email = request.Email;  
        //    account.Role = request.Role;

        //    return Ok(account);
        //}


       
    }
}
