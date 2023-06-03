using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCBP.Entities;
using NCBP.Data;

namespace NCBP.Business
{
    public class AppraisalFeedbackComponent
    {
        public int GetTotalAppraisalFeedbackCount(AppraisalCountEntity appraisalCount)
        {
            AppraisalFeedbackData appraisalFeedbackData = new AppraisalFeedbackData();
            int count = appraisalFeedbackData.Read(appraisalCount);
            return 0;
        }

        public int GetAppraisalFeedbackCount(AppraisalFeedbackEntity appraisalFeedback)
        {
            AppraisalFeedbackData appraisalFeedbackData = new AppraisalFeedbackData();
            int count = appraisalFeedbackData.Read(appraisalFeedback);
            return count;
        }

        public void BulkInsert(IEnumerable<AppraisalFeedbackEntity> appraisalFeedbacks)
        {
            AppraisalFeedbackData appraisalFeedbackData = new AppraisalFeedbackData();
            
            foreach(var item in appraisalFeedbacks)
            {
                appraisalFeedbackData.Create(item);
            }
            
        }
    }
}
