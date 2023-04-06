using MedicApp.Database;
using MedicApp.Models;
using MedicApp.RelationshipTables;
using MedicApp.Utils;
using MedicApp.Utils.AppSettings;
using Microsoft.Extensions.Options;

namespace MedicApp.Integrations
{
    public interface IComplaintIntegration
    {

    }
    public class ComplaintIntegration
    {
        private readonly AppDbContext _appDbContext;

        private readonly IUserIntegration _userIntegration;
        private readonly IClinicIntegration _clinicIntegration;
        private readonly IEmailUtils _emailUtils;
        public readonly IOptions<EmailSettings> _emailSettings;

        public ComplaintIntegration(AppDbContext appDbContext, IUserIntegration userIntegration, IClinicIntegration clinicIntegration, IOptions<EmailSettings> emailSettings, IEmailUtils emailUtils)
        {
            _appDbContext = appDbContext;
            _userIntegration = userIntegration;
            _clinicIntegration = clinicIntegration;
            _emailUtils = emailUtils;
            _emailSettings = emailSettings;
        }


        public Complaints CreateComplaint(ComplaintSaveModel complaint, Guid patientId)
        {
            var dbComplaint = _appDbContext.Complaints.FirstOrDefault(x => x.Id == complaint.Id && x.IsDeleted == false);
            var dbPatient = _appDbContext.Users.FirstOrDefault(x => x.Id == patientId && x.IsDeleted == false && x.Role == "User");

            if (dbPatient == null)
            {
                throw new Exception("Patient does not exist");
            }

            if (dbComplaint == null)
            {
                dbComplaint = new Complaints
                {
                    Answer = complaint.Answer,
                    UserInput = complaint.UserInput,
                    IsAnswered = complaint.IsAnswered,
                    IsForClinic = complaint.IsForClinic,
                    IsForEmployee = complaint.IsForEmployee,
                };

                var complaint2Patient = new Complaint2Patient()
                {
                    Complaint_RefId = dbComplaint.Id,
                    Patient_RefId = patientId
                };
                _appDbContext.Complaint2Patients.Add(complaint2Patient);
            }
            dbComplaint.Answer = complaint.Answer;
            dbComplaint.UserInput = complaint.UserInput;


            _appDbContext.Complaints.Add(dbComplaint);
            _appDbContext.SaveChanges();

            return dbComplaint;
        }


        public List<Complaints> LoadAllComplaints()
        {
            var dbComplaints = _appDbContext.Complaints.Where(x => x.IsDeleted == false).ToList();
            List<Complaints> resultList = new List<Complaints>();

            foreach (var complaint in dbComplaints)
            {
                var complaintModel = new Complaints
                {
                    Id = complaint.Id,
                    Answer = complaint.Answer,
                    UserInput = complaint.UserInput,
                    IsForClinic = complaint.IsForClinic,
                    IsForEmployee = complaint.IsForEmployee,
                    IsAnswered = complaint.IsAnswered,
                };
                resultList.Add(complaintModel);
            }
            return resultList;
        }
    }
}
