using MedicApp.Integrations;
using MedicApp.Models;
using MedicApp.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize(Roles = "Admin")]
    public class RolesController : ControllerBase
    {
        private readonly IRolesIntegration _rolesIntegration;

        public RolesController(IRolesIntegration rolesIntegration)
        {
            _rolesIntegration = rolesIntegration;
        }

        [HttpPost("save-role")]
        public async Task<StandardIntegrationResponse> SaveRole([FromBody] SaveRole role)
        {
            return new StandardIntegrationResponse
            {
                IsSuccess = true,
                Payload = _rolesIntegration.CreateRole(role)
            };

        }

        [HttpPost("load-all-roles")]
        public async Task<StandardIntegrationResponse> GetAllRoles()
        {
            return new StandardIntegrationResponse
            {
                IsSuccess = true,
                Payload = await _rolesIntegration.GetAllRoles()
            };

        }
    }
}
