using Diary.Models;
using Diary.Models.Converters;
using Diary.Models.Domains;
using Diary.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;

namespace Diary
{
    class Repository
    {
        public List<Group> GetGroups()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Groups.ToList();
            }
        }

        public List<StudentWrapper> GetStudents(int groupId)
        {
            using (var context = new ApplicationDbContext())
            {
                var students = context.Students
                    .Include(s => s.Group)
                    .Include(s => s.Ratings)
                    .AsQueryable();

                if (groupId != 0)
                {
                    students = students.Where(s => s.GroupId == groupId);
                }

                return students
                    .ToList()
                    .Select(s => s.ToWrapper())
                    .ToList();
            }
        }

        public void DeleteStudent(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var studentsToDelete = context.Students.Find(id);
                context.Students.Remove(studentsToDelete);
                context.SaveChanges();
            }
        }

        public void UpdateStudent(StudentWrapper studentWrapper)
        {
            var student = studentWrapper.ToDAO();
            var ratings = studentWrapper.ToRatingDAO();

            using (var context = new ApplicationDbContext())
            {
                UpdateStudentProperties(student, context);

                List<Rating> studentsRatings = GetStudentsRatings(student, context);

                UpdateRate(student, ratings, context, studentsRatings,
                    Subject.Math);
                UpdateRate(student, ratings, context, studentsRatings,
                    Subject.Physics);
                UpdateRate(student, ratings, context, studentsRatings,
                    Subject.Technology);
                UpdateRate(student, ratings, context, studentsRatings,
                    Subject.PolishLang);
                UpdateRate(student, ratings, context, studentsRatings,
                    Subject.ForeignLang);

                context.SaveChanges();
            }
        }

        private static List<Rating> GetStudentsRatings(Student student, ApplicationDbContext context)
        {
            return context.Ratings
                .Where(r => r.StudentId == student.Id)
                .ToList();
        }

        private static void UpdateStudentProperties(Student student, ApplicationDbContext context)
        {
            var studentToUpdate = context.Students.Find(student.Id);
            studentToUpdate.FirstName = student.FirstName;
            studentToUpdate.LastName = student.LastName;
            studentToUpdate.Activities = student.Activities;
            studentToUpdate.Comments = student.Comments;
            studentToUpdate.GroupId = student.GroupId;
        }

        private static void UpdateRate(Student student,
                                       List<Rating> ratings,
                                       ApplicationDbContext context,
                                       List<Rating> studentsRatings,
                                       Subject subject)
        {
            var subjectRatings = studentsRatings
                    .Where(r => r.SubjectId == (int)subject)
                    .Select(r => r.Rate);

            var newsubjectRatings = ratings
                .Where(r => r.SubjectId == (int)subject)
                .Select(r => r.Rate);

            var subjectRatingsToDelete = subjectRatings.Except(newsubjectRatings).ToList();
            var subjectRatingsToAdd = newsubjectRatings.Except(subjectRatings).ToList();

            subjectRatingsToDelete.ForEach(mr =>
            {
                var ratingToDelete = context.Ratings.First(r =>
                r.Rate == mr
                && r.Student.Id == student.Id
                && r.SubjectId == (int)subject);

                context.Ratings.Remove(ratingToDelete);
            });

            subjectRatingsToAdd.ForEach(mr =>
            {
                var ratingToAdd = new Rating
                {
                    Rate = mr,
                    StudentId = student.Id,
                    SubjectId = (int)subject,
                };

                context.Ratings.Add(ratingToAdd);
            });
        }

        public void AddStudent(StudentWrapper studentWrapper)
        {
            var student = studentWrapper.ToDAO();
            var ratings = studentWrapper.ToRatingDAO();

            using (var context = new ApplicationDbContext())
            {
                var dbStudent = context.Students.Add(student);

                ratings.ForEach(r =>
                {
                    r.StudentId = dbStudent.Id;
                    context.Ratings.Add(r);
                });

                context.SaveChanges();
            }
        }
    }
}
