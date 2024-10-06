using EzTabs.Model;
using EzTabs.Model.Model;
using EzTabs.Model.Repository;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace EzTabs.View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Check if the app is launched with a token as a command-line argument
            if (e.Args.Length > 0 && e.Args[0].Contains("token"))
            {
                string token = e.Args[0].Split('=')[1]; // Extract token from the URL
                HandleEmailVerification(token);
            }
        }

        private async Task HandleEmailVerification(string token)
        {
            var repo = new RepoImplementation<User>(new EzTabsContext());
            // Logic to verify the token from your local database or cache
            List<User> users = repo.GetAll();

            if (user != null && user.TokenExpiration > DateTime.UtcNow)
            {
                user.IsEmailVerified = true;
                _dbContext.SaveChanges();
                MessageBox.Show("Email successfully verified!");
            }
            else
            {
                MessageBox.Show("Invalid or expired token.");
            }
        }
    }
}
