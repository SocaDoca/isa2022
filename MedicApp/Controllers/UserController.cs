using MedicApp.Authorization;
using MedicApp.Database;
using MedicApp.Integrations;
using MedicApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedicApp.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class UserController : ControllerBase
    {

        private readonly AppDbContext _appDbContext;
        private readonly IUserIntegration _userIntegration;

        public UserController(AppDbContext appDbContext, IUserIntegration userIntegration)
        {
            _userIntegration = userIntegration;
            _appDbContext = appDbContext;
        }

        [HttpGet("get-by-id")]
        public UserLoadModel GetUserById(Guid Id)
        {
            return _userIntegration.GetUserById(Id);
        }

        [HttpPost("get-all")]
        public List<UserLoadModel> GetAll([FromBody]LoadAllUsersParameters parameters)
        {
            return _userIntegration.GetAll(parameters);
        }
        [HttpPost("get-all-employees")]
        public List<UserListModel> GetAllEmployess()
        {
            return _userIntegration.GetAllEmployess();
        }

        [HttpDelete("remove")]
        public bool DeleteUserById(Guid id)
        {
            return _userIntegration.Delete(id);
        }

        [HttpPut("{id:guid}/update-password")]
        public bool UpdatePassword(Guid id, string password)
        {
            return _userIntegration.UpdatePassword(id, password);
        }

        [HttpPost("update-user")]
        public bool UpdateUser([FromBody]UpdateUser updateUser)
        {
            return _userIntegration.UpdateUser(updateUser);
        }

        [HttpPost("questionnaire")]
        public Questionnaire SaveQuestionnaire(SaveQuestionnaire questionnaire, Guid patientId)
        {
            return _userIntegration.CreateQuestionnaireForPatientById(questionnaire, patientId);
        } 
        
        [HttpGet("get-questionnaire")]
        public SaveQuestionnaire GetQuestionnaireByUserId(Guid userId)
        {
            return _userIntegration.GetQuestionnaireByUserId(userId);
        }
        [HttpGet("get-all-users-with-appointments")]
        public List<UserListModel> GetAllUsersByAdmin(LoadAllUsersParameters parameters)
        {
            return _userIntegration.GetAllUsers(parameters);
        }

       
    }
}
