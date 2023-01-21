
namespace MedicApp.Models
{
    public class User2Token
    {
        public Guid Id { get; set; }
        public string TemporaryToken { get; set; }
        public Guid User_RefId { get; set; }
        public string Token_Ref { get; set; }
        public bool IsValid { get; set; }

        public User2Token()
        {
            Id = Guid.NewGuid();
            IsValid = false;
        }
        
    }
}
