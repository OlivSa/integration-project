using MVCApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCApplication.Data
{
    interface IRepository
    {
        IEnumerable<Project> GetProjects();
        IEnumerable<Entry> GetEntries();
        void CreateEntries(IEnumerable<Entry> entries);
        void CreateProjects(IEnumerable<Project> projects);
        void CreateClients(IEnumerable<Client> clients);
        void CreateUsers(IEnumerable<User> users);
    }
}
