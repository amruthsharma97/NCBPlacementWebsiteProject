using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCBP.Entities;

namespace NCBP.Data
{
    public class PermissionData
    {
        public IEnumerable<PermissionEntity> Read(bool isActive)
        {
            var List = new List<PermissionEntity>();
            using (var context = new NCBPEntities())
            {
                foreach (var item in context.Permissions.Where(x => x.IsActive == isActive))
                {
                    List.Add(new PermissionEntity
                    {
                        Id = item.Id,
                        Name = item.Name,
                        IsActive = true,
                    });
                }
            }
            return List;
        }

        public void Create(PermissionEntity permission)
        {
            using (var context = new NCBPEntities())
            {
                Permission info = new Permission
                {
                    Name = permission.Name,
                    IsActive = true
                };
                context.Permissions.Add(info);
                context.SaveChanges();
            }
        }

        public void Update(PermissionEntity permission)
        {
            using (var context = new NCBPEntities())
            {
                var info = context.Permissions.Single(x => x.Id == permission.Id);
                info.Name = permission.Name;
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = new NCBPEntities())
            {
                var language = context.Permissions.Single(x => x.Id == id);
                language.IsActive = false; ;
                context.SaveChanges();
            }
        }

        public void UndoDelete(int id)
        {
            using (var context = new NCBPEntities())
            {
                var staffInfo = context.Permissions.Single(x => x.Id == id);
                staffInfo.IsActive = true;
                context.SaveChanges();
            }
        }
    }
}
