
namespace MedicApp.Models
{
    public class AppSettingsModel
    {
        public string AllowedHosts { get; set; }
        public AppSettingsModel_MySQLConfig ConnectionStrings { get; set; }
        public AppSettingsModel_SecretSettings SecretSettings { get; set; }
        public AppSettingsModel_EmailSettings EmailSettings { get; set; }
        public AppSettingsModel_JwtToken JwtToken { get; set; }
    }

    public class AppSettingsModel_MySQLConfig
    {
        public string MySqlConnection { get; set; }
    }
    public class AppSettingsModel_SecretSettings
    {
        public string Secret { get; set; }
    }

    public class AppSettingsModel_EmailSettings
    {
        public string MailServer { get; set;}
        public bool UseTls { get; set; }
        public string senderAddress { get; set;}
        public string senderName { get; set;}
    }

    public class AppSettingsModel_JwtToken
    {
        public string Issuer { get; set;}
        public string Audience { get; set; }
        public string SigningKey { get; set;}
        public int TokenTimeoutMinutes { get; set;}
    }
}
