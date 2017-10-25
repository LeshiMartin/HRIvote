namespace HRiVote.DAL.EntityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Entity.ProjectManagement", "EmployeeID", "Entity.Employee");
            DropIndex("Entity.ProjectManagement", new[] { "EmployeeID" });
            AlterColumn("Entity.ProjectManagement", "EndDate", c => c.DateTime());
            AlterColumn("Entity.ProjectManagement", "EmployeeID", c => c.Int(nullable: false));
            CreateIndex("Entity.ProjectManagement", "EmployeeID");
            AddForeignKey("Entity.ProjectManagement", "EmployeeID", "Entity.Employee", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("Entity.ProjectManagement", "EmployeeID", "Entity.Employee");
            DropIndex("Entity.ProjectManagement", new[] { "EmployeeID" });
            AlterColumn("Entity.ProjectManagement", "EmployeeID", c => c.Int());
            AlterColumn("Entity.ProjectManagement", "EndDate", c => c.DateTime(nullable: false));
            CreateIndex("Entity.ProjectManagement", "EmployeeID");
            AddForeignKey("Entity.ProjectManagement", "EmployeeID", "Entity.Employee", "ID");
        }
    }
}
