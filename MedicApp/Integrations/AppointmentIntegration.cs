using IronBarCode;
using MedicApp.Database;
using MedicApp.Models;
using MedicApp.RelationshipTables;
using MedicApp.Utils;
using MedicApp.Utils.AppSettings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Drawing;

namespace MedicApp.Integrations
{
    public interface IAppointmentIntegration
    {
        bool SaveAppointment(Guid appointmentId, ReportSaveModel report);
        AppointmentLoadModel LoadAppointmentById(Guid Id);
        List<Appointment> CreatePredefiendAppointments(SavePredefiendAppointment predefiendAppointment);
        List<AppointmentLoadModel> LoadAllAppointmentsByPatientId(Guid patientId);
        List<AppointmentLoadModel> LoadAllAppointmnetsByClinicId(Guid clinicId);
        bool ReserveAppointment(Guid appointmentId, Guid patientId);
        bool CancelAppointment(Guid appointmenetId);
        bool StartAppointmnet(Guid appointmentId);
        bool FinishAppointment(Guid appointmentId);
        List<LoadPredefiendAppointment> LoadPredefiendAppointments(Guid clinicId);
    }
    public class AppointmentIntegration : IAppointmentIntegration
    {
        private readonly AppDbContext _appDbContext;

        private readonly IUserIntegration _userIntegration;
        private readonly IClinicIntegration _clinicIntegration;
        private readonly IEmailUtils _emailUtils;
        public readonly IOptions<EmailSettings> _emailSettings;

        public AppointmentIntegration(AppDbContext appDbContext, IUserIntegration userIntegration, IClinicIntegration clinicIntegration, IOptions<EmailSettings> emailSettings, IEmailUtils emailUtils)
        {
            _appDbContext = appDbContext;
            _userIntegration = userIntegration;
            _clinicIntegration = clinicIntegration;
            _emailUtils = emailUtils;
            _emailSettings = emailSettings;
        }

        public bool SaveAppointment(Guid appointmentId, ReportSaveModel report)
        {
            var dbAppointment = _appDbContext.Appointments.FirstOrDefault(x => x.IsDeleted == false && x.Id == appointmentId);
            if (dbAppointment == null)
            {
                throw new Exception("appointment does not exist");
            }
            
            var dbReport = _appDbContext.AppointmentsReports.FirstOrDefault(x => x.IsDeleted == false && x.Id == report.Id);
            if(dbReport == null)
            {
                dbReport = new AppointmentReport();
            }
            
            dbReport.Description = report.Description;

            foreach (var item in report.Equipment)
            {
                var itemModel = new WorkItem
                {
                    Name = item.Name,
                    UsedInstances = item.UsedInstances
                };
                _appDbContext.WorkItems.Add(itemModel);
                var workItem2Report = new WorkItem2Reports
                {
                    Report_RefID = dbReport.Id,
                    WorkItem_RefID = itemModel.Id
                };

                _appDbContext.WorkItem2Reports.Add(workItem2Report);
            }
            _appDbContext.SaveChanges();
            return true;

        }
        public bool CancelAppointment(Guid appointmenetId)
        {
            var appointment = _appDbContext.Appointments.FirstOrDefault(x => !x.IsDeleted && x.Id == appointmenetId && !x.IsFinished);
            var dbPatient = _appDbContext.Users.FirstOrDefault(x => x.Id == appointment.Patient_RefID);
            if (dbPatient == null)
            {
                throw new Exception("Patient does not exist");
            }
            dbPatient.Penalty++;
            if (appointment == null)
            {
                throw new Exception("Appointment does not exist");
            }

            if (appointment.IsCanceled)
            {
                return false;
            }

            appointment.IsCanceled = true;

            _appDbContext.Appointments.Update(appointment);
            _appDbContext.SaveChanges();

            var emailBody = "Your appointment has been canceled";
            _emailUtils.SendMail(emailBody, "Appointment cancelation", dbPatient.Email, _emailSettings.Value.SenderAddress);
            return true;

        }
        public AppointmentLoadModel LoadAppointmentById(Guid Id)
        {
            var dbAppointment = _appDbContext.Appointments.FirstOrDefault(x => !x.IsDeleted && x.Id == Id);
            var dbClinic = _appDbContext.Clinics.First(x => x.Id == dbAppointment.Clinic_RefID.Value);
            var dbUser = _appDbContext.Users.First(x => x.Id == dbAppointment.Patient_RefID.Value);
            if (dbAppointment == null)
            {
                throw new KeyNotFoundException("Appointment does not exist");
            }
            var result = new AppointmentLoadModel();
            result.Id = Id;

            result.StartDate = dbAppointment.StartDate;
            var clinicModel = new ClinicBasicInfo()
            {
                Id = dbClinic.Id,
                Address = dbClinic.Address,
                City = dbClinic.City,
                Country = dbClinic.Country,
                Name = dbClinic.Name,
                Description = dbClinic.Description,
                Phone = dbClinic.Phone,

            };

            result.Clinic = clinicModel;

            var patientModel = new UserBasicInfo()
            {
                Id = dbUser.Id,
                Address = dbUser.Address,
                City = dbUser.City,
                Country = dbUser.Country,
                FirstName = dbUser.FirstName,
                LastName = dbUser.LastName,
                Email = dbUser.Email,
                Mobile = dbUser.Mobile

            };
            result.Patient = patientModel;

            return result;
        }
        public List<AppointmentLoadModel> LoadAllAppointmentsByPatientId(Guid patientId)
        {
            var dbPatient = _appDbContext.Users.First(x => x.Id == patientId && x.Role == "User");
            var appointment2PatientIds = _appDbContext.Appointment2Patients.Where(x => x.Patient_RefID == dbPatient.Id).Select(x => x.Appointment_RefID).ToList();
            var dbAppointments = _appDbContext.Appointments.Where(x => appointment2PatientIds.Any(s => s == x.Id)).ToList();
            var dbClinics = _appDbContext.Clinics.ToList().Where(x => !x.IsDeleted).GroupBy(x => x.Id).ToDictionary(x => x.Key, x => x.Single());
            var result = new List<AppointmentLoadModel>();
            foreach (var item in dbAppointments)
            {
                var dbClinic = dbClinics[item.Clinic_RefID.Value];
                var clinicLoad = new ClinicBasicInfo
                {
                    Id = dbClinic.Id,
                    Name = dbClinic.Name,
                    Address = dbClinic.Address,
                    City = dbClinic.City,
                    Country = dbClinic.Country,
                    Phone = dbClinic.Phone,
                    Description = dbClinic.Description

                };
                var patient = new UserBasicInfo
                {
                    Id = dbPatient.Id,
                    FirstName = dbPatient.FirstName,
                    LastName = dbPatient.LastName,
                    Address = dbPatient.Address,
                    City = dbPatient.City,
                    Country = dbPatient.Country,
                    Email = dbPatient.Email,
                    Mobile = dbPatient.Mobile,
                };
                var model = new AppointmentLoadModel
                {
                    Clinic = clinicLoad,
                    Id = item.Id,
                    StartDate = item.StartDate,
                    Patient = patient,
                    IsCanceled = item.IsCanceled,
                    IsFinished = item.IsFinished,
                    IsPredefiend = item.IsPredefiend,
                    StartTime = item.StartDate.TimeOfDay.ToString(),
                };
                result.Add(model);
            }

            return result;

        }
        public List<AppointmentLoadModel> LoadAllAppointmnetsByClinicId(Guid clinicId)
        {
            var dbAppointments2Clinic = _appDbContext.Appointment2Clinics.Where(x => x.Clinic_RefID == clinicId && x.IsDeleted == false).ToList();
            var appointmentIds = dbAppointments2Clinic.Select(x => x.Appointment_RefID).ToList();
            var dbAppointments = _appDbContext.Appointments.Where(x => appointmentIds.Any(Id => Id == x.Id)).ToList();
            var patientIds = dbAppointments.Where(x => x.Patient_RefID.HasValue).Select(x => x.Patient_RefID.Value).ToList();
            var patients = _userIntegration.LoadUserBasicInfoByIds(patientIds).GroupBy(x => x.Id).ToDictionary(x => x.Key, x => x.First());
            var companyIds = dbAppointments.Where(x => x.Clinic_RefID.HasValue).Select(x => x.Clinic_RefID.Value).ToList();
            var clinicInfo = _clinicIntegration.LoadClinicBasicInfoByIds(companyIds).GroupBy(x => x.Id).ToDictionary(x => x.Key, x => x.First());
            return dbAppointments.Select(appointment =>
            {
                var result = new List<AppointmentLoadModel>();
                var appModel = new AppointmentLoadModel
                {
                    Id = appointment.Id,
                    IsCanceled = appointment.IsCanceled,
                    IsFinished = appointment.IsFinished,
                    IsPredefiend = appointment.IsPredefiend,
                    StartDate = appointment.StartDate,
                };

                if (appointment.Patient_RefID.HasValue && patients.TryGetValue(appointment.Patient_RefID.Value, out var patient))
                {
                    appModel.Patient = patient;
                }
                if (appointment.Clinic_RefID.HasValue && clinicInfo.TryGetValue(appointment.Clinic_RefID.HasValue ? appointment.Clinic_RefID.Value : Guid.Empty, out var clinic))
                {
                    appModel.Clinic = clinic;
                }
                return appModel;
            }).ToList();

        }
        public List<Appointment> CreatePredefiendAppointments(SavePredefiendAppointment predefiendAppointment)
        {
            var dbAppointments = _appDbContext.Appointments.Where(x => !x.IsDeleted && x.IsPredefiend && predefiendAppointment.Date == x.StartDate).ToList();
            var dbClinic = _appDbContext.Clinics.First(x => x.Id == predefiendAppointment.Clinic_RefID);
            var result = new List<Appointment>();
            var count = 0;
            while (count <= predefiendAppointment.NumberOfAppointmentsInDay)
            {
                var model = new Appointment
                {
                    Clinic_RefID = predefiendAppointment.Clinic_RefID,
                    Duration = predefiendAppointment.Duration,
                    IsPredefiend = true,
                    StartDate = predefiendAppointment.Date.Value.AddMinutes(predefiendAppointment.Duration * count),
                    IsCanceled = false,
                    IsFinished = false,

                };

                _appDbContext.Appointments.Add(model);

                var appointment2Clinics = new Appointment2Clinic
                {
                    Appointment_RefID = model.Id,
                    Clinic_RefID = model.Clinic_RefID.Value
                };

                _appDbContext.Appointment2Clinics.Add(appointment2Clinics);
                count++;
                result.Add(model);
            }
            _appDbContext.SaveChanges();

            return result.OrderBy(x => x.StartDate).ToList();

        }
        public bool ReserveAppointment(Guid appointmentId, Guid patientId)
        {
            var dbAppointment = _appDbContext.Appointments.SingleOrDefault(x => x.IsReserved == false && x.IsDeleted == false && x.Id == appointmentId);
            if (dbAppointment == null)
            {
                return false;
            }
            dbAppointment.Patient_RefID = patientId;
            dbAppointment.IsReserved = true;
            _appDbContext.Update(dbAppointment);
            _appDbContext.SaveChanges();
            return true;
            /*   var code = IronBarCode.BarcodeWriter.CreateBarcode(appointment.Id.ToByteArray(), BarcodeWriterEncoding.QRCode);
               code.SetMargins(100);
               code.ChangeBarCodeColor(Color.Purple);
               _emailUtils.SendMail(code.ToString(), String.Format("Appointmnet for patient {0} {1}", dbPatient.FirstName, dbPatient.LastName), dbPatient.Email, _emailSettings.Value.SenderAddress);
            */
        }
        public bool StartAppointmnet(Guid appointmentId)
        {
            var dbAppointment = _appDbContext.Appointments.SingleOrDefault(x => x.IsReserved == true && x.IsDeleted == false && x.Id == appointmentId);
            if (dbAppointment == null)
            {
                return false;
            }
            dbAppointment.IsReserved = false;
            dbAppointment.IsStarted = true;

            _appDbContext.Update(dbAppointment);
            _appDbContext.SaveChanges();
            return true;
        }
        public bool FinishAppointment(Guid appointmentId)
        {
            var dbAppointment = _appDbContext.Appointments.SingleOrDefault(x => x.IsStarted == true && x.IsDeleted == false && x.Id == appointmentId);
            if (dbAppointment == null)
            {
                return false;
            }
            dbAppointment.IsStarted = false;
            dbAppointment.IsFinished = true;

            _appDbContext.Update(dbAppointment);
            _appDbContext.SaveChanges();
            return true;
        }
        public bool CancelAppointmnet(Guid appointmentId)
        {
            var dbAppointment = _appDbContext.Appointments.SingleOrDefault(x => x.Id == appointmentId && x.IsDeleted == false);
            var dbPatient = _appDbContext.Users.SingleOrDefault(x => x.Id == dbAppointment.Patient_RefID && x.IsDeleted == false);
            dbAppointment.IsCanceled = true;
            dbPatient.Penalty++;

            _appDbContext.Update(dbAppointment);
            _appDbContext.SaveChanges();
            return true;
        }

        public List<LoadPredefiendAppointment> LoadPredefiendAppointments(Guid clinicId)
        {
            var appointments2Clinic = _appDbContext.Appointment2Clinics.Where(x => x.Clinic_RefID == clinicId && x.IsDeleted == false);
            var appointmentIds = appointments2Clinic.Select(x => x.Appointment_RefID).ToList();
            var appoitments = _appDbContext.Appointments.Where(x => x.IsPredefiend == true && appointmentIds.Any(Id => x.Id == Id)).ToList();

            return appoitments.Select(x => new LoadPredefiendAppointment { Id = x.Id, StartDate = x.StartDate }).ToList();
        }
    }
}
