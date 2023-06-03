using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCBP.Entities
{
    public class CurriculumQuestionnaireEntity
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public string Question { get; set; }
        public int Year { get; set; }
    }
}
