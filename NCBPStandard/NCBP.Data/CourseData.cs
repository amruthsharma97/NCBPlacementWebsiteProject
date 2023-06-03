using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCBP.Entities;

namespace NCBP.Data
{
    public class CourseData
    {
        public IEnumerable<CourseEntity> Read(bool isActive)
        {
            var List = new List<CourseEntity>();
            using (var context = new NCBPEntities())
            {
                foreach (var item in context.Courses.Where(x => x.IsActive == isActive))
                {
                    List.Add(new CourseEntity
                    {
                        Id = item.Id,
                        Name = item.Name,
                        IsActive = true,
                    });
                }
            }
            return List;
        }

        public void Create(CourseEntity Course)
        {
            using (var context = new NCBPEntities())
            {
                Course info = new Course
                {
                    Name = Course.Name,
                    IsActive = true
                };
                context.Courses.Add(info);
                context.SaveChanges();
            }
        }

        public void Update(CourseEntity Course)
        {
            using (var context = new NCBPEntities())
            {
                var info = context.Courses.Single(x => x.Id == Course.Id);
                info.Name = Course.Name;
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = new NCBPEntities())
            {
                var course = context.Courses.Single(x => x.Id == id);
                course.IsActive = false; ;
                context.SaveChanges();
            }
        }

        public void UndoDelete(int id)
        {
            using (var context = new NCBPEntities())
            {
                var course = context.Courses.Single(x => x.Id == id);
                course.IsActive = true;
                context.SaveChanges();
            }
        }

    }
}
