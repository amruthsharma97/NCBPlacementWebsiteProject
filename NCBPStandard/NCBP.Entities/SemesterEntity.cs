using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCBP.Entities
{
    public class SemesterEntity
    {
        public int Id { get; set; }
        public Guid StudentId { get; set; }
        public int SemesterNo { get; set; }
        public string Result { get; set; }
    }
}
