using MedicApp.Database;
using MedicApp.Models;
using MedicApp.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedicApp.Integrations
{
    public interface IRolesIntegration
    {
        Roles CreateRole([FromBody] SaveRole role);
        bool Delete(Guid Id);
        LoadRole GetRoleById(Guid Id);
        List<LoadRole> GetAllRoles();
    }
    public class RolesIntegration : IRolesIntegration
    {
        private readonly AppDbContext _appDbContext;

        public RolesIntegration(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Roles CreateRole(SaveRole role)
        {
            var findRole = _appDbContext.Roles.FirstOrDefault(x => x.Id == role.Id && !x.IsDeleted);
            if (findRole == null)
            {
                findRole = new Roles
                {
                    Name = role.Name,
                };
                _appDbContext.Roles.Add(findRole);
            }

            findRole.Name = role.Name;
            _appDbContext.SaveChanges();

            return findRole;
        }
        public bool Delete(Guid Id)
        {
            var findRole = _appDbContext.Roles.FirstOrDefault(x => x.Id == Id && !x.IsDeleted);
            if (findRole == null)
            {
                return false;
            }

            findRole.IsDeleted = true;
            _appDbContext.SaveChanges();

            return true;
        }
        public List<LoadRole> GetAllRoles()
        {
            var dbRoles = _appDbContext.Roles.Where(x => !x.IsDeleted).ToList();

            var result = new List<LoadRole>();
            if(dbRoles.Any())
            {
                result = dbRoles.Select(role => new LoadRole
                {
                    Id = role.Id,
                    Name = role.Name
                }).ToList();
            }

            return result;
        }
        public LoadRole GetRoleById(Guid Id)
        {
            var findRole = _appDbContext.Roles.FirstOrDefault(x => x.Id == Id && !x.IsDeleted);
            if (findRole == null)
            {
                throw new KeyNotFoundException();
            }

            return new LoadRole
            {
                Id = findRole.Id,
                Name = findRole.Name,

            };
        }
    }
}

