﻿using MedicApp.Database;
using MedicApp.Models;
using MedicApp.RelationshipTables;

namespace MedicApp.Integrations
{
    public interface IAppointmentIntegration
    {
        Appointment SaveAppointment(AppointmentSaveModel appointmentSave);
        AppointmentLoadModel LoadAppointmentById(Guid Id);
    }
    public class AppointmentIntegration : IAppointmentIntegration
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserIntegration _userIntegration;
        private readonly IClinicIntegration _clinicIntegration;

        public AppointmentIntegration(AppDbContext appDbContext, IUserIntegration userIntegration, IClinicIntegration clinicIntegration)
        {
            _appDbContext = appDbContext;
            _userIntegration = userIntegration;
            _clinicIntegration = clinicIntegration;
        }


        public Appointment SaveAppointment(AppointmentSaveModel appointmentSave)
        {
            var dbAppointment = _appDbContext.Appointments.Where(x => !x.IsDeleted && x.Id == appointmentSave.Id).FirstOrDefault();
            var dbAppointmentsInDay = _appDbContext.Appointments
                .Where(x => !x.IsDeleted &&
                             x.StartDate == appointmentSave.StartDate &&
                             x.ResponsiblePerson_RefID == appointmentSave.ResponsiblePerson_RefID)
                .ToList();
            var dbClinic = _appDbContext.Clinics.FirstOrDefault(x => !x.IsDeleted && x.Id == appointmentSave.Clinic_RefID);
            var allDbWorkingHours = _appDbContext.WorkingHours.ToList();
            var clinic2WorkingHours = _appDbContext.Clinic2WorkingHours.Where(x => x.Clinic_RefID == dbClinic.Id).ToList();
            var dbWorkingHoursId = clinic2WorkingHours.Select(x => x.WorkingHours_RefID).ToList();
            var dbClinicWorkingHours = allDbWorkingHours.Where(x => dbWorkingHoursId.Any(s => s == x.Id)).ToList();

            var dbPatient = _appDbContext.Users.FirstOrDefault(x => !x.IsDeleted && appointmentSave.Patient_RefID == x.Id && x.Role == "User");
            var dbResponsiblePerson = _appDbContext.Users.FirstOrDefault(x => !x.IsDeleted && appointmentSave.ResponsiblePerson_RefID == x.Id && x.Role == "Employee" && !x.IsAdminCenter);
            var patient2Questionnaire = _appDbContext.Patient2Questionnaires.Where(x => x.Patient_RefId == dbPatient.Id).ToList();
            //var questionnaires = _appDbContext.Questionnaire.Where(x => patient2Questionnaire.Any(s => s.Questionnaire_RefId == x.Id)).ToList();

            if (dbClinic == null)
            {
                throw new KeyNotFoundException("Clinic does not exist");
            }
            if (dbResponsiblePerson == null)
            {
                throw new Exception("Employee does not exist");
            }
            //if (questionnaires.Any(x => x.ExpireDate > DateTime.Now))
            //{
            //    throw new Exception("Patient have had given blood in last six months");
            //}
            //if (patient2Questionnaire.Any())
            //{
            //    foreach (var item in questionnaires)
            //    {
            //        if (!item.IsQuestionireSigned())
            //        {
            //            throw new Exception("Questionnaire is not filled");
            //        }
            //    }
            //}
            foreach (var item in dbClinicWorkingHours)
            {

                if (DateTime.Parse(appointmentSave.StartTime).Hour >= DateTime.Parse(item.Start).Hour && DateTime.Parse(appointmentSave.StartTime).Hour <= DateTime.Parse(item.End).Hour)
                {
                    if (dbAppointmentsInDay.Any(x => x.StartDate.Hour == DateTime.Parse(item.Start).Hour &&
                                                     x.StartDate.Minute == DateTime.Parse(item.Start).Minute))
                    {
                        throw new Exception("Time of appointment is taken");
                    }

                    dbAppointment.ResponsiblePerson_RefID = dbResponsiblePerson.Id;

                    if (dbPatient == null)
                    {
                        throw new Exception("Patient does not exist");
                    }
                    dbAppointment.Patient_RefID = dbPatient.Id;
                    //if (!patient2Questionnaire.Any() && !questionnaires.Any(x => x.ExpireDate < DateTime.UtcNow))
                    //{
                    //    throw new Exception("Patient does not have any valid questionnaires");
                    //}
                    if (dbAppointment == null)
                    {
                        dbAppointment = new Appointment();
                    }
                    dbAppointment.StartDate = DateTime.Parse(appointmentSave.StartTime).Add(TimeSpan.Parse(appointmentSave.StartTime));
                    dbAppointment.Patient_RefID = appointmentSave.Patient_RefID;
                    dbAppointment.Clinic_RefID = appointmentSave.Clinic_RefID;
                    dbAppointment.ResponsiblePerson_RefID = appointmentSave.ResponsiblePerson_RefID;
                    dbAppointment.IsCanceled = appointmentSave.IsCanceled;
                    dbAppointment.IsFinished = appointmentSave.IsFinished;

                    var qrCode = IronBarCode.QRCodeWriter.CreateQrCode("appointment", 500);
                }
                else
                {
                    throw new Exception("Appointment is set to non-working hours");
                }
            }

            return dbAppointment;
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
            if (dbAppointment == null)
            {
                throw new KeyNotFoundException("Appointment does not exist");
            }
            var result = new AppointmentLoadModel();
            result.Id = Id;
            result.Title = dbAppointment.Title;
            result.StartDate = dbAppointment.StartDate;
            result.Clinic = _clinicIntegration.GetClinicById(Id);
            result.ResponsiblePerson = _userIntegration.GetUserById(dbAppointment.ResponsiblePerson_RefID.Value);
            result.Patient = _userIntegration.GetUserById(dbAppointment.Patient_RefID.Value);

            return result;
        }

        //public List<Appointment> SavePredefinedAppointmets(List<AppointmentSaveModel> appointments)
        //{
        //    var dbAppointments = _appDbContext.Appointments.Where(x => appointments.Any(s => s.Id == x.Id)).ToList();
        //    if(dbAppointments.)
        //}

        public List<Appointment> CreatePredefiendAppointments(SavePredefiendAppointment predefiendAppointment)
        {
            var dbAppointments = _appDbContext.Appointments.Where(x => !x.IsDeleted && x.IsPredefiend && predefiendAppointment.Date == x.StartDate).ToList();
            var result = new List<Appointment>();
            if (dbAppointments != null && dbAppointments.Any())
            {
                throw new Exception("Predefiend appointments are already made");
            }
            else
            {
                for (int i = 0; i < predefiendAppointment.NumberOfAppointmentsInDay; i++)
                {
                    var predefAppointmnet = new Appointment
                    {
                        Duration = predefiendAppointment.Duration,
                        IsPredefiend = true,
                        StartDate = predefiendAppointment.Date.Value.Add(TimeSpan.Parse(predefiendAppointment.Time) * i)                       

                    };
                    result.Add(predefAppointmnet);
                    _appDbContext.Appointments.Add(predefAppointmnet);
                }
                _appDbContext.SaveChanges();
            }
            return result;

        }

    }

}
