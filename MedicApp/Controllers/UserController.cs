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

        [HttpGet("user/{id}")]
        public UserLoadModel GetUserById([FromBody]Guid Id)
        {
            return _userIntegration.GetUserById(Id);
        }

    }
}
