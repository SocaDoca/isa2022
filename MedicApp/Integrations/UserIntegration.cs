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
using MedicApp.RelationshipTables;

namespace MedicApp.Integrations
{
    public interface IUserIntegration
    {
        User? Register(RegisterRequest model);
        LoginResponse LogIn(LoginModel model);
        List<UserLoadModel> GetAll();
        UserLoadModel GetUserById(Guid id);
        bool UpdateUser(UpdateUser updateUser);
        bool UpdatePassword(Guid Id, string password);
        bool Delete(Guid id);
        User SaveUser(SaveUserModel createUser);
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

        public User SaveUser(SaveUserModel createUser)
        {
            var dbUser = _appDbContext.Users.Where(x => !x.IsDeleted && x.Id == createUser.Id).FirstOrDefault();
            if (dbUser == null)
            {
                dbUser = new User()
                {
                    JMBG = createUser.JMBG,
                };
            }
            dbUser.FirstName = createUser.FirstName;
            dbUser.LastName = createUser.LastName;
            dbUser.Email = createUser.Email;
            dbUser.Address = createUser.Address;
            dbUser.Username = createUser.Username;
            dbUser.City = createUser.City;
            dbUser.Country = createUser.Country;
            dbUser.Gender = createUser.Gender;
            dbUser.Job = createUser.Job;
            dbUser.IsAdminCenter = createUser.IsAdminCenter;
            dbUser.Role = createUser.Roles;
            dbUser.Mobile = createUser.Moblie;

            if (createUser.Password == createUser.ConfirmPassword)
            {
                using (HMACSHA512? hmac = new HMACSHA512())
                {
                    dbUser.PasswordSalt = hmac.Key;
                    dbUser.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(createUser.Password));
                }
            }

            _appDbContext.Users.Add(dbUser);
            _appDbContext.SaveChanges();
            return dbUser;
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

        public Questionnaire CreateQuestionnaireForPatientById(Questionnaire questionnaire, Guid PatientId)
        {
            var dbPatient = _appDbContext.Users.FirstOrDefault(x => x.Id == PatientId && !x.IsDeleted && x.Role == "Patient");
            if (dbPatient == null)
            {
                throw new Exception("Patient does not exist");
            }
            var dbQuestionnaire = new Questionnaire
            {
                ExpireDate = DateTime.Now.AddMonths(6),
                question1 = questionnaire.question1,
                question2 = questionnaire.question2,
                question3 = questionnaire.question3,
                question4 = questionnaire.question4,
                question5 = questionnaire.question5,
                question6 = questionnaire.question6,
                question7 = questionnaire.question7,
                question8 = questionnaire.question8,
                question9 = questionnaire.question9,
                question10 = questionnaire.question10,
                question11 = questionnaire.question11,
                question12 = questionnaire.question12,
            };
            _appDbContext.Questionnaire.Add(dbQuestionnaire);
            
            var patient2questinnaire = new Patient2Questionnaire
            {
                Patient_RefId = dbPatient.Id,
                Questionnaire_RefId = dbQuestionnaire.Id
            };
            _appDbContext.Patient2Questionnaires.Add(patient2questinnaire);
            _appDbContext.SaveChanges();
            return dbQuestionnaire;
        }

        #region Delete
        public bool Delete(Guid id)
        {
            var user = _appDbContext.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                throw new KeyNotFoundException("User does not exist");
            }
            _appDbContext.Users.Remove(user);
            _appDbContext.SaveChanges();
            return true;
        }

        #endregion

        #region Update 
        public bool UpdateUser(UpdateUser updateUser)
        {
            var getUser = _appDbContext.Users.First(x => x.Id == updateUser.Id);

            if (getUser == null)
            {
                return false;
            }
            getUser.Address = updateUser.Address;
            getUser.Email = updateUser.Email;
            getUser.Role = updateUser.Role;
            getUser.City = updateUser.City;
            getUser.Country = updateUser.Country;

            _appDbContext.Users.Update(getUser);
            _appDbContext.SaveChanges();
            return true;
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
            _appDbContext.Users.Update(findUser);
            _appDbContext.SaveChanges();
            return true;
        }


        #endregion

        #region Get Methods
        public List<UserLoadModel> GetAll()
        {
            var resultList = new List<UserLoadModel>();

            foreach (var user in _appDbContext.Users)
            {
                resultList.Add(GetUserById(user.Id));
            };

            return resultList;
        }

        public UserLoadModel GetUserById(Guid id)
        {
            var dbUser = _appDbContext.Users.FirstOrDefault(x => x.Id == id);
            if (dbUser == null)
            {
                throw (new KeyNotFoundException("User not found"));
            }
            var resultUser = new UserLoadModel
            {
                Id = dbUser.Id,
                FullAddress = String.Format("{0}, {1}, {2}", dbUser.Address, dbUser.City, dbUser.Country) ?? String.Empty,
                Gender = dbUser.Gender,
                Job = dbUser.Job ?? String.Empty,
                Role = dbUser.Role,
                LoyaltyPoints = dbUser.LoyaltyPoints,
                Name = String.Format("{0} {1}", dbUser.FirstName, dbUser.LastName) ?? String.Empty,
                Username = dbUser.Username,
                Email = dbUser.Email ?? String.Empty,
                Mobile = dbUser.Mobile ?? String.Empty,
                JMBG = dbUser.JMBG ?? String.Empty,
                IsAdminCenter = dbUser.IsAdminCenter

            };

            return resultUser;

        }

        #endregion

        #region Support methods
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
        #endregion
    }



}
