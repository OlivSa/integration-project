using MVCApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCApplication.Services
{
    public static class BusinessHelper
    {
       public static IEnumerable<Entry> GroupEntriesByProjectsAndUsers(IEnumerable<Entry> entries)
        {
            var customList = new List<Entry>();
            var groupedProjects = from entry in entries
                                  group entry by entry.ProjectId into newGroup
                                  orderby newGroup.Key
                                  select newGroup;

            foreach (var project in groupedProjects)
            {
                decimal hourSum = 0M;
                var groupedUsers = project
                    .GroupBy(u => u.UserId);
                foreach (var item in groupedUsers)
                {
                    hourSum = item.Select(i => i.Hours).Sum();
                    if (hourSum>0)
                    {
                        customList.Add(new Entry()
                        {
                            UserId = item.First().UserId,
                            ProjectId = item.First().ProjectId,
                            Hours = hourSum
                        });
                    }
                    
                }
            }

            return customList;
        }
       public static IEnumerable<int> GetProjectIds(IEnumerable<Project> projects)
       {
           return projects
               .Select(p => p.ProjectId)
               .ToList();
       }
       public static IEnumerable<int> GetUserIds(IEnumerable<Entry> entries)
       {
           return entries
               .Select(e => e.UserId)
               .Distinct()
               .ToList();
       }

       public static IEnumerable<int> GetClientIds(IEnumerable<Project> projects)
       {
           return projects
               .Select(p => p.ClientId)
               .Distinct() //added
               .ToList();
       }

       public static IEnumerable<InvoiceViewModel> CreateInvoiceViewModels(IEnumerable<Project> projects,
       IEnumerable<Entry> entries)
      {

          var invoiceViewModels = new List<InvoiceViewModel>();
          var entriesPerProject = new List<Entry>();
          var projectsPerClient = projects
             .GroupBy(p => p.ClientId);
          foreach (var item in projectsPerClient)
          {
              var invoice = new InvoiceViewModel();
              var sum = 0M;
              var sum_tax = 0M;
              var itemsList = new List<Item>();

              invoice.InvoiceDate = DateTime.Today.ToString("yyyyMMdd");
              invoice.InvoiceDueDate = DateTime.Today.AddDays(10).ToString("yyyyMMdd");
              invoice.ClientCurrency = item.First().Client.Currency;
              invoice.ClientName = item.First().Client.Name;
              invoice.ClientAddress = item.First().Client.Details;
              foreach (var project in item)
              {
                  
                  entriesPerProject = entries
                  .Where(e => e.ProjectId == project.ProjectId)
                  .ToList();
                  var projectName = project.Name;

                  foreach (var entry in entriesPerProject)
                  {
                      var hoursxrate = entry.Hours * entry.User.HourlyRate;
                      var invoiceItem = new Item()
                      {
                          Amount = Convert.ToDouble(entry.Hours),
                          Subject = entry.User.FirstName + " " + entry.User.LastName,
                          Code = entry.Project.Name,
                          UnitPrice = entry.User.HourlyRate.ToString(),
                          Sum = (hoursxrate).ToString(),
                          Tax = 22,
                          Sum_tax = (hoursxrate + (hoursxrate*0.22m)).ToString()
                      };
                      itemsList.Add(invoiceItem);
                      sum = sum + hoursxrate;
                      sum_tax = sum_tax + (hoursxrate + (hoursxrate * 0.22m)); 
                  }

                  invoice.InvoiceSum = sum.ToString();
                  invoice.InvoiceSum_tax = sum_tax.ToString();
                  invoice.Items = itemsList.ToArray();
              }
              if (Convert.ToDouble(invoice.InvoiceSum)>0)
              {
                  invoiceViewModels.Add(invoice);
              }
              
          }
          return invoiceViewModels;
      }
    }
}