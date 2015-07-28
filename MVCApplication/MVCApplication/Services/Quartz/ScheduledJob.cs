using Quartz;
using System;
using System.Net;
using System.IO;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Xml.Linq;
using MVCApplication.Services.Harvest;
using System.Collections.Generic;
using MVCApplication.Data;
using MVCApplication.Models;
using MVCApplication.Services.Maventa;
using System.Threading.Tasks;

namespace MVCApplication.Services.Quartz
{
    public class ScheduledJob: IJob
    {
        //private IEnumerable<Entry> entries;
        private Repository repo;
        public ScheduledJob()
        {
            repo = new Repository();        
        }

        public static bool Validator(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        public void Execute(IJobExecutionContext context)
        {
            HarvestService harvestService = new HarvestService();
            var projects = harvestService.GetProjects();
            var entries = BusinessHelper.GroupEntriesByProjectsAndUsers(
                harvestService.GetEntries(BusinessHelper.GetProjectIds(projects)));
            var users = harvestService.GetUsers(BusinessHelper.GetUserIds(entries));
            var clients = harvestService.GetClients(BusinessHelper.GetClientIds(projects));

            repo.DeleteDataFromDatabase();
            repo.CreateClients(clients);
            repo.CreateUsers(users);
            repo.CreateProjects(projects);
            repo.CreateEntries(entries);

            var maventaService = new MaventaService();

            var invoices = BusinessHelper.CreateInvoiceViewModels(repo.GetProjects(),repo.GetEntries());
            maventaService.SendInvoicesToMaventa(invoices);
           
        }
    }
}