using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCBP.Entities;
using NCBP.Data;

namespace NCBP.Business
{
    public class CurriculumQuestionnaireComponent
    {
        public void AddCurriculumQuestionnaire(CurriculumQuestionnaireEntity curriculumQuestionnaire)
        {
            CurriculumQuestionnaireData curriculumQuestionnaireData = new CurriculumQuestionnaireData();
            curriculumQuestionnaireData.Create(curriculumQuestionnaire);
        }

        public void UpdateCurriculumQuestionaire(CurriculumQuestionnaireEntity CurriculumQuestionnaire)
        {
            CurriculumQuestionnaireData CurriculumQuestionnaireData = new CurriculumQuestionnaireData();
            CurriculumQuestionnaireData.Update(CurriculumQuestionnaire);
        }

        public IEnumerable<CurriculumQuestionnaireEntity> GetCurriculumQuestionnaires(int Year)
        {
            CurriculumQuestionnaireData CurriculumQuestionnaire = new CurriculumQuestionnaireData();
            IEnumerable<CurriculumQuestionnaireEntity> CurriculumQuestionnaires;
            CurriculumQuestionnaires = CurriculumQuestionnaire.Read(Year);
            return CurriculumQuestionnaires;
        }

        public void RetainPreviousQuestionnaire(int previousYear, int thisYear)
        {
            CurriculumQuestionnaireData CurriculumQuestionnaire = new CurriculumQuestionnaireData();
            CurriculumQuestionnaire.RetainOldQuestionnaire(previousYear, thisYear);
        }

        public void RemoveCurriculumQuestionnaire(int Year)
        {
            CurriculumQuestionnaireData CurriculumQuestionnaire = new CurriculumQuestionnaireData();
            CurriculumQuestionnaire.Delete(Year);
        }

    }
}
