using MVCApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCApplication.Data
{
    public class AppContext: DbContext
    {
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Project>()
                .HasKey(e => e.ProjectId)//configures the primary key property for this entity
                .Property(e => e.ProjectId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<User>()
                .HasKey(e => e.Id)//configures the primary key property for this entity
                .Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Client>()
                .HasKey(e => e.Id)//configures the primary key property for this entity
                .Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);


        }
    }
}