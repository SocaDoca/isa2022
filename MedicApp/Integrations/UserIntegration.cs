using MedicApp.Database;
using MedicApp.Models;
using MedicApp.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using static MedicApp.Controllers.AuthController;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;

namespace MedicApp.Integrations
{
    public interface IUserIntegration
    {
        User? Register(RegisterRequest model);
        LoginResponse LogIn(LoginModel model);
        IEnumerable<UserLoadModel> GetAll();
        UserLoadModel GetUserById(Guid id);
        bool UpdatePassword(Guid Id, string password);
        void Delete(Guid id);
    }

    public class UserIntegration : IUserIntegration
    {
        private readonly AppDbContext _appDbContext;
        private readonly IOptions<SecretSettings> _secretSettings;


        public UserIntegration(AppDbContext context, IConfiguration config, IOptions<SecretSettings> secretSettings)
        {
            _appDbContext = context;
            _secretSettings = secretSettings;
        }

        public LoginResponse LogIn(LoginModel model)
        {

            var findUser = _appDbContext.Users.FirstOrDefault(x => x.Username == model.Username);
            if (findUser == null)
            {
                throw new Exception();
            }
            var matchPassword = CheckPassword(model.Password, findUser);
            if (!matchPassword)
            {
                throw new Exception();
            }
            //var role = _appDbContext.Roles.FirstOrDefault(x => x.Name == findUser.Role);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secretSettings.Value.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", findUser.Username), new Claim(ClaimTypes.Role, findUser.Role) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encrypterToken = tokenHandler.WriteToken(token);

            return new LoginResponse
            {
                Id = findUser.Id,
                Username = findUser.Username,
                FirstName = findUser.FirstName,
                LastName = findUser.LastName,
                Token = encrypterToken,
                Role = findUser.Role,
            };
        }

        private bool CheckPassword(string password, User user)
        {
            bool result;
            using (HMACSHA512? hmac = new HMACSHA512(user.PasswordSalt))
            {
                var compute = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                result = compute.SequenceEqual(user.PasswordHash);
            }
            return result;
        }

        public IEnumerable<UserLoadModel> GetAll()
        {
            var resultList = new List<UserLoadModel>();
            
            foreach(var user in _appDbContext.Users)
            {
                resultList.Add(GetUserById(user.Id));
            };

            return resultList;
        }

        public User? Register(RegisterRequest model)
        {
            if (_appDbContext.Users.Any(x => x.Username == model.Username))
                throw new AppException("Username '" + model.Username + "' is already taken");

            // map model to new user object
            var newUser = new User()
            {
                Username = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model?.Address ?? string.Empty,
                Country = model?.Country ?? string.Empty,
                City = model?.City ?? string.Empty,
                Email = model.Email,
                Gender = model.Gender,
                Role = model.Roles,
                JMBG = model.JMBG,
                Mobile = model.Moblie,
                Job = model.Job,

            };
            
            if (model.Password == model.ConfirmPassword)
            {
                using (HMACSHA512? hmac = new HMACSHA512())
                {
                    newUser.PasswordSalt = hmac.Key;
                    newUser.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(model.Password));
                }
            }
            else
            {
                return null;
            }
            // save user
            _appDbContext.Users.Add(newUser);
            _appDbContext.SaveChanges();
            return newUser;
        }

        public bool UpdatePassword(Guid Id, string password)
        {
            var findUser = _appDbContext.Users.FirstOrDefault(x => x.Id == Id);
            if (findUser == null)
            {
                return false;
            }
            using (HMACSHA512? hmac = new HMACSHA512())
            {
                findUser.PasswordSalt = hmac.Key;
                findUser.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            return true;
        }

        public void Delete(Guid id)
        {
            var user = _appDbContext.Users.FirstOrDefault(x => x.Id == id);
            _appDbContext.Users.Remove(user);
            _appDbContext.SaveChanges();
        }



        public UserLoadModel GetUserById(Guid id)
        {
            var dbUser = _appDbContext.Users.Find(id);
            if (dbUser == null)
            {
                throw (new KeyNotFoundException("User not found"));
            }
            var resultUser = new UserLoadModel
            {
                Id = dbUser.Id,
                FullAddress = String.Format("{0}, {1}, {2}", dbUser.Address, dbUser.City, dbUser.Country),
                Gender = dbUser.Gender,
                Job = dbUser.Job, 
                Role = dbUser.Role,
                LoyaltyPoints = dbUser.LoyaltyPoints,
                Name = String.Format("{0} {1}", dbUser.FirstName, dbUser.LastName),
                Username = dbUser.Username,
                Email = dbUser.Email,
                Mobile = dbUser.Mobile,
                JMBG = dbUser.JMBG
            };

            return resultUser;

        }
    }



}
