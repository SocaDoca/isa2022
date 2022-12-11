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

        public async Task<Guid> CreateAccount(RegisterModel account)
        {
            
        }



        
    }

    public class RegisterModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsPasswordChanged { get; set; } 
        public 
    }
}
