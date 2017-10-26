using HRiVote.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace HRiVote.DAL
{
    public class Entity : DbContext
    {
        public Entity() : base("DefaultConnection")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.HasDefaultSchema("Entity");
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Employee> emps { get; set; }
        public DbSet<ProjectManagement> project { get; set; }
        public DbSet<Candidate> aplikanti { get; set; }
        public DbSet<OpenPosition> pozicii { get; set; }
        public DbSet<Meeting> sredbi { get; set; } 
        public DbSet<Calendar> kalendar { get; set; }
        public DbSet<JobPosition> positions { get; set; }
    }
}