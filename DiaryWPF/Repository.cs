using DiaryWPF.Models.Domains;
using DiaryWPF.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DiaryWPF.Models.Converters;
using DiaryWPF.Models;

namespace DiaryWPF
{
    public class Repository
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
            using (var context =  new ApplicationDbContext())
            {
                var students = context.Students
                    .Include(x => x.Group)
                    .Include(y => y.Ratings).AsQueryable();

                if (groupId != 0)
                  students =  students.Where(x => x.GroupId == groupId);

                return students.ToList().Select(x => x.ToWrapper()).ToList();

                

            }
        }

        public void DeleteStudent(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var studentToDelete = context.Students.Find(id);
                if (studentToDelete != null) context.Students.Remove(studentToDelete);
                context.SaveChanges();
            }
        }

        public void UpdateStudent(StudentWrapper studentWrapper)
        {
            var student = studentWrapper.ToDao();
            var ratings = studentWrapper.ToRatingDao();

            using (var context = new ApplicationDbContext())
            {
                UpdateStudentProperties(student, context);
                List<Rating> studentRatingsOld = GetStudentRatings(student, context);

                UpdateRate(student, ratings, context, studentRatingsOld, Subject.Math);
                UpdateRate(student, ratings, context, studentRatingsOld, Subject.Physics);
                UpdateRate(student, ratings, context, studentRatingsOld, Subject.Technology);
                UpdateRate(student, ratings, context, studentRatingsOld, Subject.ForeignLang);
                UpdateRate(student, ratings, context, studentRatingsOld, Subject.PolishLang);

                context.SaveChanges();
            }
        }

        private static List<Rating> GetStudentRatings(Student student, ApplicationDbContext context)
        {
            return context.Ratings.Where(x => x.StudentId == student.Id).ToList();
        }

        private static void UpdateStudentProperties(Student student, ApplicationDbContext context)
        {
            var studentToUpdate = context.Students.Find(student.Id);

            studentToUpdate.Acitivities = student.Acitivities;
            studentToUpdate.Comments = student.Comments;
            studentToUpdate.FirstName = student.FirstName;
            studentToUpdate.LastName = student.LastName;
            studentToUpdate.GroupId = student.GroupId;
        }

        private static void UpdateRate(Student student, List<Rating> newRatings, ApplicationDbContext context, List<Rating> studentRatingsOld, Subject subject)
        {
            var subRatings = studentRatingsOld.Where(x => x.SubjectId == (int)subject)
                .Select(x => x.Rate);

            var newSubRatings = newRatings.Where(x => x.SubjectId == (int)subject)
                .Select(x => x.Rate);

            var subRatingsToDelete = subRatings.Except(newSubRatings).ToList();
            var subRatingsToAdd = newSubRatings.Except(subRatings).ToList();

            subRatingsToDelete.ForEach(x =>
            {
                var ratingToDelete = context.Ratings.First
                (y => y.Rate == x &&
                y.StudentId == student.Id &&
                y.SubjectId == (int)subject);

                context.Ratings.Remove(ratingToDelete);
            });

            subRatingsToAdd.ForEach(x =>
            {
                var ratingToAdd = new Rating
                {
                    Rate = x,
                    SubjectId = (int)subject,
                    StudentId = student.Id,
                };
                context.Ratings.Add(ratingToAdd);
            });
        }

        public void AddStudent(StudentWrapper studentWrapper)
        {
            var student = studentWrapper.ToDao();
            var ratings = studentWrapper.ToRatingDao();

            using (var context = new ApplicationDbContext())
            {
                var dbStudent = context.Students.Add(student);
                ratings.ForEach(x =>
                {
                    x.StudentId = dbStudent.Id;
                    context.Ratings.Add(x);
                });
                context.SaveChanges();
            }
        }
    }
}
