using IronBarCode;
using MedicApp.Database;
using MedicApp.Models;
using MedicApp.RelationshipTables;
using MedicApp.Utils;
using MedicApp.Utils.AppSettings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Drawing;
using System.Linq;

namespace MedicApp.Integrations
{
    public interface IAppointmentIntegration
    {
        bool SaveAppointmentReport(Guid appointmentId, ReportSaveModel report);
        AppointmentLoadModel LoadAppointmentById(Guid Id);
        List<Appointment> CreatePredefiendAppointments(SavePredefiendAppointment predefiendAppointment);
        List<AppointmentLoadModel> LoadAllAppointmentsByPatientId(Guid patientId);
        List<AppointmentLoadModel> LoadAllAppointmnetsByClinicId(Guid clinicId);
        void ReserveAppointment(Guid appointmentId, Guid patientId);
        void CancelAppointment(Guid appointmenetId);
        void StartAppointmnet(Guid appointmentId);
        void FinishAppointment(Guid appointmentId);
        List<LoadPredefiendAppointment> LoadPredefiendAppointments(Guid clinicId);
        List<AppointmentLoadModel> LoadAllReservedAppointmentsByClinicId(Guid clinicId);
        List<AppointmentLoadModel> LoadAllReservedAppointmnets();


    }
    public class AppointmentIntegration : IAppointmentIntegration
    {
        #region Properties
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
        #endregion

        public bool SaveAppointmentReport(Guid appointmentId, ReportSaveModel report)
        {
            var dbAppointment = _appDbContext.Appointments.FirstOrDefault(x => x.IsDeleted == false && x.Id == appointmentId);
            if (dbAppointment == null)
            {
                throw new Exception("appointment does not exist");
            }

            var dbReport = _appDbContext.AppointmentsReports.FirstOrDefault(x => x.IsDeleted == false && x.Id == report.Id);
            if (dbReport == null)
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
        #region Load methods

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
                    Title = appointment.Title ?? string.Empty,
                    IsReserved = appointment.IsReserved,
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
        public List<AppointmentLoadModel> LoadAllReservedAppointmentsByClinicId(Guid clinicId)
        {
            var dbAppointmentIDs = _appDbContext.Appointment2Clinics.Where(x => !x.IsDeleted && x.Clinic_RefID == clinicId).Select(x => x.Appointment_RefID).ToList();
            var dbClinics = _appDbContext.Clinics.FirstOrDefault(x => x.Id == clinicId);
            var dbAppointments = _appDbContext.Appointments.Where(x => dbAppointmentIDs.Any(s => s == x.Id) && x.IsReserved).ToList();
            var patients2Appointment = _appDbContext.Appointment2Patients.Where(x => !x.IsDeleted && dbAppointmentIDs.Any(s => x.Appointment_RefID == s)).Select(x => x.Patient_RefID).ToList();
            var patients = _appDbContext.Users.Where(x => !x.IsDeleted && x.Role == "User" && patients2Appointment.Any(s => x.Id == s)).ToDictionary(x => x.Id, x => x);
            var result = new List<AppointmentLoadModel>();
            foreach (var appointment in dbAppointments)
            {
                var model = new AppointmentLoadModel
                {
                    Id = appointment.Id,
                    IsCanceled = appointment.IsCanceled,
                    IsPredefiend = appointment.IsPredefiend,
                    IsFinished = appointment.IsFinished,
                    StartDate = appointment.StartDate,
                    Title = appointment.Title,
                };
                if (patients.TryGetValue(appointment?.Patient_RefID.Value ?? Guid.Empty, out var patient))
                {
                    model.Patient = new UserBasicInfo
                    {
                        Id = patient.Id,
                        Address = patient.Address,
                        City = patient.City,
                        Country = patient.Country,
                        Email = patient.Email,
                        FirstName = patient.FirstName,
                        LastName = patient.LastName,
                        Mobile = patient.Mobile
                    };
                }
                model.Clinic = new ClinicBasicInfo
                {
                    Id = dbClinics.Id,
                    Address = dbClinics.Address,
                    City = dbClinics.City,
                    Country = dbClinics.Country,
                    Description = dbClinics.Description,
                    Name = dbClinics.Name,
                    Phone = dbClinics.Phone,
                    WorksFrom = dbClinics.WorksFrom,
                    WorksTo = dbClinics.WorksTo
                };
                result.Add(model);
            }
            return result;
        }
        public List<AppointmentLoadModel> LoadAllReservedAppointmnets()
        {
            var dbAppointmnets = _appDbContext.Appointments.Where(x => !x.IsDeleted && x.IsReserved).ToList();
            var clinicIds = dbAppointmnets.Select(x => x.Clinic_RefID).ToList();
            var clinics = _appDbContext.Clinics.Where(x => clinicIds.Any(s => x.Id == s)).ToDictionary(x => x.Id, x => x);

            var patientsIds = dbAppointmnets.Select(x => x.Patient_RefID).ToList();
            var patients = _appDbContext.Users.Where(x => patientsIds.Any(u => u == x.Id)).ToDictionary(x => x.Id, x => x);
            // var reportsIds = _appDbContext.Appointment2Reports.Where(x => dbAppointmnets.Any(s => x.Appointment_RefID == s.Id)).ToList();
            List<AppointmentLoadModel> result = new List<AppointmentLoadModel>();
            foreach (var item in dbAppointmnets)
            {
                var model = new AppointmentLoadModel
                {
                    Id = item.Id,
                    IsCanceled = item.IsCanceled,
                    IsFinished = item.IsFinished,
                    IsPredefiend = item.IsPredefiend,
                    StartDate = item.StartDate,
                    Report = null,
                };
                if (clinics.TryGetValue(item.Clinic_RefID.Value, out var clinic))
                {
                    model.Clinic = new ClinicBasicInfo
                    {
                        Id = clinic.Id,
                        Address = clinic.Address,
                        City = clinic.City,
                        Country = clinic.Country,
                        Description = clinic.Description,
                        Name = clinic.Name,
                        Phone = clinic.Phone,
                        WorksFrom = clinic.WorksFrom,
                        WorksTo = clinic.WorksTo,
                    };

                }
                if (patients.TryGetValue(item.Patient_RefID.Value, out var patient))
                {
                    model.Patient = new UserBasicInfo
                    {
                        Address = patient.Address,
                        City = patient.City,
                        Country = patient.Country,
                        Email = patient.Email,
                        FirstName = patient.FirstName,
                        LastName = patient.LastName,
                        Id = patient.Id,
                        Mobile = patient.Mobile
                    };
                }
                result.Add(model);
            }
            return result;
        }
        #endregion

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

                var appointmentHistory = new AppointmentHistory
                {
                    AppointmentId = model.Id,
                    IsStartedAppointment = false,
                    IsFinishedAppointment = false
                };

                _appDbContext.AppointmentHistories.Add(appointmentHistory);
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

        #region Change status appointment

        public void ReserveAppointment(Guid appointmentId, Guid patientId)
        {
            var dbAppointment = _appDbContext.Appointments.SingleOrDefault(x => x.IsReserved == false && x.IsDeleted == false && x.Id == appointmentId);
            var dbPatient = _appDbContext.Users.SingleOrDefault(x => x.Id == patientId);
            dbAppointment.Patient_RefID = patientId;
            dbAppointment.IsReserved = true;

            _appDbContext.SaveChanges();

            var code = IronBarCode.BarcodeWriter.CreateBarcode(dbAppointment.Id.ToByteArray(), BarcodeWriterEncoding.QRCode);
            code.SetMargins(100);
            code.ChangeBarCodeColor(Color.Purple);
            _emailUtils.SendMail(code.ToString(), $"Appointmnet for patient {dbPatient.FirstName} {dbPatient.LastName}", dbPatient.Email, _emailSettings.Value.SenderAddress);

        }
        public void StartAppointmnet(Guid appointmentId)
        {
            var dbAppointment = _appDbContext.Appointments.SingleOrDefault(x => x.IsReserved == true && x.IsDeleted == false && x.Id == appointmentId);
            var appointmentHistory = _appDbContext.AppointmentHistories.Where(x => x.AppointmentId == appointmentId).FirstOrDefault();
            dbAppointment.IsReserved = false;
            appointmentHistory.IsStartedAppointment = true;
            dbAppointment.IsStarted = true;

            _appDbContext.SaveChanges();
        }

        public void FinishAppointment(Guid appointmentId)
        {
            var dbAppointment = _appDbContext.Appointments.SingleOrDefault(x => x.IsStarted == true && x.IsDeleted == false && x.Id == appointmentId);
            var appointmentHistory = _appDbContext.AppointmentHistories.Where(x => x.AppointmentId == appointmentId).FirstOrDefault();
            
            appointmentHistory.IsStartedAppointment = false;
            appointmentHistory.IsFinishedAppointment = true;
            dbAppointment.IsStarted = false;
            dbAppointment.IsFinished = true;

            _appDbContext.SaveChanges();

        }
        public void CancelAppointment(Guid appointmentId)
        {
            var dbAppointment = _appDbContext.Appointments.First(x => !x.IsDeleted && x.Id == appointmentId);
            var dbPatient = _appDbContext.Users.FirstOrDefault(x => x.Id == dbAppointment.Patient_RefID && !x.IsDeleted);
            var appointmentHistory = _appDbContext.AppointmentHistories.FirstOrDefault(x => x.AppointmentId == appointmentId);

            if (DateTime.Now < dbAppointment.StartDate.AddDays(-1))
            {
                throw new Exception("Appointment cant be cancled");
            }

            dbPatient.Penalty++;
            appointmentHistory.IsStartedAppointment = false;
            appointmentHistory.IsFinishedAppointment = false;
            dbAppointment.IsCanceled = true;

            _appDbContext.SaveChanges();

        }
        #endregion

        public List<LoadPredefiendAppointment> LoadPredefiendAppointments(Guid clinicId)
        {
            var appointments2Clinic = _appDbContext.Appointment2Clinics.Where(x => x.Clinic_RefID == clinicId && x.IsDeleted == false);
            var appointmentIds = appointments2Clinic.Select(x => x.Appointment_RefID).ToList();
            var appoitments = _appDbContext.Appointments.Where(x => x.IsPredefiend == true && appointmentIds.Any(Id => x.Id == Id) && !x.IsReserved ).ToList();

            return appoitments.Select(x => new LoadPredefiendAppointment { Id = x.Id, StartDate = x.StartDate }).ToList();
        }
    }
}
