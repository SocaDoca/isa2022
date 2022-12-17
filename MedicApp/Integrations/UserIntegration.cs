using MedicApp.Database;
using MedicApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace MedicApp.Integrations
{
    public interface IUserIntegration
    {

    }
    public class UserIntegration
    {
        public readonly AppDbContext _appDbContext;
        public UserIntegration(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


       
        private void CreatePasswordHash(String password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }



}
