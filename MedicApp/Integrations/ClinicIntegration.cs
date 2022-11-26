using MedicApp.Controllers;
using MedicApp.Database;
using MedicApp.Models;
using MedicApp.RelationshipTables;
using static MedicApp.Models.DbClinic;

namespace MedicApp.Integrations
{
    public interface IClinicIntegration
    {
        //Task<ClinicListModel> LoadAllClinics(Guid id);
    }
    public class ClinicIntegration
    {
        private readonly AppDbContext _appDbContext;
        public ClinicIntegration(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<DbClinic> SaveClinic(ClinicSaveModel clincSaveModel)
        {
            var dbClinic = _appDbContext.Clinics.FirstOrDefault(x => x.Id == clincSaveModel.Id && x.IsDeleted == false);
            if (dbClinic == null)
            {
                dbClinic = new DbClinic();
            }
            dbClinic.Name = clincSaveModel.Name;
            dbClinic.Description = clincSaveModel.Description;
            dbClinic.Rating = clincSaveModel.Rating;
            dbClinic.Email = clincSaveModel.Email;

            #region Address
            var dbAddress = _appDbContext.Addresses.FirstOrDefault(x => x.Id == clincSaveModel.Address.Id && x.IsDeleted == false);
            if (dbAddress == null)
            {
                dbAddress = new DbAddress()
                {
                    Address = clincSaveModel.Address.Address,
                    City = clincSaveModel.Address.City,
                };

                _appDbContext.Addresses.Add(dbAddress);

                var clinic2Address = new Clinic2Address()
                {
                    Clinic_RefID = dbClinic.Id,
                    Address_RefID = dbAddress.Id
                };
                _appDbContext.Clinic2Addresses.Add(clinic2Address);

                _appDbContext.SaveChanges();
            }
            #endregion

            #region AdminCenter
            var dbAdminCenter = _appDbContext.Employees.FirstOrDefault(x => x.Id == clincSaveModel.AdminCenter.Id && x.IsAdminCenter == true && x.IsDeleted == false);
            if (dbAdminCenter == null)
            {
                dbAdminCenter = new DbEmployee()
                {
                    FistName = clincSaveModel.AdminCenter.FistName,
                    LastName = clincSaveModel.AdminCenter.LastName,
                    Email = clincSaveModel.AdminCenter.Email,
                    Mobile = clincSaveModel.AdminCenter.Mobile,
                    Birthday = clincSaveModel.AdminCenter.Birthday,
                    IsAdminCenter = true,
                };
                _appDbContext.Employees.Add(dbAdminCenter);

                var clinic2AdminCenter = new Clinic2AdminCenter()
                {
                    Clinic_RefID = dbClinic.Id,
                    AdminCenter_RefID = dbAdminCenter.Id
                };
                _appDbContext.Clinic2AdminCenters.Add(clinic2AdminCenter);
                _appDbContext.SaveChanges();
            }

            #endregion

            #region Employees
            if (clincSaveModel.Employees.Any())
            {
                List<DbEmployee> employees = _appDbContext.Employees.Where(x => x.IsDeleted == false).ToList();
                foreach (var employee in clincSaveModel.Employees)
                {
                    if (!employees.Any(x => x.Id == employee.Id))
                    {
                        var dbModelEmployee = new DbEmployee()
                        {
                            FistName = employee.FistName,
                            LastName = employee.FistName,
                            Email = employee.FistName,
                            Mobile = employee.FistName,
                            IsAdminCenter = false
                        };

                        var clinic2Employee = new Clinic2Employee()
                        {
                            Clinic_RefID = dbClinic.Id,
                            Employee_RefID = dbModelEmployee.Id
                        };
                        _appDbContext.Clinic2Employees.Add(clinic2Employee);
                        _appDbContext.Employees.Add(dbModelEmployee);
                        _appDbContext.SaveChanges();
                    }
                }
            }
            #endregion

            #region Working Hours
            if (clincSaveModel.WorkingHours.Any())
            {
                var dbWorkingHours = _appDbContext.WorkingHours.Where(x => x.IsDeleted == false).ToList();
                foreach(var item in clincSaveModel.WorkingHours)
                {
                    if(!dbWorkingHours.Any(x =>x.Id == item.Id))
                    {
                        var model = new DbWorkingHours()
                        {
                            WorkDay = item.WorkDay,
                            WorkDuration = item.WorkDuration,
                            WorkStart = item.WorkStart,
                        };

                        var clinic2WorkingHours = new Clinic2WorkingHours()
                        {
                            Clinic_RefID = dbClinic.Id,
                            WorkingHours_RefID = model.Id
                        };

                        _appDbContext.Clinic2WorkingHours.Add(clinic2WorkingHours);
                        _appDbContext.WorkingHours.Add(model);
                        _appDbContext.SaveChanges();
                    }
                }

            }
            #endregion;

            return dbClinic;
        }

        public async Task<ClinicSaveModel> LoadClinicById(Guid id)
        {
           var dbClinic = 

            return null;
        }
    }
}
