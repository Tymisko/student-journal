using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Models.Domains
{
    public class Rating
    {
        public Rating()
        {
            Ratings = new Collection<Rating>();
        }

        public int Id { get; set; }
        public int Rate { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }

        public Student Student { get; set; }

        public ICollection<Rating> Ratings { get; set; }
    }
}
