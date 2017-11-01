namespace HRiVote.DAL.EntityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DescriptionCalendar : DbMigration
    {
        public override void Up()
        {
            AddColumn("Entity.Calendar", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("Entity.Calendar", "Description");
        }
    }
}
