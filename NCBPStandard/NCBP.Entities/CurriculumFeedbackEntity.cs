using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCBP.Entities
{
    public class CurriculumFeedbackEntity
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int SubjectId { get; set; }
        public string Feedback { get; set; }
        public int Year { get; set; }
    }
}
