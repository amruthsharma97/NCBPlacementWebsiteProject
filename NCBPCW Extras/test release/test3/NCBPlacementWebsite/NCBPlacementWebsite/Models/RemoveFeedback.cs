using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NCBPlacementWebsite.Models
{
    public class RemoveFeedback
    {

        [Required]

        public int Year { get; set; }
    }
}