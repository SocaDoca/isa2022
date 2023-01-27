﻿using IronBarCode;
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
    }
    public class AppointmentIntegration : IAppointmentIntegration
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserIntegration _userIntegration;
        private readonly IClinicIntegration _clinicIntegration;
        private readonly IEmailUtils _emailUtils;
        public readonly IOptions<EmailSettings> _emailSettings;

        public AppointmentIntegration(AppDbContext appDbContext, IUserIntegration userIntegration, IClinicIntegration clinicIntegration,IOptions<EmailSettings>emailSettings ,IEmailUtils emailUtils)
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


            if (appointment2Clinics.Contains(appointmentSave.Id))
            {
                throw new Exception("Appointment already exist");
            }
            var clinic2WorkingHours = _appDbContext.Clinic2WorkingHours.Where(x => x.Clinic_RefID == dbClinic.Id).Select(x => x.WorkingHours_RefID).ToList();
            var dbWorkingHours = _appDbContext.WorkingHours.Where(x => clinic2WorkingHours.Any(s => s == x.Id)).ToList();
            if (dbWorkingHours.Any())
            {
                if (dbWorkingHours.Any(x => TimeOnly.Parse(x.Start) <= TimeOnly.Parse(appointmentSave.StartTime) &&
                                           TimeOnly.Parse(x.End) >= TimeOnly.Parse(appointmentSave.StartTime)))
                {
                    var appointment = new Appointment()
                    {
                        Title = appointmentSave.Title,
                        Clinic_RefID = appointmentSave.Clinic_RefID,
                        IsFinished = appointmentSave.IsFinished,
                        IsCanceled = appointmentSave.IsCanceled,
                        Patient_RefID = appointmentSave.Patient_RefID,
                        StartDate = appointmentSave.StartDate.Add(TimeSpan.Parse(appointmentSave.StartTime)),
                    };

                    _appDbContext.Appointments.Add(appointment);

                    var report = new AppointmentReport
                    {
                        Description = appointmentSave.Report.Description,
                        Equipment = appointmentSave.Report.Equipment
                    };

                    _appDbContext.AppointmentsReports.Add(report);

                    var appointmnet2report = new Appointment2Report
                    {
                        Appointment_RefID = appointment.Id,
                        ReportId = report.Id,
                    };

                    _appDbContext.Appointment2Reports.Add(appointmnet2report);

                    var appointment2Patient = new Appointment2Patient
                    {
                        Appointment_RefID = appointment.Id,
                        Patient_RefID = dbPatient.Id,
                    };

                    _appDbContext.Appointment2Patients.Add(appointment2Patient);

                    var appointment2Clinic = new Appointment2Clinic
                    {
                        Appointment_RefID = appointment.Id,
                        Clinic_RefID = dbClinic.Id
                    };
                    _appDbContext.Appointment2Clinics.Add(appointment2Clinic);
                    _appDbContext.SaveChanges();

                    var code = IronBarCode.BarcodeWriter.CreateBarcode(appointment.Id.ToByteArray(), BarcodeWriterEncoding.QRCode);
                    code.SetMargins(100);
                    code.ChangeBarCodeColor(Color.Purple);
                    _emailUtils.SendMail(code.ToString(), String.Format("Appointmnet for patient {0} {1}", dbPatient.FirstName, dbPatient.LastName), dbPatient.Email, _emailSettings.Value.SenderAddress);


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
            result.Title = dbAppointment.Title;
            result.StartDate = dbAppointment.StartDate;
            var clinicModel = new ClinicLoadModel()
            {
                Id = dbClinic.Id,
                Address = dbClinic.Address,
                City = dbClinic.City,
                Country = dbClinic.Country,
                Name = dbClinic.Name,
                Description = dbClinic.Description,
                Phone = dbClinic.Phone,
                Rating = dbClinic.Rating,
            };

            result.Clinic = clinicModel;

            var patientModel = new UserLoadModel()
            {
                Id = dbUser.Id,
                FullAddress = String.Format("{0} {1} {2}", dbUser.Address, dbUser.City, dbUser.Country),
                FirstName = dbUser.FirstName,
                LastName = dbUser.LastName,
                Email = dbUser.Email,
                Mobile = dbUser.Mobile, 
                JMBG = dbUser.JMBG,

            };
            result.Patient = patientModel;

            return result;
        }


        public List<Appointment> CreatePredefiendAppointments(SavePredefiendAppointment predefiendAppointment)
        {
            var dbAppointments = _appDbContext.Appointments.Where(x => !x.IsDeleted && x.IsPredefiend && predefiendAppointment.Date == x.StartDate).ToList();
            var result = new List<Appointment>();
      
          
                for (int i = 0; i <= predefiendAppointment.NumberOfAppointmentsInDay; i++)
                {
                    var predefAppointmnet = new Appointment
                    {
                        Duration = predefiendAppointment.Duration,
                        IsPredefiend = true,
                        StartDate = predefiendAppointment.Date.Value.Add(TimeSpan.Parse(predefiendAppointment.Time) + TimeSpan.FromMinutes(predefiendAppointment.Duration * i))

                    };
                    result.Add(predefAppointmnet);
                    _appDbContext.Appointments.Add(predefAppointmnet);
                }
                _appDbContext.SaveChanges();
            
            return result.OrderBy(x => x.StartDate).ToList();

        }

    }

}
