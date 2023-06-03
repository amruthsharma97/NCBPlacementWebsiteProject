using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCBP.Entities;

namespace NCBP.Data
{
    public class CurriculumQuestionnaireData
    {
        public void Create(CurriculumQuestionnaireEntity CurriculumQuestionnaire)
        {
            using (var context = new NCBPEntities())
            {
                CurriculumQuestionnaire questionnaire = new CurriculumQuestionnaire()
                {
                    Order = CurriculumQuestionnaire.Order,
                    Question = CurriculumQuestionnaire.Question,
                    Year = CurriculumQuestionnaire.Year
                };
                context.CurriculumQuestionnaires.Add(questionnaire);
                context.SaveChanges();
            }
        }

        public void Update(CurriculumQuestionnaireEntity CurriculumQuestionnaire)
        {
            using (var context = new NCBPEntities())
            {
                var CurriculumQuestionnaireInfo = context.CurriculumQuestionnaires.Single(x => x.Id == CurriculumQuestionnaire.Id);
                CurriculumQuestionnaireInfo.Order = CurriculumQuestionnaire.Order;
                CurriculumQuestionnaireInfo.Question = CurriculumQuestionnaire.Question;
                CurriculumQuestionnaireInfo.Year = CurriculumQuestionnaire.Year;
                context.SaveChanges();
            }
        }


        public IEnumerable<CurriculumQuestionnaireEntity> Read()
        {
            IEnumerable<CurriculumQuestionnaireEntity> CurriculumQuestionnaire;
            using (var context = new NCBPEntities())
            {
                CurriculumQuestionnaire = ParseToEntity(context.CurriculumQuestionnaires).OrderByDescending(s => s.Year).ThenBy(s => s.Order).ToList();
            }
            return CurriculumQuestionnaire;
        }


        public IEnumerable<CurriculumQuestionnaireEntity> Read(int Year)
        {
            IEnumerable<CurriculumQuestionnaireEntity> CurriculumQuestionnaire;
            using (var context = new NCBPEntities())
            {
                CurriculumQuestionnaire = ParseToEntity(context.CurriculumQuestionnaires.Where(x => x.Year == Year)).OrderBy(s => s.Order).ToList();
            }
            return CurriculumQuestionnaire;
        }

        public IEnumerable<CurriculumQuestionnaireEntity> ParseToEntity(IQueryable<CurriculumQuestionnaire> list)
        {
            var CurriculumQuestionnaireList = new List<CurriculumQuestionnaireEntity>();
            foreach (var item in list)
            {
                CurriculumQuestionnaireList.Add(new CurriculumQuestionnaireEntity
                {
                    Id = item.Id,
                    Question = item.Question,
                    Order = item.Order,
                    Year = item.Year
                });
            }
            return CurriculumQuestionnaireList;
        }

        public void RetainOldQuestionnaire(int previousYear, int thisYear)
        {

            IEnumerable<CurriculumQuestionnaireEntity> CurriculumQuestionnaires;
            using (var context = new NCBPEntities())
            {
                CurriculumQuestionnaires = ParseToEntity(context.CurriculumQuestionnaires.Where(x => x.Year == previousYear)).ToList();
                foreach (var item in CurriculumQuestionnaires)
                {
                    CurriculumQuestionnaire questionnaire = new CurriculumQuestionnaire()
                    {
                        Order = item.Order,
                        Question = item.Question,
                        Year = thisYear
                    };
                    context.CurriculumQuestionnaires.Add(questionnaire);
                    context.SaveChanges();
                }
            }
        }

        public void Delete(int Year)
        {
            IEnumerable<CurriculumQuestionnaireEntity> CurriculumQuestionnaires;
            using (var context = new NCBPEntities())
            {
                CurriculumQuestionnaires = ParseToEntity(context.CurriculumQuestionnaires.Where(x => x.Year == Year)).ToList();
                foreach (var item in CurriculumQuestionnaires)
                {
                    var CurriculumQuestionnaireInfo = context.CurriculumQuestionnaires.Single(x => x.Id == item.Id);
                    context.CurriculumQuestionnaires.Remove(CurriculumQuestionnaireInfo);
                    context.SaveChanges();
                }
            }
        }

    }
}
