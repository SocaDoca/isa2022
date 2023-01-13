using MedicApp.Integrations;
using MedicApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedicApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkingHoursController : ControllerBase
    {
        public readonly IWorkingHoursIntegration _workingHoursIntegration;

        public WorkingHoursController(IWorkingHoursIntegration workingHoursIntegration)
        {
            _workingHoursIntegration = workingHoursIntegration;
        }

        [HttpPost("save-working-hours")]
        public WorkingHours SaveWorkingHours(SaveWorkingHoursModel saveWorkingHours)
        {
            return _workingHoursIntegration.SaveWorkingHours(saveWorkingHours);
        }

        [HttpGet]
        public WorkingHours LoadById(Guid Id)
        {
            return _workingHoursIntegration.LoadDBWorkingHourById(Id);
        }
    }
}
