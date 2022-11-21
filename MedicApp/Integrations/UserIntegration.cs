using MedicApp.Database;

namespace MedicApp.Integrations
{
    public interface IUserIntegration
    {

    }
    public class UserIntegration
    {
        private readonly AppDbContext _appDbContext;
        public UserIntegration(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;   
        }

        //public async Task<>
        
    }
}
