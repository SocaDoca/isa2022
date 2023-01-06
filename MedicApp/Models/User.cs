using MedicApp.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MedicApp.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public Genders Gender { get; set; }
        public int LoyaltyPoints { get; set; }
        public string City { get; set; }
        public string Role { get; set; }
        //public Guid Role_RefID { get; set; }        
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool IsDeleted { get; set; }  
        
        
        public User()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
            LoyaltyPoints = 1000;
        }
    }

    public class LoginModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

    }
    public class LoginResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }

        public String Role { get; set; }
    }
    public class RegisterRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; } 
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Email { get; set; }
        public string? Moblie { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public Genders Gender { get; set; }
       
        public string Roles { get; set; }
        public string Job { get; set; }
    }
    public class UpdateRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Moblie { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string Role { get; set; }
        public string City { get; set; }
        public string Job { get; set; }
    }
}
