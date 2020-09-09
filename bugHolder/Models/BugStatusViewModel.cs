using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace bugHolder.Models
{
    public class BugStatusViewModel
    {
        public List<Bug> Bugs { get; set; }
        public SelectList Statuses { get; set; }
        public string BugStatus { get; set; }
        public string SearchString { get; set; }

    }
}
