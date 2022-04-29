using System.Data.Entity.ModelConfiguration;
using Diary.Models.Domains;

namespace Diary.Models.Configurations
{
    internal class StudentConfiguration : EntityTypeConfiguration<Student>
    {
        public StudentConfiguration()
        {
            ToTable("dbo.Students");
            HasKey(s => s.Id);

            Property(s => s.FirstName)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
