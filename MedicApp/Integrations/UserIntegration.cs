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
using System.Security.Policy;
using MedicApp.Utils.AppSettings;
using Microsoft.AspNetCore.Mvc.Routing;

namespace MedicApp.Integrations
{
    public interface IUserIntegration
    {
        User? Register(RegisterRequest model);
        LoginResponse LogIn(LoginModel model);
        List<UserLoadModel> GetAll(LoadAllUsersParameters parameters);
        List<UserListModel> GetAllUsers(LoadAllUsersParameters parameters);
        List<UserListModel> GetAllEmployess();

        UserLoadModel GetUserById(Guid id);
        bool VerifyUser(VerifyParams verifyParams);
        bool UpdateUser(UpdateUser updateUser);
        bool UpdatePassword(Guid Id, string password);
        bool Delete(Guid id);
        List<UserBasicInfo> LoadUserBasicInfoByIds(List<Guid> userIds);
        SaveQuestionnaire GetQuestionnaireByUserId(Guid Id);
        Questionnaire CreateQuestionnaireForPatientById(SaveQuestionnaire questionnaire, Guid PatientId);
        void RemovePenalty();
        int RateClinic(SaveGrade grade);

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
        public void RemovePenalty()
        {
            var dbPenaltyUsers = _appDbContext.Users.Where(x => !x.IsDeleted).ToList();
            foreach (var user in dbPenaltyUsers)
            {
                user.Penalty = 0;
                _appDbContext.SaveChanges();
            }
        }

        #region Registration and Validation
        public bool VerifyUser(VerifyParams verifyParams)
        {
            var dbUser = _appDbContext.Users.FirstOrDefault(x => x.Id == verifyParams.UserId && x.Email == verifyParams.UserEmail);
            if (dbUser == null)
            {
                throw new KeyNotFoundException("User does not exist");
            }
            dbUser.IsVerified = true;
            _appDbContext.Users.Update(dbUser);
            _appDbContext.SaveChanges();
            return true;
        }
        public LoginResponse LogIn(LoginModel model)
        {
            var findUser = _appDbContext.Users.FirstOrDefault(x => x.Email == model.Email);
            if (findUser == null)
            {
                throw new KeyNotFoundException("User dos not exist");
            }
            var matchPassword = CheckPassword(model.Password, findUser);
            if (!matchPassword)
            {
                throw new Exception("Password does not match");
            }
            if (!findUser.IsVerified)
            {
                throw new Exception("User is not verifed");
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secretSettings.Value.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[] { new Claim("Id", findUser.Id.ToString()), new Claim(ClaimTypes.Role, findUser.Role) }),
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
                IsFirstTime = findUser.IsFirstTime
            };
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
                IsAdminCenter = model.IsAdminCenter
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
                throw new Exception("Password does not match");
            }


            // save user
            _appDbContext.Users.Add(newUser);
            _appDbContext.SaveChanges();
            return newUser;
        }
        #endregion
        public Questionnaire CreateQuestionnaireForPatientById(SaveQuestionnaire questionnaire, Guid PatientId)
        {   //nemamo rolu Patient, ispravila na User
            var dbPatient = _appDbContext.Users.FirstOrDefault(x => x.Id == PatientId && !x.IsDeleted && x.Role == "User");
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
                Patient_RefID = dbPatient.Id
            };
            dbQuestionnaire.IsValid = dbQuestionnaire.IsQuestionireSigned() ? true : false;
            _appDbContext.Questionnaire.Add(dbQuestionnaire);

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
            getUser.Username = updateUser.Username;
            getUser.FirstName = updateUser.FirstName;
            getUser.LastName = updateUser.LastName;
            getUser.Address = updateUser.Address;
            getUser.Mobile = updateUser.Mobile;
            getUser.City = updateUser.City;
            getUser.Country = updateUser.Country;
            getUser.Job = updateUser.Job;

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
            findUser.IsFirstTime = false;
            _appDbContext.Users.Update(findUser);
            _appDbContext.SaveChanges();
            return true;
        }
        #endregion

        #region Get Methods
        public List<UserLoadModel> GetAll(LoadAllUsersParameters parameters)
        {
            var resultList = new List<UserLoadModel>();
            var dbUsers = _appDbContext.Users.Where(x => x.Role == "User").ToList();
            var dbQuestionnnaire = _appDbContext.Questionnaire.ToList().GroupBy(x => x.Patient_RefID).ToDictionary(x => x.Key, x => x.OrderByDescending(x => x.Creation_TimeStamp).ToList());
            foreach (var user in dbUsers)
            {

                bool isQesionareValid = false;
                if (dbQuestionnnaire.TryGetValue(user.Id, out var questioneList))
                {
                    var lastQuestionnaire = questioneList.FirstOrDefault();
                    if (lastQuestionnaire != null) isQesionareValid = lastQuestionnaire.IsValid;
                }
                resultList.Add(new UserLoadModel
                {
                    FirstName = user.FirstName ?? String.Empty,
                    LastName = user.LastName ?? String.Empty,
                    Email = user.Email ?? String.Empty,
                    Gender = user.Gender,
                    Id = user.Id,
                    IsAdminCenter = user.IsAdminCenter,
                    JMBG = user.JMBG ?? String.Empty,
                    Job = user.Job ?? String.Empty,
                    Mobile = user.Mobile ?? String.Empty,
                    Username = user.Username ?? String.Empty,
                    Address = user.Address ?? String.Empty,
                    City = user.City ?? String.Empty,
                    Country = user.Country ?? String.Empty,
                    Role = user.Role,
                    IsQuestionnaireValid = isQesionareValid,
                    Penalty = user.Penalty
                });

               
            };

            #region SEARCH
            if (!String.IsNullOrEmpty(parameters.SearchCriteria))
            {
                resultList = resultList.Where(
                    x => x.FirstName.ToLower().Contains(parameters.SearchCriteria) ||
                         x.LastName.ToLower().Contains(parameters.SearchCriteria) ||
                         x.Email.ToLower().Contains(parameters.SearchCriteria)).ToList();
            }
            #endregion

            #region FILTER
            if (parameters.UserFilterParams is not null)
            {
                if (parameters.UserFilterParams.LastName != null)
                    resultList = resultList.Where(x => x.LastName.ToLower().Contains(parameters.UserFilterParams.LastName.ToLower())).ToList();

                if (parameters.UserFilterParams.FirstName != null)
                    resultList = resultList.Where(x => x.FirstName.ToLower().Contains(parameters.UserFilterParams.FirstName.ToLower())).ToList();
            }
            #endregion

            #region SORT

            switch (parameters.SortBy)
            {
                case "firstname":
                    resultList = parameters.OrderAsc ?
                        resultList.OrderBy(x => x.FirstName).ToList() : resultList.OrderByDescending(x => x.FirstName).ToList();
                    break;
                case "lastname":
                    resultList = parameters.OrderAsc ?
                        resultList.OrderBy(x => x.FirstName).ToList() : resultList.OrderByDescending(x => x.FirstName).ToList();
                    break;
                case "address":
                    resultList = parameters.OrderAsc ?
                        resultList.OrderBy(x => x.FirstName).ToList() : resultList.OrderByDescending(x => x.FirstName).ToList();
                    break;
                case "email":
                    resultList = parameters.OrderAsc ?
                        resultList.OrderBy(x => x.FirstName).ToList() : resultList.OrderByDescending(x => x.FirstName).ToList();
                    break;
            }
            #endregion

            return resultList.Skip(parameters.Offset).Take(parameters.Limit).ToList();

        }

        public List<UserListModel> GetAllEmployess()
        {
            var employees = _appDbContext.Users.Where(x => x.Role == "Admin" && !x.IsDeleted).ToList();
            var result = new List<UserListModel>();
            foreach (var item in employees)
            {
                var model = new UserListModel
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName
                };
                result.Add(model);
            }

            return result;
        }

        public SaveQuestionnaire GetQuestionnaireByUserId(Guid Id)
        {
            var dbPatient = _appDbContext.Users.Where(x => x.Id == Id && !x.IsDeleted && x.Role == "User").FirstOrDefault();
            var questionnaire = _appDbContext.Questionnaire.FirstOrDefault(x => !x.IsDeleted && x.Patient_RefID == dbPatient.Id);
            if (questionnaire is null)
            {
                throw new Exception("questionnaire is does not exist");
            }
            var result = new SaveQuestionnaire
            {
                Id = questionnaire.Id,
                ExpireDate = questionnaire.ExpireDate,
                Patient_RefID = questionnaire.Patient_RefID,
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
                IsValid = questionnaire.IsValid
            };
            return result;

        }

        public UserLoadModel GetUserById(Guid id)
        {
            var dbUser = _appDbContext.Users.FirstOrDefault(x => x.Id == id);
            var user2Appointment = _appDbContext.Appointment2Patients.Where(x => x.Patient_RefID == dbUser.Id).ToList();
            var appointmIds = user2Appointment.Select(x => x.Appointment_RefID).ToList();
            var appointmentHistory = _appDbContext.AppointmentHistories.Where(x => appointmIds.Any(s => s == x.AppointmentId.Value)).ToList();
            //var dbQuestionnaire = _appDbContext.Questionnaire.FirstOrDefault(x => x.Patient_RefID == dbUser.Id);
            if (dbUser == null)
            {
                throw (new KeyNotFoundException("User not found"));
            }
            /* var questionModel = new SaveQuestionnaire
             {
                 Id = dbQuestionnaire.Id,
                 ExpireDate = dbQuestionnaire.ExpireDate,
                 Patient_RefID = dbQuestionnaire.Patient_RefID,
                 question1 = dbQuestionnaire.question1,
                 question2 = dbQuestionnaire.question2,
                 question3 = dbQuestionnaire.question3,
                 question4 = dbQuestionnaire.question4,
                 question5 = dbQuestionnaire.question5,
                 question6 = dbQuestionnaire.question6,
                 question7 = dbQuestionnaire.question7,
                 question8 = dbQuestionnaire.question8,
                 question9 = dbQuestionnaire.question9,
                 question10 = dbQuestionnaire.question10,
                 question11 = dbQuestionnaire.question11,
                 question12 = dbQuestionnaire.question12,
                 IsValid = dbQuestionnaire.IsValid
             };*/

            var resultUser = new UserLoadModel
            {
                Id = dbUser.Id,
                Address = dbUser.Address ?? String.Empty,
                City = dbUser.City ?? String.Empty,
                Country = dbUser.Country ?? String.Empty,
                Gender = dbUser.Gender,
                Job = dbUser.Job ?? String.Empty,
                Role = dbUser.Role,
                LoyaltyPoints = dbUser.LoyaltyPoints,
                FirstName = dbUser.FirstName ?? String.Empty,
                LastName = dbUser.LastName ?? String.Empty,
                Username = dbUser.Username ?? String.Empty,
                Email = dbUser.Email ?? String.Empty,
                Mobile = dbUser.Mobile ?? String.Empty,
                JMBG = dbUser.JMBG ?? String.Empty,
                IsAdminCenter = dbUser.IsAdminCenter,
                IsFirstTime = dbUser.IsFirstTime,
                Penalty = dbUser.Penalty,
            };
            if(appointmentHistory.Any())
            {
                appointmentHistory = appointmentHistory.Where(x => x.IsFinishedAppointment).ToList();
                resultUser.AppointmentHistories = appointmentHistory.Select(history => new AppointmentListHistory
                {
                    AppointmentId = history.AppointmentId,
                    Id = history.Id,
                    TimeFinished = history.Creation_TimeStamp
                }).ToList();
            };
            return resultUser;
        }

        public List<UserBasicInfo> LoadUserBasicInfoByIds(List<Guid> userIds)
        {
            var dbPatients = _appDbContext.Users.Where(x => x.IsDeleted == false && userIds.Any(s => s == x.Id)).ToList();
            return dbPatients.Select(patient => new UserBasicInfo
            {
                Id = patient.Id,
                Address = patient.Address,
                City = patient.City,
                Country = patient.Country,
                Email = patient.Email,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Mobile = patient.Mobile
            }).ToList();
        }

        #endregion

        public int RateClinic(SaveGrade grade)
        {
            var dbGrade = _appDbContext.Grades.FirstOrDefault(x => x.Id == grade.Id);
            if (dbGrade == null)
            {
                if (_appDbContext.Grades.Where(x => x.Clinic_RefId == grade.Clinic_RefId && x.Patient_RefId == grade.Patient_RefId).Any())
                {
                    throw new Exception("User already rated this clinic");
                }
                dbGrade = new Grades
                {
                    Clinic_RefId = grade.Clinic_RefId,
                    Patient_RefId = grade.Patient_RefId
                };
            }
            dbGrade.Value = grade.Value;
            _appDbContext.Grades.Add(dbGrade);
            _appDbContext.SaveChanges();
            return dbGrade.Value;
        }

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

        public List<UserListModel> GetAllUsers(LoadAllUsersParameters parameters)
        {
            var dbUser = _appDbContext.Users.Where(x => !x.IsDeleted && x.Role == "User").ToList();
            var dbAppointments2Patient = _appDbContext.Appointment2Patients.Where(x => !x.IsDeleted).GroupBy(x => x.Patient_RefID).ToDictionary(x => x.Key, x => x.OrderByDescending(s => s.Creation_TimeStamp).ToList());
            var result = new List<UserListModel>();
            foreach (var user in dbUser)
            {
                var model = new UserListModel
                {
                    FirstName = user.FirstName ?? string.Empty,
                    Id = user.Id,
                    LastName = user.LastName ?? string.Empty
                };
                if (dbAppointments2Patient.TryGetValue(user.Id, out var appointments))
                {
                    model.LastAppointmentDate = appointments.First().Creation_TimeStamp;
                }
                result.Add(model);
            }
            return result;
        }

    }


    public class VerifyParams
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public string UserEmail { get; set; }
    }


}
