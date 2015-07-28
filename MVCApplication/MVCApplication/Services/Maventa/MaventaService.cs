using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using MVCApplication.MaventaAPI;
using MVCApplication.Models;

namespace MVCApplication.Services.Maventa
{
    public class MaventaService
    {
        public void SendInvoicesToMaventa(IEnumerable<InvoiceViewModel>invoices)
        {
            ApiKeys apiKeys = new ApiKeys();
            apiKeys.user_api_key = "x"; //User API key
            apiKeys.company_uuid = "y"; //UUID of current company

            //Create invoice
            foreach(var invoice in invoices)
            {
                MaventaAPI.InvoiceParamsInC invoiceOut = new MaventaAPI.InvoiceParamsInC();
                //invoiceOut.invoice_nr = "1001";
                //invoiceOut.reference_nr = "10016";
                invoiceOut.date = invoice.InvoiceDate;
                invoiceOut.date_due = invoice.InvoiceDueDate;
                invoiceOut.sum = invoice.InvoiceSum;
                invoiceOut.sum_tax = invoice.InvoiceSum_tax;
                invoiceOut.currency = invoice.ClientCurrency;
                //invoiceOut.lang = "FI";

                //Customer information
                MaventaAPI.CustomerParamsInC customerOut = new CustomerParamsInC();
                customerOut.name = invoice.ClientName;
                //customerOut.bid = "FI1234567";
                //customerOut.ovt = "0037111111";
                //customerOut.email = "customer@maventa.com";
                //customerOut.country = "FI";
                customerOut.address1 = invoice.ClientAddress;
                invoiceOut.customer = customerOut;

                //Invoice items

                List<ItemsInC> itemList = new List<ItemsInC>();
                foreach(var item in invoice.Items)
                {
                    ItemsInC itemOut = new ItemsInC();
                    itemOut.item_code = item.Code;
                    itemOut.subject = item.Subject;
                    itemOut.amount = item.Amount;
                    itemOut.price = item.UnitPrice;
                    itemOut.tax = item.Tax;
                    itemOut.sum = item.Sum;
                    itemOut.sum_tax = item.Sum_tax;
                    itemList.Add(itemOut);
                }
                
                invoiceOut.items = itemList.ToArray();


                //Send invoice
                InvoiceStatus invoiceResponse = new InvoiceStatus();
                MaventaApiPortClient port = new MaventaApiPortClient();
                invoiceResponse = port.invoice_create(apiKeys, invoiceOut);
            }
            
        }
    }
}