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
        Appointment SaveAppointment(AppointmentSaveModel appointmentSave);
        AppointmentLoadModel LoadAppointmentById(Guid Id);
        List<Appointment> CreatePredefiendAppointments(SavePredefiendAppointment predefiendAppointment);
        List<AppointmentLoadModel> LoadAllAppointmentsByPatientId(Guid patientId);
        List<AppointmentLoadModel> LoadAllAppointmnetsByClinicId(Guid clinicId);
        bool CancelAppointment(Guid appointmenetId);
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


        public Appointment SaveAppointment(AppointmentSaveModel appointmentSave)
        {
            var dbClinic = _appDbContext.Clinics.First(x => !x.IsDeleted && appointmentSave.Clinic_RefID == x.Id);
            var dbPatient = _appDbContext.Users.First(x => !x.IsDeleted && x.Id == appointmentSave.Patient_RefID);
            var appointment2Clinics = _appDbContext.Appointment2Clinics.Where(x => x.Clinic_RefID == appointmentSave.Clinic_RefID).Select(x => x.Appointment_RefID).ToList();


            var clinic2WorkingHours = _appDbContext.Clinic2WorkingHours.Where(x => x.Clinic_RefID == dbClinic.Id).Select(x => x.WorkingHours_RefID).ToList();
            var dbWorkingHours = _appDbContext.WorkingHours.Where(x => clinic2WorkingHours.Any(s => s == x.Id)).ToList();
            if (dbWorkingHours.Any())
            {
                if (dbWorkingHours.Any(x => TimeOnly.Parse(x.Start) <= TimeOnly.Parse(appointmentSave.StartTime) &&
                                           TimeOnly.Parse(x.End) >= TimeOnly.Parse(appointmentSave.StartTime)))
                {
                    var appointment = new Appointment()
                    {

                        Clinic_RefID = appointmentSave.Clinic_RefID,
                        IsFinished = appointmentSave.IsFinished,
                        IsCanceled = appointmentSave.IsCanceled,
                        Patient_RefID = appointmentSave.Patient_RefID,
                        StartDate = appointmentSave.StartDate.Add(TimeSpan.Parse(appointmentSave.StartTime)),
                    };

                    _appDbContext.Appointments.Add(appointment);
                    _appDbContext.SaveChanges();
                    var report = new AppointmentReport
                    {
                        Description = appointmentSave.Report.Description,
                        Equipment = appointmentSave.Report.Equipment
                    };

                    _appDbContext.AppointmentsReports.Add(report);
                    _appDbContext.SaveChanges();
                    var appointmnet2report = new Appointment2Report
                    {
                        Appointment_RefID = appointment.Id,
                        ReportId = report.Id,
                    };

                    _appDbContext.Appointment2Reports.Add(appointmnet2report);
                    _appDbContext.SaveChanges();
                    var appointment2Patient = new Appointment2Patient
                    {
                        Appointment_RefID = appointment.Id,
                        Patient_RefID = dbPatient.Id,
                    };

                    _appDbContext.Appointment2Patients.Add(appointment2Patient);
                    _appDbContext.SaveChanges();
                    var appointment2Clinic = new Appointment2Clinic
                    {
                        Appointment_RefID = appointment.Id,
                        Clinic_RefID = dbClinic.Id
                    };
                    _appDbContext.Appointment2Clinics.Add(appointment2Clinic);
                    _appDbContext.SaveChanges();

                    /*   var code = IronBarCode.BarcodeWriter.CreateBarcode(appointment.Id.ToByteArray(), BarcodeWriterEncoding.QRCode);
                       code.SetMargins(100);
                       code.ChangeBarCodeColor(Color.Purple);
                       _emailUtils.SendMail(code.ToString(), String.Format("Appointmnet for patient {0} {1}", dbPatient.FirstName, dbPatient.LastName), dbPatient.Email, _emailSettings.Value.SenderAddress);
                    */

                    return appointment;
                }

                return null;

            }
            return null;
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
            var patients = _userIntegration.LoadUserBasicInfoByIds(patientIds);
            var companyIds = dbAppointments.Where(x => x.Clinic_RefID.HasValue).Select(x => x.Clinic_RefID.Value).ToList();
            var companyInfos = _clinicIntegration.LoadClinicBasicInfoByIds(companyIds);
            return dbAppointments.Select(appointment => new AppointmentLoadModel
            {
                Clinic = companyInfos.FirstOrDefault(x => x.Id == appointment.Clinic_RefID),
                Id = appointment.Id,
                IsCanceled = appointment.IsCanceled,
                IsFinished = appointment.IsFinished,
                IsPredefiend = appointment.IsPredefiend,
                Patient = patients.FirstOrDefault(x => x.Id == appointment.Patient_RefID),
                StartDate = appointment.StartDate,
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

                var appointment2Clinics = new Appointment2Clinic
                {
                    Appointment_RefID = model.Id,
                    Clinic_RefID = model.Clinic_RefID.Value
                };

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
            return true;
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
            return true;
        }

        public bool CancelAppointmnet(Guid appointmentId)
        {
            var dbAppointmnet = _appDbContext.Appointments.SingleOrDefault(x => x.Id == appointmentId && x.IsDeleted == false);
            var dbPatient = _appDbContext.Users.SingleOrDefault(x => x.Id == dbAppointmnet.Patient_RefID && x.IsDeleted == false);
            dbAppointmnet.IsCanceled = true;
            dbPatient.Penalty++;
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
