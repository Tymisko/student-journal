using System.Data.Entity.ModelConfiguration;
using Diary.Models.Domains;

namespace Diary.Models.Configurations
{
    internal class RatingConfiguration : EntityTypeConfiguration<Rating>
    {
        public RatingConfiguration()
        {
            ToTable("dbo.Ratings");
            HasKey(r => r.Id);
        }
    }
}
