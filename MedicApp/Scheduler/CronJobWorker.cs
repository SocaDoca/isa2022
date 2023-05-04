using MedicApp.Integrations;
using Quartz;

namespace MedicApp.Scheduler
{
    public class CronJobWorker : IJob
    {
        private readonly IUserIntegration _userIntegration;
        CronJobWorker(IUserIntegration userIntegration)
        {
            _userIntegration = userIntegration;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _userIntegration.RemovePenalty();
            return Task.FromResult(true);
        }
    }
}
