namespace HRiVote.DAL.EntityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedtheCandidateIDfromCalendar : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Entity.Calendar", "CandidateID", "Entity.Candidate");
            DropIndex("Entity.Calendar", new[] { "CandidateID" });
            DropColumn("Entity.Calendar", "CandidateID");
        }
        
        public override void Down()
        {
            AddColumn("Entity.Calendar", "CandidateID", c => c.Int());
            CreateIndex("Entity.Calendar", "CandidateID");
            AddForeignKey("Entity.Calendar", "CandidateID", "Entity.Candidate", "ID");
        }
    }
}
