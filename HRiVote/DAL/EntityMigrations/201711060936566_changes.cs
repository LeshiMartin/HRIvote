namespace HRiVote.DAL.EntityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Entity.Employee", "JobPositionID", "Entity.JobPosition");
            DropIndex("Entity.Employee", new[] { "JobPositionID" });
            RenameColumn(table: "Entity.Employee", name: "JobPositionID", newName: "job_ID");
            AlterColumn("Entity.Employee", "job_ID", c => c.Int());
            CreateIndex("Entity.Employee", "job_ID");
            AddForeignKey("Entity.Employee", "job_ID", "Entity.JobPosition", "ID");
            DropColumn("Entity.Employee", "LastName");
            DropColumn("Entity.Employee", "FirstName");
            DropColumn("Entity.Employee", "BirthDate");
            DropColumn("Entity.Employee", "IsAvailable");
            DropColumn("Entity.Employee", "Achievements");
            DropColumn("Entity.Employee", "Phone");
            DropColumn("Entity.Employee", "Email");
        }
        
        public override void Down()
        {
            AddColumn("Entity.Employee", "Email", c => c.String(nullable: false));
            AddColumn("Entity.Employee", "Phone", c => c.Long(nullable: false));
            AddColumn("Entity.Employee", "Achievements", c => c.String());
            AddColumn("Entity.Employee", "IsAvailable", c => c.Boolean(nullable: false));
            AddColumn("Entity.Employee", "BirthDate", c => c.DateTime(nullable: false));
            AddColumn("Entity.Employee", "FirstName", c => c.String(nullable: false));
            AddColumn("Entity.Employee", "LastName", c => c.String(nullable: false));
            DropForeignKey("Entity.Employee", "job_ID", "Entity.JobPosition");
            DropIndex("Entity.Employee", new[] { "job_ID" });
            AlterColumn("Entity.Employee", "job_ID", c => c.Int(nullable: false));
            RenameColumn(table: "Entity.Employee", name: "job_ID", newName: "JobPositionID");
            CreateIndex("Entity.Employee", "JobPositionID");
            AddForeignKey("Entity.Employee", "JobPositionID", "Entity.JobPosition", "ID", cascadeDelete: true);
        }
    }
}
