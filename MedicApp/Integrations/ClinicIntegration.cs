using MedicApp.Database;
using MedicApp.Models;
using static MedicApp.Models.DbClinic;

namespace MedicApp.Integrations
{
    public class ClinicIntegration
    {
        private readonly AppDbContext _appDbContext;
        public ClinicIntegration(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

      

        
    }
}
