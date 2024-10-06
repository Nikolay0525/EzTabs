using System.Net.Mail;
using System.Net;

namespace EzTabs.Model.EmailService
{
    public class EmailSender
    {
        public static void SendVerificationEmail(string recipientEmail, string verificationLink)
        {
            // Set up the Gmail SMTP client
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587, // Use port 587 for TLS/SSL
                Credentials = new NetworkCredential("eztabsverif@gmail.com", "TyYvuzkJamZ2"), // Replace with your Gmail password or App Password if 2FA is enabled
                EnableSsl = true, // Secure connection using SSL
            };

            // Create the email message
            var mailMessage = new MailMessage
            {
                From = new MailAddress("eztabsverif@gmail.com"), // Sender's email
                Subject = "Email Verification", // Subject of the email
                Body = $"<h1>Welcome to EZTabs!</h1><p>Please verify your email by clicking the link below:</p><a href='{verificationLink}'>Verify Email</a>", // Email body (HTML)
                IsBodyHtml = true, // Enable HTML in the email body
            };
            mailMessage.To.Add(recipientEmail); // Add the recipient's email address

            // Send the email
            try
            {
                smtpClient.Send(mailMessage);
                Console.WriteLine("Verification email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }
    }
}