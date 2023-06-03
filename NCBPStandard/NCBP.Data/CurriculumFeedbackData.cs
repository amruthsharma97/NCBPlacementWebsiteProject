using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCBP.Entities;

namespace NCBP.Data
{
    public class CurriculumFeedbackData
    {
        public int Read(CurriculumCountEntity CurriculumCount)
        {
            int count;
            using (var context = new NCBPEntities())
            {
                count = context.CurriculumFeedbacks.Where(x => x.SubjectId == CurriculumCount.SubjectId && x.QuestionId == CurriculumCount.QuestionId && x.Year == CurriculumCount.Year).ToList().Count();
            }
            return count;
        }

        public int Read(CurriculumFeedbackEntity CurriculumFeedback)
        {
            int count;
            using (var context = new NCBPEntities())
            {
                count = context.CurriculumFeedbacks.Where(x => x.SubjectId == CurriculumFeedback.SubjectId && x.QuestionId == CurriculumFeedback.QuestionId && x.Feedback == CurriculumFeedback.Feedback && x.Year == CurriculumFeedback.Year).ToList().Count();
            }
            return count;
        }

        public void Create(CurriculumFeedbackEntity CurriculumFeedback)
        {
            using (var context = new NCBPEntities())
            {
                CurriculumFeedback feedback = new CurriculumFeedback()
                {
                    QuestionId = CurriculumFeedback.QuestionId,
                    SubjectId = CurriculumFeedback.SubjectId,
                    Feedback = CurriculumFeedback.Feedback,
                    Year = CurriculumFeedback.Year
                };

                context.CurriculumFeedbacks.Add(feedback);
                context.SaveChanges();
            }
        }

    }
}
