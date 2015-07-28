using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCApplication.Models
{
    public class Entry
    {
        [Key] 
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public decimal Hours { get; set; }
        public int UserId { get; set; }

        
        public virtual User User { get; set; }

        public virtual Project Project { get; set; }



    }
}