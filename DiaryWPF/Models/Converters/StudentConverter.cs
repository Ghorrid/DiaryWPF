using DiaryWPF.Models.Domains;
using DiaryWPF.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryWPF.Models.Converters
{
    // konwertuje klase domenowa student do klasy student wrapper
    public  static class StudentConverter
    {
        public static StudentWrapper ToWrapper(this Student model)
        {
            return new StudentWrapper
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Comments = model.Comments,
                Acitivities = model.Acitivities,
                Group = new GroupWrapper
                {
                    Id = model.Group.Id,
                    Name = model.Group.Name
                },
                Math = String.Join(", ",model.Ratings.Where(t => t.SubjectId == (int)Subject.Math)
                .Select(t => t.Rate)),
                Physics = String.Join(", ", model.Ratings.Where(t => t.SubjectId == (int)Subject.Physics)
                .Select(t => t.Rate)),
                PolishLang = String.Join(", ", model.Ratings.Where(t => t.SubjectId == (int)Subject.PolishLang)
                .Select(t => t.Rate)),
                Technology = String.Join(", ", model.Ratings.Where(t => t.SubjectId == (int)Subject.Technology)
                .Select(t => t.Rate)),
                ForeignLang = String.Join(", ", model.Ratings.Where(t => t.SubjectId == (int)Subject.ForeignLang)
                .Select(t => t.Rate))

            };

        }

         public static Student ToDao (this StudentWrapper model)
            {
            return new Student
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Comments = model.Comments,
                GroupId = model.Group.Id,
                Acitivities = model.Acitivities,
            };
            }

        public static List<Rating> ToRatingDao (this StudentWrapper model)
        {
            var ratingList = new List<Rating>();

            if (!string.IsNullOrWhiteSpace(model.Math))
            {
                model.Math.Split(',').ToList().ForEach(x => ratingList
                .Add(new Rating
                {
                    Rate = int.Parse(x),
                    StudentId = model.Id,
                    SubjectId = (int)Subject.Math
                }));
            }

            if (!string.IsNullOrWhiteSpace(model.Physics))
            {
                model.Physics.Split(',').ToList().ForEach(x => ratingList
                .Add(new Rating
                {
                    Rate = int.Parse(x),
                    StudentId = model.Id,
                    SubjectId = (int)Subject.Physics
                }));
            }

            if (!string.IsNullOrWhiteSpace(model.Technology))
            {
                model.Technology.Split(',').ToList().ForEach(x => ratingList
                .Add(new Rating
                {
                    Rate = int.Parse(x),
                    StudentId = model.Id,
                    SubjectId = (int)Subject.Technology
                }));
            }
            if (!string.IsNullOrWhiteSpace(model.PolishLang))
            {
                model.PolishLang.Split(',').ToList().ForEach(x => ratingList
                .Add(new Rating
                {
                    Rate = int.Parse(x),
                    StudentId = model.Id,
                    SubjectId = (int)Subject.PolishLang
                }));
            }
            if (!string.IsNullOrWhiteSpace(model.ForeignLang))
            {
                model.ForeignLang.Split(',').ToList().ForEach(x => ratingList
                .Add(new Rating
                {
                    Rate = int.Parse(x),
                    StudentId = model.Id,
                    SubjectId = (int)Subject.ForeignLang
                }));
            }
            return ratingList;

        }
    }
}
