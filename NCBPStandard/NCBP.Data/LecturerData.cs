using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCBP.Entities;

namespace NCBP.Data
{
    public class LecturerData
    {
        public IEnumerable<LecturerEntity> Read(bool isActive)
        {
            var List = new List<LecturerEntity>();
            using (var context = new NCBPEntities())
            {
                foreach (var item in context.Lecturers.Where(x => x.IsActive == isActive))
                {
                    List.Add(new LecturerEntity
                    {
                        Id = item.Id,
                        Name = item.Name,
                        DepartmentId=item.DepartmentId,
                        IsActive = true,
                    });
                }
            }
            return List;
        }

        public void Create(LecturerEntity lecturer)
        {
            using (var context = new NCBPEntities())
            {
                Lecturer info = new Lecturer
                {
                    Name = lecturer.Name,
                    DepartmentId=lecturer.DepartmentId,
                    IsActive = true
                };
                context.Lecturers.Add(info);
                context.SaveChanges();
            }
        }

        public void Update(LecturerEntity lecturer)
        {
            using (var context = new NCBPEntities())
            {
                var info = context.Lecturers.Single(x => x.Id == lecturer.Id);
                info.Name = lecturer.Name;
                info.DepartmentId = lecturer.DepartmentId;
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = new NCBPEntities())
            {
                var lecturer = context.Lecturers.Single(x => x.Id == id);
                lecturer.IsActive = false; ;
                context.SaveChanges();
            }
        }

        public void UndoDelete(int id)
        {
            using (var context = new NCBPEntities())
            {
                var lecturer = context.Lecturers.Single(x => x.Id == id);
                lecturer.IsActive = true;
                context.SaveChanges();
            }
        }

    }
}
