using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CICO.Models
{
    public class AlimentModel
    {
        
        public Guid IdAliment { get; set; }

        [Required(ErrorMessage = "Mandatory field")]
        public string Name { get; set; }
    }
}