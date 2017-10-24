namespace HRiVote.DAL.EntityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Entity.Employee", "MeetingID", "Entity.Meeting");
            DropForeignKey("Entity.ProjectManagement", "MeetingID", "Entity.Meeting");
            DropIndex("Entity.Employee", new[] { "MeetingID" });
            DropIndex("Entity.ProjectManagement", new[] { "MeetingID" });
            AlterColumn("Entity.Employee", "MeetingID", c => c.Int());
            AlterColumn("Entity.ProjectManagement", "MeetingID", c => c.Int());
            CreateIndex("Entity.Employee", "MeetingID");
            CreateIndex("Entity.ProjectManagement", "MeetingID");
            AddForeignKey("Entity.Employee", "MeetingID", "Entity.Meeting", "ID");
            AddForeignKey("Entity.ProjectManagement", "MeetingID", "Entity.Meeting", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("Entity.ProjectManagement", "MeetingID", "Entity.Meeting");
            DropForeignKey("Entity.Employee", "MeetingID", "Entity.Meeting");
            DropIndex("Entity.ProjectManagement", new[] { "MeetingID" });
            DropIndex("Entity.Employee", new[] { "MeetingID" });
            AlterColumn("Entity.ProjectManagement", "MeetingID", c => c.Int(nullable: false));
            AlterColumn("Entity.Employee", "MeetingID", c => c.Int(nullable: false));
            CreateIndex("Entity.ProjectManagement", "MeetingID");
            CreateIndex("Entity.Employee", "MeetingID");
            AddForeignKey("Entity.ProjectManagement", "MeetingID", "Entity.Meeting", "ID", cascadeDelete: true);
            AddForeignKey("Entity.Employee", "MeetingID", "Entity.Meeting", "ID", cascadeDelete: true);
        }
    }
}
