namespace HRiVote.DAL.EntityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesinTheCalendarModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Entity.Calendar", "EmployeeID", "Entity.Employee");
            DropIndex("Entity.Calendar", new[] { "EmployeeID" });
            AlterColumn("Entity.Calendar", "EmployeeID", c => c.Int(nullable: false));
         //   AlterColumn("Entity.Calendar", "Title", c => c.String(nullable: false));
            CreateIndex("Entity.Calendar", "EmployeeID");
            AddForeignKey("Entity.Calendar", "EmployeeID", "Entity.Employee", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("Entity.Calendar", "EmployeeID", "Entity.Employee");
            DropIndex("Entity.Calendar", new[] { "EmployeeID" });
            AlterColumn("Entity.Calendar", "Title", c => c.String());
            AlterColumn("Entity.Calendar", "EmployeeID", c => c.Int());
            CreateIndex("Entity.Calendar", "EmployeeID");
            AddForeignKey("Entity.Calendar", "EmployeeID", "Entity.Employee", "ID");
        }
    }
}
