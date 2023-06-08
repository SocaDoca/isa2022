using MedicApp.Integrations;
using MedicApp.Models;
using MedicApp.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize(Roles = "Admin")]
    public class ComplaintsController : ControllerBase
    {
        private readonly IComplaintIntegration _complaintsIntegration;

        public ComplaintsController(IComplaintIntegration complaintsIntegration)
        {
            _complaintsIntegration = complaintsIntegration;
        }

        [HttpPost("save-complaints")]
        public Complaints SaveComplaints([FromBody] SaveComplaintsRequest parameters)
        {
            return _complaintsIntegration.CreateComplaint(parameters);
        }

        [HttpPost("load-all-complaints")]
        public List<ComplaintListModel> LoadAllComplaints()
        {
            return _complaintsIntegration.LoadAllComplaints();
        }
        [HttpPost("answer-complaint")]
        public bool AnswerComplaint(AnswerCompaintRequest parameters)
        {
            return _complaintsIntegration.AnswerComplaint(parameters);
        }
    }
}
