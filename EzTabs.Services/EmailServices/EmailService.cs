using System.Net;
using System.Net.Mail;


namespace EzTabs.Services.EmailServices
{
    public static class EmailService
    {
        public static void SendVerificationEmail(string email, string verificationCode)
        {
            string smtpAddress = "smtp.gmail.com";
            int portNumber = 587;
            bool enableSSL = true;

            string emailFrom = "eztabsverif@gmail.com";
            string password = "szov zwks szgx yywd";
            string subject = "Verify Your Email";
            string body = $"Welcome to our community, here's your code: {verificationCode}";

            using MailMessage mail = new MailMessage();
            mail.From = new MailAddress(emailFrom);
            mail.To.Add(email);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            using SmtpClient smtp = new SmtpClient(smtpAddress, portNumber);
            smtp.Credentials = new NetworkCredential(emailFrom, password);
            smtp.EnableSsl = enableSSL;
            smtp.Send(mail);
        }
    }
}
