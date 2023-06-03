using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCBP.Entities;
using NCBP.Data;

namespace NCBP.Business
{
    public class AppraisalQuestionnaireComponent
    {

        public void AddAppraisalQuestionnaire(AppraisalQuestionnaireEntity appraisalQuestionnaire)
        {
            AppraisalQuestionnaireData appraisalQuestionnaireData = new AppraisalQuestionnaireData();
            appraisalQuestionnaireData.Create(appraisalQuestionnaire);
        }

        public void UpdateAppraisalQuestionaire(AppraisalQuestionnaireEntity appraisalQuestionnaire)
        {
            AppraisalQuestionnaireData appraisalQuestionnaireData = new AppraisalQuestionnaireData();
            appraisalQuestionnaireData.Update(appraisalQuestionnaire);
        }

        public IEnumerable<AppraisalQuestionnaireEntity> GetAppraisalQuestionnaires(int Year)
        {
            AppraisalQuestionnaireData appraisalQuestionnaire = new AppraisalQuestionnaireData();
            IEnumerable<AppraisalQuestionnaireEntity> appraisalQuestionnaires;
            appraisalQuestionnaires = appraisalQuestionnaire.Read(Year);
            return appraisalQuestionnaires;
        }

        public void RetainPreviousQuestionnaire(int previousYear,int thisYear)
        {
            AppraisalQuestionnaireData appraisalQuestionnaire = new AppraisalQuestionnaireData();
            appraisalQuestionnaire.RetainOldQuestionnaire(previousYear, thisYear);
        }

        public void RemoveAppraisalQuestionnaire(int Year)
        {
            AppraisalQuestionnaireData appraisalQuestionnaire = new AppraisalQuestionnaireData();
            appraisalQuestionnaire.Delete(Year);
        }
    }
}
