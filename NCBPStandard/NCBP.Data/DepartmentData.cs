using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCBP.Entities;

namespace NCBP.Data
{
    public class DepartmentData
    {
        public IEnumerable<DepartmentEntity> Read(bool isActive)
        {
            var List = new List<DepartmentEntity>();
            using (var context = new NCBPEntities())
            {
                foreach (var item in context.Departments.Where(x => x.IsActive == isActive))
                {
                    List.Add(new DepartmentEntity
                    {
                        Id = item.Id,
                        Name = item.Name,
                        IsActive = true,
                    });
                }
            }
            return List;
        }

        public void Create(DepartmentEntity Department)
        {
            using (var context = new NCBPEntities())
            {
                Department info = new Department
                {
                    Name = Department.Name,
                    IsActive = true
                };
                context.Departments.Add(info);
                context.SaveChanges();
            }
        }

        public void Update(DepartmentEntity Department)
        {
            using (var context = new NCBPEntities())
            {
                var info = context.Departments.Single(x => x.Id == Department.Id);
                info.Name = Department.Name;
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = new NCBPEntities())
            {
                var department = context.Departments.Single(x => x.Id == id);
                department.IsActive = false; ;
                context.SaveChanges();
            }
        }

        public void UndoDelete(int id)
        {
            using (var context = new NCBPEntities())
            {
                var department = context.Departments.Single(x => x.Id == id);
                department.IsActive = true;
                context.SaveChanges();
            }
        }

    }
}
