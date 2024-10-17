using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using EzTabs.Model;
using Newtonsoft.Json;

namespace EzTabs.Data
{
    public class EzTabsContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Tab> Tabs { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public EzTabsContext() { }
        public struct Connection
        {
            public Version Version { get; set; }
            public string ConnectionString { get; set; }
        };

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string projectDirectory = Directory.GetCurrentDirectory();
            var json = File.ReadAllText(projectDirectory + "/ConnectionString.json");
            var jsonConverted = JsonConvert.DeserializeObject<Connection>(json);
            optionsBuilder.UseMySql(jsonConverted.ConnectionString, new MySqlServerVersion(jsonConverted.Version));
            optionsBuilder.LogTo(message => Debug.WriteLine(message));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tuning>()
                .HasKey(t => new { t.TabId, t.StringOrder });

            modelBuilder.Entity<Tuning>()
                .HasOne(t => t.Tab)
                .WithMany(t => t.Tunings)
                .HasForeignKey(t => t.TabId);

            modelBuilder.Entity<CommentRate>()
                .HasKey(cr => new { cr.CommentId, cr.UserId });

            modelBuilder.Entity<CommentRate>()
                .HasOne(cr => cr.User)
                .WithMany(cr => cr.CommentRates)
                .HasForeignKey(cr => cr.UserId);

            modelBuilder.Entity<CommentRate>()
                .HasOne(cr => cr.Comment)
                .WithMany(cr => cr.CommentRates)
                .HasForeignKey(cr => cr.CommentId);

            modelBuilder.Entity<FavouriteTab>()
                        .HasKey(sc => new { sc.UserId, sc.TabId });

            modelBuilder.Entity<FavouriteTab>()
                .HasOne(sc => sc.User)
                .WithMany(s => s.FavouriteTabs)
                .HasForeignKey(sc => sc.UserId);

            modelBuilder.Entity<FavouriteTab>()
                .HasOne(sc => sc.Tab)
                .WithMany(c => c.FavouriteTabs)
                .HasForeignKey(sc => sc.TabId);

            modelBuilder.Entity<TabRate>()
            .HasKey(sc => new { sc.UserId, sc.TabId });

            modelBuilder.Entity<TabRate>()
                .HasOne(sc => sc.User)
                .WithMany(s => s.TabRates)
                .HasForeignKey(sc => sc.UserId);

            modelBuilder.Entity<TabRate>()
                .HasOne(sc => sc.Tab)
                .WithMany(c => c.TabRates)
                .HasForeignKey(sc => sc.TabId);

            modelBuilder.Entity<TabReport>()
            .HasKey(tr => new { tr.UserId, tr.TabId });

            modelBuilder.Entity<TabReport>()
                .HasOne(tr => tr.User)
                .WithMany(s => s.TabReports)
                .HasForeignKey(sc => sc.UserId);

            modelBuilder.Entity<TabReport>()
                .HasOne(sc => sc.Tab)
                .WithMany(c => c.TabReports)
                .HasForeignKey(sc => sc.TabId);
            
            modelBuilder.Entity<CommentReport>()
            .HasKey(sc => new { sc.UserId, sc.CommentId });

            modelBuilder.Entity<CommentReport>()
                .HasOne(sc => sc.User)
                .WithMany(s => s.CommentReports)
                .HasForeignKey(sc => sc.UserId);

            modelBuilder.Entity<CommentReport>()
                .HasOne(sc => sc.Comment)
                .WithMany(c => c.CommentReports)
                .HasForeignKey(sc => sc.CommentId);
        }
    }
}
