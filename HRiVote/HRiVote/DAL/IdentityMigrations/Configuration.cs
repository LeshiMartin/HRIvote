namespace HRiVote.DAL.IdentityMigrations
{
    using HRiVote.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HRiVote.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DAL\IdentityMigrations";
        }

        protected override void Seed(HRiVote.Models.ApplicationDbContext context)
        {
            if (!context.Users.Any(u => u.UserName == "martin.leshi@ivote.mk"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var Manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser {  UserName= "martin.leshi@ivote.mk" };
                Manager.Create(user, "password");
            }
        }
    }
}
