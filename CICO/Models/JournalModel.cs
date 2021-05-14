using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CICO.Models
{
    public class JournalModel
    {
        public Guid IdJournal { get; set; }

        [Required(ErrorMessage ="Mandatory field")]
        [DisplayName("Aliment")]
        public Guid IdAliment { get; set; }

        [Required(ErrorMessage = "Mandatory field")]
        public string MealName { get; set; }

        [Required(ErrorMessage = "Mandatory field")]
        public decimal CalloriesAmount { get; set; }
        public List<AlimentModel> Aliments { get; set; }

    }
}