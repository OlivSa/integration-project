using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MVCApplication.Models
{
    public class Item
    {
        public string Code { get; set; }//project name
        [DisplayName("Employeer")]
        public string Subject { get; set; }//user name
        [DisplayName("Hourly rate")]
        public string UnitPrice { get; set; }//hourly rate
        [DisplayName("Amount (hours)")]
        public double Amount { get; set; }//hours
        [DisplayName("Sum Tax Excl.")]
        public string Sum { get; set; }//UnitPrice*Amount
        public double Tax { get; set; }//22
        [DisplayName(" Sum")]
        public string Sum_tax { get; set; }



    }
}