using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCBP.Entities;

namespace NCBP.Data
{
    public class EntitlementData
    {
        public IEnumerable<EntitlementEntity> Read(bool isActive, int staffId)
        {
            IEnumerable<EntitlementEntity> entitlements;
            using(var context = new NCBPEntities())
            {
                entitlements = ParseToEntity(context.Entitlements.Where(x => x.IsActive == isActive && x.StaffId == staffId)).ToList();
            }
            return entitlements;
        }

        public IEnumerable<EntitlementEntity> ParseToEntity(IQueryable<Entitlement> entitlements)
        {
            var entitlementsList = new List<EntitlementEntity>();
            foreach(var item in entitlements)
            {
                entitlementsList.Add(new EntitlementEntity
                {
                    Id=item.Id,
                    StaffId=item.StaffId,
                    PermissionId=item.PermissionId,
                    IsActive=item.IsActive,
                    CreatedBy=item.CreatedBy,
                    CreatedDate=item.CreatedDate
                });
            }
            return entitlementsList;
        }

        public void Create(EntitlementEntity entitlementEntity)
        {
            using(var context = new NCBPEntities())
            {
                Entitlement entitlement = new Entitlement()
                {
                    PermissionId=entitlementEntity.PermissionId,
                    StaffId=entitlementEntity.StaffId,
                    IsActive=entitlementEntity.IsActive,
                    CreatedBy=entitlementEntity.CreatedBy,
                    CreatedDate=entitlementEntity.CreatedDate
                };

                context.Entitlements.Add(entitlement);
                context.SaveChanges();
            }
        }

        public void Update(EntitlementEntity entitlementEntity)
        {
            using(var context =new NCBPEntities())
            {
                var info = context.Entitlements.Single(x => x.Id == entitlementEntity.Id);
                info.IsActive = entitlementEntity.IsActive;
                info.UpdatedBy = entitlementEntity.UpdatedBy;
                info.UpdatedDate = entitlementEntity.UpdatedDate;
                context.SaveChanges();
            }
        }
    }
}
