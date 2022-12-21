using MedicApp.Database;
using MedicApp.Enums;
using MedicApp.Models;
using MedicApp.Utils;
using Microsoft.AspNetCore.Mvc;


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
        private AppDbContext _context;
        private IJwtUtils _jwtUtils;


        public UserIntegration(
            AppDbContext context,
            IJwtUtils jwtUtils

            )
        {
            _context = context;
            _jwtUtils = jwtUtils;


        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == model.Username);

            // validate
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                throw new AppException("Username or password is incorrect");

            // authentication successful
            var response = new AuthenticateResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = model.Username,
            };
            response.Token = _jwtUtils.GenerateToken(user);
            return response;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(Guid id)
        {
            return getUser(id);
        }

        public void Register(RegisterRequest model)
        {
            // validate
            if (_context.Users.Any(x => x.Username == model.Username))
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
            newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // save user
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        public void Update(Guid id, UpdateRequest model)
        {
            var user = getUser(id);

            // validate
            if (model.Username != user.Username && _context.Users.Any(x => x.Username == model.Username))
                throw new AppException("Username '" + model.Username + "' is already taken");

            // hash password if it was entered
            if (!string.IsNullOrEmpty(model.Password))
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // copy model to user and save
            user.Username = model.Username;
            user.Address = model.Address;
            user.City = model.City;
            user.Email = model.Email;
            user.Roles = model.Role;

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var user = getUser(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        // helper methods

        private User getUser(Guid id)
        {
            var user = _context.Users.Find(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }
    }



}
