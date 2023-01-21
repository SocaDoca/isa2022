using System.Net.Mail;
using System.Net;

namespace MedicApp.Utils
{
    public interface IEmailUtils
    {
        void SendMail(string sBody, string sSub, string sTo, string sFrom);
    }
    public class EmailUtils : IEmailUtils
    {
        public void SendMail(string sBody, string sSub, string sTo, string sFrom)
        {
            //Send email via SMTP.
            SmtpClient Client = new SmtpClient()
            {
                //Using GMail SMTP
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    UserName = "isa22.test@gmail.com", //Returns valid Gmail address.
                    Password = "icazpzvhbvmtiunh" //Password to access email above. 
                }
            };

            MailAddress FromeMail = new MailAddress(sFrom, "From");
            MailAddress ToeMail = new MailAddress(sTo, "To");

            MailMessage Message = new MailMessage()
            {
                From = FromeMail,
                Subject = sSub,
                Body = sBody
            };

            Message.To.Add(ToeMail);

            try
            {
                Client.Send(Message);
                
            }
            catch (Exception ex)
            {
                throw new Exception("Mail not sent");
            }
        }
    }
}
