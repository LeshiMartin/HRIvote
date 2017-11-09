namespace HRiVote.DAL.EntityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Entity.Calendar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(),
                        ProjectManagementID = c.Int(),
                        CandidateID = c.Int(),
                        Title = c.String(),
                        StartOfVacation = c.DateTime(),
                        EndOfVacation = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Entity.Candidate", t => t.CandidateID)
                .ForeignKey("Entity.Employee", t => t.EmployeeID,cascadeDelete:true)
                .ForeignKey("Entity.ProjectManagement", t => t.ProjectManagementID)
                .Index(t => t.EmployeeID )
                .Index(t => t.ProjectManagementID)
                .Index(t => t.CandidateID);
            
            AddColumn("Entity.Meeting", "MeetingTitle", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("Entity.Calendar", "ProjectManagementID", "Entity.ProjectManagement");
            DropForeignKey("Entity.Calendar", "EmployeeID", "Entity.Employee");
            DropForeignKey("Entity.Calendar", "CandidateID", "Entity.Candidate");
            DropIndex("Entity.Calendar", new[] { "CandidateID" });
            DropIndex("Entity.Calendar", new[] { "ProjectManagementID" });
            DropIndex("Entity.Calendar", new[] { "EmployeeID" });
            DropColumn("Entity.Meeting", "MeetingTitle");
            DropTable("Entity.Calendar");
        }
    }
}
