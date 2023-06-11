using MedicApp.Database;
using MedicApp.Models;
using MedicApp.Models.RequestModels;
using MedicApp.RelationshipTables;
using MedicApp.Utils;
using MedicApp.Utils.AppSettings;
using Microsoft.Extensions.Options;

namespace MedicApp.Integrations
{
    public interface IComplaintIntegration
    {
        Complaints CreateComplaint(SaveComplaintsRequest parameters);
        bool AnswerComplaint(AnswerCompaintRequest parameters);
        List<ComplaintListModel> LoadAllComplaints();
    }
    public class ComplaintIntegration : IComplaintIntegration
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


        public Complaints CreateComplaint(SaveComplaintsRequest parameters)
        {
            var complaint = parameters.Complaint;
            var dbComplaint = _appDbContext.Complaints.FirstOrDefault(x => x.Id == complaint.Id && x.IsDeleted == false);
            var dbPatient = _appDbContext.Users.FirstOrDefault(x => x.Id == parameters.PatientId && x.IsDeleted == false && x.Role == "User");

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
                    IsForClinic_Clinic_RefId = parameters.ClinicId.HasValue ? parameters.ClinicId.Value : null,
                    IsForEmployee_User_RefId = parameters.EmployeeId.HasValue ? parameters.EmployeeId.Value : null,
                    ComplaintBy_User_RefId = dbPatient.Id
                };

                var complaint2Patient = new Complaint2Patient()
                {
                    Complaint_RefId = dbComplaint.Id,
                    Patient_RefId = parameters.PatientId
                };
                _appDbContext.Complaint2Patients.Add(complaint2Patient);
            }
            dbComplaint.Answer = complaint.Answer;
            dbComplaint.UserInput = complaint.UserInput;

            var body = "You have succsefully created complaint";
            _emailUtils.SendMail(body, $"Created complaint by {dbPatient.FirstName} {dbPatient.LastName}", _emailSettings.Value.SenderAddress, dbPatient?.Email ?? string.Empty);
            _appDbContext.Complaints.Add(dbComplaint);
            _appDbContext.SaveChanges();

            return dbComplaint;
        }

        public bool AnswerComplaint(AnswerCompaintRequest parameters)
        {
            var dbComplaint = _appDbContext.Complaints.FirstOrDefault(x => x.Id == parameters.ComplaintId && !x.IsDeleted);
            if (dbComplaint == null) return false;
            var dbPatient = _appDbContext.Users.FirstOrDefault(x => x.Id == dbComplaint.ComplaintBy_User_RefId && !x.IsDeleted);
            dbComplaint.Answer = parameters.Answer;
            _emailUtils.SendMail(parameters.Answer, $"Answer on complaint", dbPatient.Email, _emailSettings.Value.SenderAddress);
            return true;
        }

        public List<ComplaintListModel> LoadAllComplaints()
        {
            var dbComplaints = _appDbContext.Complaints.Where(x => x.IsDeleted == false).ToList();
            var userIds = dbComplaints.Select(x => x.ComplaintBy_User_RefId).ToList();
            var dbUser = _appDbContext.Users.Where(x => userIds.Any(s => s == x.Id)).ToList();
            List<ComplaintListModel> resultList = new List<ComplaintListModel>();

            foreach (var complaint in dbComplaints)
            {
                var user = dbUser.FirstOrDefault(x => x.Id == complaint.ComplaintBy_User_RefId);
                var complaintModel = new ComplaintListModel
                {
                    Id = complaint.Id,
                    IsForClinic = complaint.IsForClinic,
                    IsForEmployee = complaint.IsForEmployee,
                    IsAnswered = complaint.IsAnswered,
                    UserEmail = user.Email,
                    UserName = String.Format("{0} {1}", user.FirstName, user.LastName)
                };
                resultList.Add(complaintModel);
            }
            return resultList;
        }
    }
}
