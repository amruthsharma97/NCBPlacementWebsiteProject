using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCBP.Entities;

namespace NCBP.Data
{
    public class SubjectData
    {
        public IEnumerable<SubjectEntity> Read(bool isActive)
        {
            var List = new List<SubjectEntity>();
            using (var context = new NCBPEntities())
            {
                foreach (var item in context.Subjects.Where(x => x.IsActive == isActive))
                {
                    List.Add(new SubjectEntity
                    {
                        Id = item.Id,
                        Name = item.Name,
                        CourseId = item.CourseId,
                        IsActive = true,
                    });
                }
            }
            return List;
        }

        public void Create(SubjectEntity subject)
        {
            using (var context = new NCBPEntities())
            {
                Subject info = new Subject
                {
                    Name = subject.Name,
                    CourseId = subject.CourseId,
                    IsActive = true
                };
                context.Subjects.Add(info);
                context.SaveChanges();
            }
        }

        public void Update(SubjectEntity subject)
        {
            using (var context = new NCBPEntities())
            {
                var info = context.Subjects.Single(x => x.Id == subject.Id);
                info.Name = subject.Name;
                info.CourseId = subject.CourseId;
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = new NCBPEntities())
            {
                var Subject = context.Subjects.Single(x => x.Id == id);
                Subject.IsActive = false; ;
                context.SaveChanges();
            }
        }

        public void UndoDelete(int id)
        {
            using (var context = new NCBPEntities())
            {
                var Subject = context.Subjects.Single(x => x.Id == id);
                Subject.IsActive = true;
                context.SaveChanges();
            }
        }


    }
}
