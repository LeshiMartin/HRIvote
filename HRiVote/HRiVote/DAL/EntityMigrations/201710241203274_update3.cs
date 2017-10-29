namespace HRiVote.DAL.EntityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("Entity.Candidate", "InterviewTime", c => c.DateTime());
            AlterColumn("Entity.Candidate", "LastName", c => c.String(nullable: false));
            AlterColumn("Entity.Candidate", "FirstName", c => c.String(nullable: false));
            AlterColumn("Entity.Candidate", "Email", c => c.String(nullable: false));
            AlterColumn("Entity.Candidate", "PhoneNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("Entity.Candidate", "PhoneNumber", c => c.String());
            AlterColumn("Entity.Candidate", "Email", c => c.String());
            AlterColumn("Entity.Candidate", "FirstName", c => c.String());
            AlterColumn("Entity.Candidate", "LastName", c => c.String());
            DropColumn("Entity.Candidate", "InterviewTime");
        }
    }
}
