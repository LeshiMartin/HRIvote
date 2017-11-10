namespace HRiVote.DAL.EntityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRemarks : DbMigration
    {
        public override void Up()
        {
            AddColumn("Entity.Candidate", "Remarks", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("Entity.Candidate", "Remarks");
        }
    }
}
