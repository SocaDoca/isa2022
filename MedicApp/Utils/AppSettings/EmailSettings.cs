namespace MedicApp.Utils.AppSettings
{
    public class EmailSettings
    {
        public string MailServer { get; set; }
        public bool useTls { get; set; }
        public string SenderAddress { get; set; } = "isa22.test@gmail.com";
        public string Password { get; set; } = "icazpzvhbvmtiunh";
        public string SenderName { get; set; } = "Milos Rankovic";

    }
}
