using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCBP.Entities;
using System.Data.Entity;

namespace NCBP.Data
{
    public class AppraisalQuestionnaireData
    {
        public void Create(AppraisalQuestionnaireEntity appraisalQuestionnaire)
        {
            using(var context=new NCBPEntities())
            {
                AppraisalQuestionnaire questionnaire = new AppraisalQuestionnaire()
                {
                    Order = appraisalQuestionnaire.Order,
                    Question=appraisalQuestionnaire.Question,
                    Year=appraisalQuestionnaire.Year
                };
                context.AppraisalQuestionnaires.Add(questionnaire);
                context.SaveChanges();
            }
        }

        public void Update(AppraisalQuestionnaireEntity appraisalQuestionnaire)
        {
            using(var context=new NCBPEntities())
            {
                var appraisalQuestionnaireInfo = context.AppraisalQuestionnaires.Single(x => x.Id == appraisalQuestionnaire.Id);
                appraisalQuestionnaireInfo.Order = appraisalQuestionnaire.Order;
                appraisalQuestionnaireInfo.Question = appraisalQuestionnaire.Question;
                appraisalQuestionnaireInfo.Year = appraisalQuestionnaire.Year;
                context.SaveChanges();
            }
        }


        public IEnumerable<AppraisalQuestionnaireEntity> Read()
        {
            IEnumerable<AppraisalQuestionnaireEntity> appraisalQuestionnaire;
            using (var context = new NCBPEntities())
            {
                appraisalQuestionnaire = ParseToEntity(context.AppraisalQuestionnaires).OrderByDescending(s=>s.Year).ThenBy(s=>s.Order).ToList();
            }
            return appraisalQuestionnaire;
        }


        public IEnumerable<AppraisalQuestionnaireEntity> Read(int Year)
        {
            IEnumerable<AppraisalQuestionnaireEntity> appraisalQuestionnaire;
            using(var context=new NCBPEntities())
            {
                appraisalQuestionnaire = ParseToEntity(context.AppraisalQuestionnaires.Where(x => x.Year == Year)).OrderBy(s => s.Order).ToList();
            }
            return appraisalQuestionnaire;
        }

        public IEnumerable<AppraisalQuestionnaireEntity> ParseToEntity(IQueryable<AppraisalQuestionnaire> list)
        {
            var appraisalQuestionnaireList = new List<AppraisalQuestionnaireEntity>();
            foreach(var item in list)
            {
                appraisalQuestionnaireList.Add(new AppraisalQuestionnaireEntity
                {
                    Id = item.Id,
                    Question=item.Question,
                    Order=item.Order,
                    Year=item.Year
                });
            }
            return appraisalQuestionnaireList;
        }

        public void RetainOldQuestionnaire(int previousYear,int thisYear)
        {
            
            IEnumerable<AppraisalQuestionnaireEntity> appraisalQuestionnaires;
            using(var context=new NCBPEntities())
            {
                appraisalQuestionnaires = ParseToEntity(context.AppraisalQuestionnaires.Where(x => x.Year == previousYear)).ToList();
                foreach (var item in appraisalQuestionnaires)
                {
                    AppraisalQuestionnaire questionnaire = new AppraisalQuestionnaire()
                    {
                        Order = item.Order,
                        Question = item.Question,
                        Year = thisYear
                    };
                    context.AppraisalQuestionnaires.Add(questionnaire);
                    context.SaveChanges();
                }
            }
        }

        public void Delete(int Year)
        {
            IEnumerable<AppraisalQuestionnaireEntity> appraisalQuestionnaires;
            using (var context=new NCBPEntities())
            {
                appraisalQuestionnaires = ParseToEntity(context.AppraisalQuestionnaires.Where(x => x.Year == Year)).ToList();
                foreach (var item in appraisalQuestionnaires)
                {
                    var appraisalQuestionnaireInfo = context.AppraisalQuestionnaires.Single(x => x.Id == item.Id);
                    context.AppraisalQuestionnaires.Remove(appraisalQuestionnaireInfo);
                    context.SaveChanges();
                }
            }
        }
    }
}
