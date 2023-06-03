using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCBP.Entities
{
    public class CityEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }
        public bool IsActive { get; set; }
    }
}
