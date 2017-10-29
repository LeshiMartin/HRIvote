namespace HRiVote.DAL.EntityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Entity.Candidate",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        CV = c.String(),
                        InterviewDate = c.DateTime(nullable: false),
                        InterviewRound = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "Entity.Employee",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        FullName = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        IsAvailable = c.Boolean(nullable: false),
                        VacationDays = c.Int(nullable: false),
                        Projects = c.String(),
                        Achievements = c.String(),
                        JobPositionID = c.Int(nullable: false),
                        Phone = c.Long(nullable: false),
                        Email = c.String(),
                        CV = c.String(),
                        Photo = c.String(),
                        MeetingID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Entity.JobPosition", t => t.JobPositionID, cascadeDelete: true)
                .ForeignKey("Entity.Meeting", t => t.MeetingID, cascadeDelete: true)
                .Index(t => t.JobPositionID)
                .Index(t => t.MeetingID);
            
            CreateTable(
                "Entity.JobPosition",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "Entity.Meeting",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MeetingDay = c.DateTime(nullable: false),
                        MeetingTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            //CreateTable(
            //    "Entity.OpenPosition",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            Name = c.String(),
            //            Description = c.String(),
            //        })
            //    .PrimaryKey(t => t.ID);
            
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
                        EndDate = c.DateTime(nullable: false),
                        EmployeeID = c.Int(),
                        MeetingID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Entity.Employee", t => t.EmployeeID)
                .ForeignKey("Entity.Meeting", t => t.MeetingID, cascadeDelete: true)
                .Index(t => t.EmployeeID)
                .Index(t => t.MeetingID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Entity.ProjectManagement", "MeetingID", "Entity.Meeting");
            DropForeignKey("Entity.ProjectManagement", "EmployeeID", "Entity.Employee");
            DropForeignKey("Entity.Employee", "MeetingID", "Entity.Meeting");
            DropForeignKey("Entity.Employee", "JobPositionID", "Entity.JobPosition");
            DropIndex("Entity.ProjectManagement", new[] { "MeetingID" });
            DropIndex("Entity.ProjectManagement", new[] { "EmployeeID" });
            DropIndex("Entity.Employee", new[] { "MeetingID" });
            DropIndex("Entity.Employee", new[] { "JobPositionID" });
            DropTable("Entity.ProjectManagement");
            DropTable("Entity.OpenPosition");
            DropTable("Entity.Meeting");
            DropTable("Entity.JobPosition");
            DropTable("Entity.Employee");
            DropTable("Entity.Candidate");
        }
    }
}
