using MedicApp.Database;
using MedicApp.Enums;
using MedicApp.Models;
using MedicApp.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using static MedicApp.Controllers.AuthController;
using System.Security.Claims;

namespace MedicApp.Integrations
{
    public interface IUserIntegration
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(Guid id);
        void Register(RegisterRequest model);
        void Update(Guid id, UpdateRequest model);
        void Delete(Guid id);
    }

    public class UserIntegration : IUserIntegration
    {
        private AppDbContext _appDbContext;
        private IJwtUtils _jwtUtils;


        public UserIntegration(
            AppDbContext context,
            IJwtUtils jwtUtils

            )
        {
            _appDbContext = context;
            _jwtUtils = jwtUtils;


        }

        public async Task<ClaimsIdentity> SignInAsync(SignInRequest signInRequest)
        {           
            var user = _appDbContext.Users.FirstOrDefault(x => x.Email == signInRequest.Username &&
                                       x.Password == signInRequest.Password);
            if (user is null)
            {
                var error = new AppException();
                throw new Exception();
            }

            var claims = new List<Claim>
            {
                new Claim(type: ClaimTypes.Email, value: signInRequest.Username),
                new Claim(type: ClaimTypes.Name,value: String.Format("{0} {1}", user.FirstName, user.LastName))
            };

            return new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        }

        //public AuthenticateResponse Authenticate(AuthenticateRequest model)
        //{
        //    var user = _context.Users.SingleOrDefault(x => x.Username == model.Username);

        //    // validate
        //    if (user == null || (model.Password != user.Password))
        //        throw new AppException("Username or password is incorrect");

        //    // authentication successful
        //    var response = new AuthenticateResponse
        //    {
        //        Id = user.Id,
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        Username = model.Username,
        //    };
        //    response.Token = _jwtUtils.GenerateToken(user);
        //    return response;
        //}

        public IEnumerable<User> GetAll()
        {
            return _appDbContext.Users;
        }

        public User GetById(Guid id)
        {
            return getUser(id);
        }

        public void Register(RegisterRequest model)
        {
            // validate
            if (_appDbContext.Users.Any(x => x.Username == model.Username))
                throw new AppException("Username '" + model.Username + "' is already taken");

            // map model to new user object
            var newUser = new User()
            {
                Username = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                Country = model.Country,
                City = model.City,
                Email = model.Email,
                Gender = model.Gender,
                Roles = model.Roles
            };

            // hash password
            //newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
            newUser.Password = model.Password;
            // save user
            _appDbContext.Users.Add(newUser);
            _appDbContext.SaveChanges();
        }

        public void Update(Guid id, UpdateRequest model)
        {
            var user = getUser(id);

            // validate
            if (model.Username != user.Username && _appDbContext.Users.Any(x => x.Username == model.Username))
                throw new AppException("Username '" + model.Username + "' is already taken");

            // hash password if it was entered
            if (!string.IsNullOrEmpty(model.Password))
                user.Password = model.Password;

            // copy model to user and save
            user.Username = model.Username;
            user.Address = model.Address;
            user.City = model.City;
            user.Email = model.Email;
            user.Roles = model.Role;

            _appDbContext.Users.Update(user);
            _appDbContext.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var user = getUser(id);
            _appDbContext.Users.Remove(user);
            _appDbContext.SaveChanges();
        }

        // helper methods

        private User getUser(Guid id)
        {
            var user = _appDbContext.Users.Find(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }
    }



}
