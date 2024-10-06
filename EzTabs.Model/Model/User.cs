using EzTabs.Model.Model.BaseClasses;
using EzTabs.Model.Model.Enums;
using EzTabs.Model.Repository;
using EzTabs.Model.EmailService;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace EzTabs.Model.Model
{
    public class User : Entity
    {
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public bool IsEmailVerified { get; set; } = false; 
        public Guid VerificationToken { get; set; }
        public DateTime? TokenExpiration { get; set; } 
        public UserRole Role { get; private set; }
        public List<TabReport>? TabReports { get; set; }
        public List<TabRate>? TabRates { get; set; }
        public List<FavouriteTab>? FavouriteTabs { get; set; }
        public List<CommentReport>? CommentReports { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<CommentRate>? CommentRates { get; set; }
        public List<Tab>? Tab { get; set; }
        public List<Notification>? Notifications { get; set; }

        public void ChangeRole(User user, UserRole role)
        {
            if (user.Role != UserRole.Admin) return;
            this.Role = role;
        }

        public Guid GenerateVerificationToken()
        {
            return Guid.NewGuid();
        }

        public async Task RegisterUser(RepoImplementation<User> userRepository)
        {
            var verifToken = this.VerificationToken = GenerateVerificationToken();
            this.TokenExpiration = DateTime.UtcNow.AddHours(24);
            await userRepository.Add(this);

            string verificationLink = $"yourapp://verify?token={verifToken}";

            // Send verification email
            EmailSender.SendVerificationEmail(this.Email, verificationLink);
        }
    }
}
