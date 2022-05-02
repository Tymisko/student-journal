using Diary.Models.Domains;
using Diary.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Models.Converters
{
    public static class StudentConverter
    {
        public static StudentWrapper ToWrapper(this Student model)
        {
            return new StudentWrapper
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Comments = model.Comments,
                Activities = model.Activities,
                Group = new GroupWrapper
                {
                    Id = model.Group.Id,
                    Name = model.Group.Name,
                },

                Math = string.Join(", ", model.Ratings
                    .Where(r => r.SubjectId == (int)Subject.Math)
                    .Select(r => r.Rate)),

                Physics = string.Join(", ", model.Ratings
                    .Where(r => r.SubjectId == (int)Subject.Physics)
                    .Select(r => r.Rate)),

                Technology = string.Join(", ", model.Ratings
                    .Where(r => r.SubjectId == (int)Subject.Technology)
                    .Select(r => r.Rate)),

                PolishLang = string.Join(", ", model.Ratings
                    .Where(r => r.SubjectId == (int)Subject.PolishLang)
                    .Select(r => r.Rate)),

                ForeignLang = string.Join(", ", model.Ratings
                    .Where(r => r.SubjectId == (int)Subject.ForeignLang)
                    .Select(r => r.Rate))
            };
        }

        public static Student ToDAO(this StudentWrapper model)
        {
            return new Student
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Comments = model.Comments,
                Activities = model.Activities,
                GroupId = model.Group.Id
            };
        }

        public static List<Rating> ToRatingDAO(this StudentWrapper model)
        {
            var ratings = new List<Rating>();

            if(!string.IsNullOrWhiteSpace(model.Math))
            {
                model.Math.Split(',').ToList().ForEach(r => 
                    ratings.Add(
                        new Rating
                        {
                            Rate = int.Parse(r),
                            StudentId = model.Id,
                            SubjectId = (int)Subject.Math
                        }));
            }

            if (!string.IsNullOrWhiteSpace(model.Physics))
            {
                model.Physics.Split(',').ToList().ForEach(r =>
                    ratings.Add(
                        new Rating
                        {
                            Rate = int.Parse(r),
                            StudentId = model.Id,
                            SubjectId = (int)Subject.Physics
                        }));
            }

            if (!string.IsNullOrWhiteSpace(model.Technology))
            {
                model.Technology.Split(',').ToList().ForEach(r =>
                    ratings.Add(
                        new Rating
                        {
                            Rate = int.Parse(r),
                            StudentId = model.Id,
                            SubjectId = (int)Subject.Technology
                        }));
            }

            if (!string.IsNullOrWhiteSpace(model.PolishLang))
            {
                model.PolishLang.Split(',').ToList().ForEach(r =>
                    ratings.Add(
                        new Rating
                        {
                            Rate = int.Parse(r),
                            StudentId = model.Id,
                            SubjectId = (int)Subject.PolishLang
                        }));
            }

            if (!string.IsNullOrWhiteSpace(model.ForeignLang))
            {
                model.ForeignLang.Split(',').ToList().ForEach(r =>
                    ratings.Add(
                        new Rating
                        {
                            Rate = int.Parse(r),
                            StudentId = model.Id,
                            SubjectId = (int)Subject.ForeignLang
                        }));
            }


            return ratings;
        }
    }
}
