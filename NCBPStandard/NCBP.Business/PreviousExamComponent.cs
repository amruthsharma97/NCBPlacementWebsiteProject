using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCBP.Entities;
using NCBP.Data;

namespace NCBP.Business
{
    public class PreviousExamComponent
    {
        public void BulkInsert(List<PreviousExamEntity> previousExams)
        {
            PreviousExamData previousExamData = new PreviousExamData();
            foreach(var item in previousExams)
            {
                previousExamData.Create(item);
            }
        }

        public void BulkUpdate(List<PreviousExamEntity> previousExams)
        {
            PreviousExamData previousExamData = new PreviousExamData();
            foreach (var item in previousExams)
            {
                previousExamData.Update(item);
            }
        }
    }
}
