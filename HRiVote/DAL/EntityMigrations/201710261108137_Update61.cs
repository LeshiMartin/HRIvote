namespace HRiVote.DAL.EntityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update61 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Entity.Employee", "MeetingID", "Entity.Meeting");
            DropForeignKey("Entity.ProjectManagement", "EmployeeID", "Entity.Employee");
            DropForeignKey("Entity.ProjectManagement", "MeetingID", "Entity.Meeting");
            DropForeignKey("Entity.Calendar", "ProjectManagementID", "Entity.ProjectManagement");
            DropIndex("Entity.Employee", new[] { "MeetingID" });
            DropIndex("Entity.ProjectManagement", new[] { "EmployeeID" });
            DropIndex("Entity.ProjectManagement", new[] { "MeetingID" });
            DropIndex("Entity.Calendar", new[] { "ProjectManagementID" });
            DropColumn("Entity.Employee", "MeetingID");
            DropColumn("Entity.Calendar", "ProjectManagementID");
            DropTable("Entity.Meeting");
            DropTable("Entity.ProjectManagement");
        }
        
        public override void Down()
        {
            CreateTable(
                "Entity.ProjectManagement",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProjectName = c.String(),
                        Description = c.String(),
                        Status = c.Boolean(nullable: false),
                        File = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        EmployeeID = c.Int(nullable: false),
                        MeetingID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "Entity.Meeting",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MeetingDay = c.DateTime(nullable: false),
                        MeetingTime = c.DateTime(nullable: false),
                        MeetingTitle = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("Entity.Calendar", "ProjectManagementID", c => c.Int());
            AddColumn("Entity.Employee", "MeetingID", c => c.Int());
            CreateIndex("Entity.Calendar", "ProjectManagementID");
            CreateIndex("Entity.ProjectManagement", "MeetingID");
            CreateIndex("Entity.ProjectManagement", "EmployeeID");
            CreateIndex("Entity.Employee", "MeetingID");
            AddForeignKey("Entity.Calendar", "ProjectManagementID", "Entity.ProjectManagement", "ID");
            AddForeignKey("Entity.ProjectManagement", "MeetingID", "Entity.Meeting", "ID");
            AddForeignKey("Entity.ProjectManagement", "EmployeeID", "Entity.Employee", "ID", cascadeDelete: true);
            AddForeignKey("Entity.Employee", "MeetingID", "Entity.Meeting", "ID");
        }
    }
}
