using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CICO.Models
{
    public class UserModel
    {
        public Guid IdUser { get; set; }

        [Required(ErrorMessage = "Mandatory field")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Mandatory field")]
        public decimal Weight { get; set; }
        [Required(ErrorMessage = "Mandatory field")]
        public decimal Height { get; set; }
        [Required(ErrorMessage = "Mandatory field")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Mandatory field")]
        public decimal CalloeieRequirements{ get; set; }


    }
}