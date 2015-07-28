using MVCApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MVCApplication.Data
{
    public class Repository: IRepository
    {
        public Repository()
        {
           
        }

        public void DeleteDataFromDatabase()
        {
            using (var db = new AppContext())
            {
                db.Entries.RemoveRange(db.Entries);
                db.Projects.RemoveRange(db.Projects);
                db.Users.RemoveRange(db.Users);
                db.Clients.RemoveRange(db.Clients);
                db.SaveChanges();
            }
        }

        public IEnumerable<Entry> GetEntries()
        {
            using (var db = new AppContext())
            {
                return db
                    .Entries
                    .Include("User")
                    .Include("Project")
                    .ToList();
            }
        }

        public void CreateEntries(IEnumerable<Entry> entries)
        {
            using (var db = new AppContext())
            {
                foreach (var entry in entries)
                {
                    db.Entries.Add(entry);
                }
                db.SaveChanges();
            }
        }

        public IEnumerable<Project> GetProjects()
        {
            using (var db = new AppContext())
            {
                return db
                    .Projects
                    .Include("Client")
                    .ToList();
            }
        }

        public void CreateProjects(IEnumerable<Project> projects)
        {
            using (var db = new AppContext())
            {
                foreach (var project in projects)
                {
                    db.Projects.Add(project);
                }
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
        }

        public void CreateClients(IEnumerable<Client> clients)
        {
            using (var db = new AppContext())
            {
                foreach (var client in clients)
                {
                    db.Clients.Add(client);
                }
                db.SaveChanges();
            }
        }

        public void CreateUsers(IEnumerable<User> users)
        {
            using (var db = new AppContext())
            {
                foreach (var user in users)
                {
                    db.Users.Add(user);
                }
                db.SaveChanges();
            }
        }
    }
}