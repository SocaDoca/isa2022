using MedicApp.Database;
using MedicApp.Enums;
using MedicApp.Migrations;
using MedicApp.Models;
using MedicApp.RelationshipTables;

namespace MedicApp.Integrations
{
    public class PatientIntegration
    {

        public readonly AppDbContext _appDbContext;

        public PatientIntegration(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public DbPatient CreatePatient(SavePatientModel savePatientModel)
        {
            var newPatient = _appDbContext.Patients.FirstOrDefault(x => x.Id == savePatientModel.Id && !x.IsDeleted);
            if (newPatient == null)
            {
                var newAccount = new DbAccount()
                {
                    Email = savePatientModel.Email,
                    IsActive = false,
                    Role = Role.User,
                    Password = savePatientModel.Password
                };
                _appDbContext.Accounts.Add(newAccount);

                newPatient = new DbPatient()
                {
                    FirstName = savePatientModel.FirstName,
                    LastName = savePatientModel.LastName,
                    Gender = savePatientModel.Gender,
                    Birthday = savePatientModel.BirthDate,
                    JMBG = savePatientModel.JMBG
                };
                _appDbContext.Patients.Add(newPatient);

                var account2Patient = new Account2Patient
                {
                    Account_RefID = newAccount.Id,
                    Patient_RefID = newPatient.Id,
                };

                _appDbContext.Account2Patients.Add(account2Patient);
            }

            return newPatient;
        }


        public DbPatient UpdatePatient(SavePatientModel savePatientModel)
        {
            var dbPatient = _appDbContext.Patients.First(x => x.Id == savePatientModel.Id && !x.IsDeleted);
            if(dbPatient == null)
            {
                return null;
            }

            dbPatient.FirstName = savePatientModel.FirstName;
            dbPatient.LastName = savePatientModel.LastName;
            dbPatient.Birthday = savePatientModel.BirthDate;
            dbPatient.Job = savePatientModel.Job;

            _appDbContext.Patients.Add(dbPatient);
            _appDbContext.SaveChanges();

            return dbPatient;
        }
        public bool DeletePatient(SavePatientModel savePatientModel)
        {
            var dbPatient = _appDbContext.Patients.First(x => x.Id == savePatientModel.Id && !x.IsDeleted);
            var account2Patient = _appDbContext.Account2Patients.FirstOrDefault(x => x.Patient_RefID == dbPatient.Id);

            var account = _appDbContext.Accounts.FirstOrDefault(x => x.Id == account2Patient.Account_RefID);
            if(dbPatient == null)
            {
                return false;
            }

            account2Patient.IsDeleted = true;
            account.IsDeleted = true;
            dbPatient.IsDeleted = true;

            _appDbContext.Patients.Add(dbPatient);
            _appDbContext.Accounts.Add(account);
            _appDbContext.Account2Patients.Add(account2Patient);
            _appDbContext.SaveChanges();

            return true; ;
        }

        public bool ChangePassword(Guid id , string newPassword)
        {
            var patient = _appDbContext.Patients.First(x => x.Id == id && !x.IsDeleted);
            var account2Patient = _appDbContext.Account2Patients.First(x => x.Patient_RefID == patient.Id && !x.IsDeleted);
            var account = _appDbContext.Accounts.First(x => x.Id == account2Patient.Account_RefID && !x.IsDeleted);
            if (patient.IsConfirmed == true && account.IsPasswordConfirmed == true)
            {                
                account.Password = newPassword;
                
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ConfirmPatient(Guid Id , string newPassword)
        {
            var patient = _appDbContext.Patients.First(x => x.Id == Id && !x.IsDeleted);
            var account2Patient = _appDbContext.Account2Patients.First(x => x.Patient_RefID == patient.Id && !x.IsDeleted);
            var account = _appDbContext.Accounts.First(x => x.Id == account2Patient.Account_RefID && !x.IsDeleted);
            if(patient.IsConfirmed == false && account.IsPasswordConfirmed == false)
            {
                patient.IsConfirmed = true;
                account.Password = newPassword;
                account.IsPasswordConfirmed = true;

                return true;

            }
            else
            {
                return false;
            }

        }

    }

    public class SavePatientModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string JMBG { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string Job { get; set; }
    }
}
