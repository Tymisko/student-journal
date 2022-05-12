using System;
using System.Data.Entity;
using System.Linq;
using Diary.Models.Configurations;
using Diary.Models.Domains;
using StudentJournal.Properties;

namespace Diary
{
    public class ApplicationDbContext : DbContext
    {
        private static readonly string _connectionString = $@"
            Server={Settings.Default.DatabaseServerAddress}\{Settings.Default.DatabaseServerName};
            Database={Settings.Default.DatabaseName};
            User Id={Settings.Default.DatabaseUsername};
            Password={Settings.Default.DatabasePassword};";

        public ApplicationDbContext()
            : base(_connectionString)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new StudentConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());
            modelBuilder.Configurations.Add(new RatingConfiguration());
        }
    }
}