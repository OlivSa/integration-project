using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCApplication.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }

        public virtual Client Client { get; set; }
    }
}