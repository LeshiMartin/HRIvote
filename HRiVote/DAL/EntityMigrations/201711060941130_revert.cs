namespace HRiVote.DAL.EntityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class revert : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Entity.Employee", "job_ID", "Entity.JobPosition");
            DropIndex("Entity.Employee", new[] { "job_ID" });
            RenameColumn(table: "Entity.Employee", name: "job_ID", newName: "JobPositionID");
            AddColumn("Entity.Employee", "LastName", c => c.String(nullable: false));
            AddColumn("Entity.Employee", "FirstName", c => c.String(nullable: false));
            AddColumn("Entity.Employee", "BirthDate", c => c.DateTime(nullable: false));
            AddColumn("Entity.Employee", "IsAvailable", c => c.Boolean(nullable: false));
            AddColumn("Entity.Employee", "Achievements", c => c.String());
            AddColumn("Entity.Employee", "Phone", c => c.Long(nullable: false));
            AddColumn("Entity.Employee", "Email", c => c.String(nullable: false));
            AlterColumn("Entity.Employee", "JobPositionID", c => c.Int(nullable: false));
            CreateIndex("Entity.Employee", "JobPositionID");
            AddForeignKey("Entity.Employee", "JobPositionID", "Entity.JobPosition", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("Entity.Employee", "JobPositionID", "Entity.JobPosition");
            DropIndex("Entity.Employee", new[] { "JobPositionID" });
            AlterColumn("Entity.Employee", "JobPositionID", c => c.Int());
            DropColumn("Entity.Employee", "Email");
            DropColumn("Entity.Employee", "Phone");
            DropColumn("Entity.Employee", "Achievements");
            DropColumn("Entity.Employee", "IsAvailable");
            DropColumn("Entity.Employee", "BirthDate");
            DropColumn("Entity.Employee", "FirstName");
            DropColumn("Entity.Employee", "LastName");
            RenameColumn(table: "Entity.Employee", name: "JobPositionID", newName: "job_ID");
            CreateIndex("Entity.Employee", "job_ID");
            AddForeignKey("Entity.Employee", "job_ID", "Entity.JobPosition", "ID");
        }
    }
}
