using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace bugHolder.Models
{
    public class Bug
    {
        public int Id { get; set; }
        public string Title { get; set; }
        
        public string Status { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        
    }
}