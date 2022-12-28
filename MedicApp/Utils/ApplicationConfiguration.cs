namespace MedicApp.Utils
{
    public class ApplicationConfiguration
    {

        public string ApplicationName { get; set; } = "Medic App";

        public EmailConfiguration EmailSettings { get; set; } = new EmailConfiguration();

        public TokenConfiguration JwtToken { get; set; } = new TokenConfiguration();
    }

    public class EmailConfiguration
    {
        public string MailServer { get; set; } = "localhost";
        public bool UseTls { get; set; }
        public string MailServerUsername { get; set; }
        public string MailServerPassword { get; set; }

        public string SenderAddress { get; set; } = "admin@west-wind.com";
        public string SenderName { get; set; } = "West Wind Administration";
    }

    public class TokenConfiguration
    {
        public string Issuer { get; set; } = "https://timetrakker.com";
        public string Audience { get; set; } = "https://timetrakker.com";

        public string SigningKey { get; set; } = "4rTGTad3Asdd$123ads*asd3iotgfd#12axads9310#";

        public int TokenTimeoutMinutes { get; set; } = 45;
    }
}

