using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCBP.Entities;

namespace NCBP.Data
{
    public class AppraisalFeedbackData
    {
        public int Read(AppraisalCountEntity appraisalCount)
        {
            int count;
            using(var context = new NCBPEntities())
            {
                count = context.AppraisalFeedbacks.Where(x => x.LecturerId == appraisalCount.LecturerId && x.QuestionId == appraisalCount.QuestionId && x.Year == appraisalCount.Year).ToList().Count();
            }
            return count;
        }

        public int Read(AppraisalFeedbackEntity appraisalFeedback)
        {
            int count;
            using (var context = new NCBPEntities())
            {
                count = context.AppraisalFeedbacks.Where(x => x.LecturerId == appraisalFeedback.LecturerId && x.QuestionId == appraisalFeedback.QuestionId && x.Feedback == appraisalFeedback.Feedback && x.Year == appraisalFeedback.Year).ToList().Count();
            }
            return count;
        }

        public void Create(AppraisalFeedbackEntity appraisalFeedback)
        {
            using (var context=new NCBPEntities())
            {
                AppraisalFeedback feedback = new AppraisalFeedback()
                {
                    QuestionId=appraisalFeedback.QuestionId,
                    LecturerId=appraisalFeedback.LecturerId,
                    Feedback=appraisalFeedback.Feedback,
                    Year=appraisalFeedback.Year
                };

                context.AppraisalFeedbacks.Add(feedback);
                context.SaveChanges();
            }
        }
    }
}
