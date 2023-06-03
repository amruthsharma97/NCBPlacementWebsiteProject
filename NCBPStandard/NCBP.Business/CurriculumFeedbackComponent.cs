using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCBP.Entities;
using NCBP.Data;

namespace NCBP.Business
{
    public class CurriculumFeedbackComponent
    {
        public int GetTotalCurriculumFeedbackCount(CurriculumCountEntity CurriculumCount)
        {
            CurriculumFeedbackData CurriculumFeedbackData = new CurriculumFeedbackData();
            int count = CurriculumFeedbackData.Read(CurriculumCount);
            return 0;
        }

        public int GetCurriculumFeedbackCount(CurriculumFeedbackEntity CurriculumFeedback)
        {
            CurriculumFeedbackData CurriculumFeedbackData = new CurriculumFeedbackData();
            int count = CurriculumFeedbackData.Read(CurriculumFeedback);
            return count;
        }

        public void BulkInsert(IEnumerable<CurriculumFeedbackEntity> CurriculumFeedbacks)
        {
            CurriculumFeedbackData CurriculumFeedbackData = new CurriculumFeedbackData();

            foreach (var item in CurriculumFeedbacks)
            {
                CurriculumFeedbackData.Create(item);
            }

        }

    }
}
