﻿using MedicApp.Integrations;
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
        public Roles SaveRole([FromBody] SaveRole role)
        {
            return _rolesIntegration.CreateRole(role);
        }

        [HttpPost("load-all-roles")]
        public List<LoadRole> GetAllRoles()
        {
            return _rolesIntegration.GetAllRoles();
        }
    }
}
