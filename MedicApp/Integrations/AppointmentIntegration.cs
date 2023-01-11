using MedicApp.Database;
using MedicApp.Models;
using MedicApp.RelationshipTables;

namespace MedicApp.Integrations
{
    public class AppointmentIntegration
    {
        private readonly AppDbContext _appDbContext;

        public AppointmentIntegration(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public Appointment CreateAppointment(AppointmentSaveModel appointmentSave)
        {
            var dbAppointment = _appDbContext.Appointments.Where(x => !x.IsDeleted && x.Id == appointmentSave.Id).FirstOrDefault();
            if (dbAppointment == null)
            {
                dbAppointment = new Appointment();
                
            }

            return dbAppointment;
        } 

        //public async Task<Appointment> CreateAppointmentByPatient(AppointmentByPatientSaveModel appointmentSave)
        //{
        //    var findAppointment = _appDbContext.Appointments.Where(x => x.Id == appointmentSave.Id && !x.IsDeleted).FirstOrDefault();
        //    var findPatient = _appDbContext.Users.Where(x => x.Id == appointmentSave.Patient_RefID &&
        //        !x.IsDeleted).FirstOrDefault();
        //    var findDoctor = _appDbContext.Users.Where(x => x.Id == appointmentSave.Doctor.Id &&
        //        !x.IsDeleted).FirstOrDefault();

        //    if (findAppointment == null)
        //    {
        //        findAppointment = new Appointment()
        //        {
        //            Title = appointmentSave.Title,
        //            StartTime = appointmentSave.StartTime,
        //            PlannedDate = appointmentSave.PlannedDate,
        //            Status = Enums.AppointmentStatus.Planned
        //        };

        //        _appDbContext.Appointments.Add(findAppointment);

        //        var appointment2Patient = new Appointment2Patient()
        //        {
        //            Appointment_RefID = findAppointment.Id,
        //            Patient_RefID = appointmentSave.Patient_RefID
        //        };
        //        _appDbContext.Appointment2Patients.Add(appointment2Patient);

        //        var appointment2Doctor = new Appointment2Doctor()
        //        {
        //            Appointment_RefID = findAppointment.Id,
        //            Doctor_RefID = findDoctor.Id
        //        };

        //        _appDbContext.Appointment2Doctors.Add(appointment2Doctor);

        //        var appointmentHistory = new AppointmentHistory()
        //        {
        //            AppointmentId = findAppointment.Id,
        //            AppointmentStatus = Enums.AppointmentStatus.Planned,
        //            ChangedByUser_RefID = appointmentSave.Patient_RefID,
        //        };

        //        _appDbContext.AppointmentHistories.Add(appointmentHistory);
        //    }

        //    findAppointment.Title = appointmentSave.Title;
        //    findAppointment.StartTime = appointmentSave.StartTime;

        //    _appDbContext.SaveChanges();
        //    return findAppointment;

        //}


        //public async Task<List<Appointment>> CreateAppointmentByCenterAdmin(PredefinedAppointmentByAdmin appointment)
        //{
        //    var findClinic = _appDbContext.Clinics.FirstOrDefault(x => x.Id == appointment.Clinic_RefID && !x.IsDeleted);
        //    if (findClinic == null)
        //    {
        //        throw new KeyNotFoundException("Clinic does not exist");
        //    }
        //    var clinic2WorkingHours = _appDbContext.Clinic2WorkingHours.Where(x => !x.IsDeleted && x.Clinic_RefID == appointment.Clinic_RefID).ToList();

        //    var workingHoursIds = clinic2WorkingHours.Select(x => x.WorkingHours_RefID).ToList();
        //    var clinicWorkingHours = new List<WorkingHours>();

        //    var workingHours = _appDbContext.WorkingHours.Where(x => workingHoursIds.Contains(x.Id)).ToList();

        //    var resultList = new List<Appointment>();
        //    var numOfAppointments = appointment.NumberOfWantedAppointments;
            
        //    for (var item = 0; item <= numOfAppointments; item++)
        //    {
        //        foreach(var workingDay in workingHours)
        //        {
        //            if(appointment.PlannedDate.AddMinutes(item * appointment.Duration).Hour >= workingDay.Start && 
        //                appointment.PlannedDate.AddMinutes(item * appointment.Duration).Hour <= workingDay.End)
        //            {
        //                var model = new Appointment()
        //                {
        //                    Title = String.Format("{0} {1}:{2}", appointment.Title, appointment.StartTime.AddMinutes(item * appointment.Duration).Hour, appointment.StartTime.AddMinutes(item * appointment.Duration).Minute),
        //                    PlannedDate = appointment.PlannedDate,
        //                    StartTime = appointment.StartTime.AddMinutes(item * appointment.Duration), 
        //                    Status = Enums.AppointmentStatus.Planned
        //                };
        //                _appDbContext.Appointments.Add(model);
        //                resultList.Add(model);

        //                var appointment2Clinic = new Appointment2Clinic
        //                {
        //                    Appointment_RefID = model.Id,
        //                    Clinic_RefID = appointment.Clinic_RefID
        //                };

        //                _appDbContext.Appointment2Clinics.Add(appointment2Clinic);
        //            }
        //        }
                
        //    }

        //    _appDbContext.SaveChanges();
        //    return resultList;
        //}
    }

    public class AppointmentSaveModel
    { 
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime StartTime { get; set;}
        public User Doctor { get; set; }
        public User Patient { get; set; }
        public Clinic Clinic { get; set; } // maybe only send gid?
    }

    public class AppointmentLoadModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public User Doctor { get; set; }
        public User Patient { get; set; }
        public Clinic Clinic { get; set; }


    }
}
