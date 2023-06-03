using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCBP.Entities
{
    public class AppraisalFeedbackEntity
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int LecturerId { get; set; }
        public int Feedback { get; set; }
        public int Year { get; set; }
    }
}
