﻿using MedicApp.Controllers;
using MedicApp.Database;
using MedicApp.Models;
using MedicApp.RelationshipTables;
using static MedicApp.Models.DbClinic;

namespace MedicApp.Integrations
{
    public interface IClinicIntegration
    {
        DbClinic SaveClinic(ClinicSaveModel model);
        ClinicSaveModel LoadClinicById(Guid id);
    }
    public class ClinicIntegration
    {
        private readonly AppDbContext _appDbContext;
        public ClinicIntegration(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public DbClinic SaveClinic(ClinicSaveModel clincSaveModel)
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
                foreach (var item in clincSaveModel.WorkingHours)
                {
                    if (!dbWorkingHours.Any(x => x.Id == item.Id))
                    {
                        var model = new DbWorkingHours()
                        {
                            WorkDay = item.WorkingDay,
                            WorkDuration = item.Duration,
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

        public ClinicSaveModel LoadClinicById(Guid id)
        {
            var dbClinic = _appDbContext.Clinics.FirstOrDefault(x => x.Id == id && x.IsDeleted == false);
            if (dbClinic == null)
            {
                return null;
            }

            var clinicSaveModel = new ClinicSaveModel()
            {
                Id = dbClinic.Id,
                Name = dbClinic.Name,
                Rating = dbClinic.Rating.Value,
                Capacity = dbClinic.Capacity,
                Description = dbClinic.Description ?? String.Empty,
                Email = dbClinic.Email
            };

            var clinic2Employee = _appDbContext.Clinic2Employees.Where(x => x.Clinic_RefID == dbClinic.Id);
            var employeesIds = clinic2Employee.Select(x => x.Employee_RefID).ToList();
            var dbEmployees = _appDbContext.Employees.Where(x => x.IsDeleted == false).ToList();
            var employeeList = new List<EmployeeBasicModel>();

            foreach (var item in employeesIds)
            {
                var dbModel = dbEmployees.FirstOrDefault(x => x.Id == item);
                if (dbModel != null)
                {
                    employeeList.Add(new EmployeeBasicModel()
                    {
                        Id = dbModel.Id,
                        FistName = dbModel.FistName,
                        LastName = dbModel.LastName,
                        Birthday = dbModel.Birthday,
                        Email = dbModel.Email,
                        IsAdmin = dbModel.IsAdminCenter,
                        Mobile = dbModel.Mobile,
                    });
                }
            }

            clinicSaveModel.Employees = employeeList;
            #region Address
            var clinic2Address = _appDbContext.Clinic2Addresses.FirstOrDefault(x => x.Clinic_RefID == dbClinic.Id && x.IsDeleted == false);
            var address = _appDbContext.Addresses.FirstOrDefault(x => x.Id == clinic2Address.Address_RefID);
            if (address != null)
            {
                clinicSaveModel.Address = new AddressBasicInfo()
                {
                    Id = address.Id,
                    Address = address.Address,
                    City = address.City,
                };
            }
            #endregion


            var clinic2AdminCenter = _appDbContext.Clinic2AdminCenters.FirstOrDefault(x => x.Clinic_RefID == dbClinic.Id && x.IsDeleted == false); ;
            var adminCenter = dbEmployees.FirstOrDefault(x => x.Id == clinic2AdminCenter.AdminCenter_RefID && x.IsAdminCenter == true && x.IsDeleted == false);
            if (adminCenter != null)
            {
                clinicSaveModel.AdminCenter = new EmployeeBasicModel()
                {
                    Id = adminCenter.Id,
                    FistName = adminCenter.FistName,
                    LastName = adminCenter.LastName,
                    Birthday = adminCenter.Birthday,
                    Email = adminCenter.Email,
                    IsAdmin = adminCenter.IsAdminCenter,
                    Mobile = adminCenter.Mobile,
                };
            }

            var clinic2WorkingHours = _appDbContext.Clinic2WorkingHours.Where(x => x.Clinic_RefID == dbClinic.Id && x.IsDeleted == false);
            var workingHoursIds = clinic2WorkingHours.Select(x => x.WorkingHours_RefID).ToList();
            var workingHours = _appDbContext.WorkingHours.Where(x => x.IsDeleted == false).ToList();
            var clinicWorkingHours = new List<WorkingHoursBasicInfo>();
            foreach(var item in workingHoursIds)
            {
                var dbWorkingHours = workingHours.FirstOrDefault(x => x.Id == item && x.IsDeleted == false);
                if (dbWorkingHours != null)
                {
                    clinicWorkingHours.Add(new WorkingHoursBasicInfo
                    {
                        Id = dbWorkingHours.Id,
                        Duration = dbWorkingHours.WorkDuration,
                        WorkingDay = dbWorkingHours.WorkDay,
                        WorkStart = dbWorkingHours.WorkStart,
                    });
                }
            }

            clinicSaveModel.WorkingHours = clinicWorkingHours;

            return clinicSaveModel;
        }

        public List<LoadAllClinicsModel> LoadAllClinics()
        {
            var dbClinics = _appDbContext.Clinics.Where(x => x.IsDeleted == false);
            var result = new List<LoadAllClinicsModel>();
            foreach(var clinic in dbClinics)
            {
                result.Add(new LoadAllClinicsModel()
                {
                    Id = clinic.Id,
                    Name = clinic.Name,
                    Address = clinic.Address,
                    Capacity = clinic.Capacity,
                    Rating = clinic.Rating.Value
                });
            }
            return result;
        }
    }
}
