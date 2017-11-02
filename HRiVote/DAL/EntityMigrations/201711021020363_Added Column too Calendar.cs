namespace HRiVote.DAL.EntityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedColumntooCalendar : DbMigration
    {
        public override void Up()
        {
            AddColumn("Entity.Calendar", "status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Entity.Calendar", "status");
        }
    }
}
