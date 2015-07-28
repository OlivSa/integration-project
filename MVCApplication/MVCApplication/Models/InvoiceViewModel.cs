using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MVCApplication.Models
{
    public class InvoiceViewModel
    {

        [DisplayName("Date")]
        public string InvoiceDate { get; set; }

        [DisplayName("Due date")]
        public string InvoiceDueDate { get; set; }

        [DisplayName("Without tax")]
        public string InvoiceSum { get; set; }
        [DisplayName("Sum total")]
        public string InvoiceSum_tax { get; set; }
        [DisplayName("Client Name")]
        public string ClientName { get; set; }
        [DisplayName("Client address etc.")]
        public string ClientAddress { get; set; }
        [DisplayName("Client currency")]
        public string ClientCurrency { get; set; }
        public  Item[] Items{ get; set; }

    }
}