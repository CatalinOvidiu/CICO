using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CICO.Models.AlimentViewModel
{
    public class AlimentViewModel
    {
        public JournalModel Journal { get; set; }
        public IEnumerable<SelectListItem> TypeDropDown { get; set; }
    }
}