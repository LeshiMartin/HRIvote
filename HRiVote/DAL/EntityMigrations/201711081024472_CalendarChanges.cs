namespace HRiVote.DAL.EntityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CalendarChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Entity.Calendar", "EmployeeID", "Entity.Employee");
            DropIndex("Entity.Calendar", new[] { "EmployeeID" });
            AlterColumn("Entity.Calendar", "EmployeeID", c => c.Int());
            AlterColumn("Entity.Calendar", "Title", c => c.String());
            CreateIndex("Entity.Calendar", "EmployeeID");
            AddForeignKey("Entity.Calendar", "EmployeeID", "Entity.Employee", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("Entity.Calendar", "EmployeeID", "Entity.Employee");
            DropIndex("Entity.Calendar", new[] { "EmployeeID" });
            AlterColumn("Entity.Calendar", "Title", c => c.String(nullable: false));
            AlterColumn("Entity.Calendar", "EmployeeID", c => c.Int(nullable: false));
            CreateIndex("Entity.Calendar", "EmployeeID");
            AddForeignKey("Entity.Calendar", "EmployeeID", "Entity.Employee", "ID", cascadeDelete: true);
        }
    }
}
