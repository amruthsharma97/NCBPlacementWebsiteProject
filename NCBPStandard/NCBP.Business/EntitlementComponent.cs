using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCBP.Entities;
using NCBP.Data;

namespace NCBP.Business
{
    public class EntitlementComponent
    {
        public void BulkInsert(List<EntitlementEntity> entitlements)
        {
            EntitlementData entitlementData = new EntitlementData();
            foreach(var item in entitlements)
            {
                entitlementData.Create(item);
            }
        }

        public void BulkUpdate(List<EntitlementEntity> entitlements)
        {
            EntitlementData entitlementData = new EntitlementData();
            foreach(var item in entitlements)
            {
                entitlementData.Update(item);
            }
        }
    }
}
