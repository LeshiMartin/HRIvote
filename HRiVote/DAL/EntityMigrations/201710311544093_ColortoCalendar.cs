namespace HRiVote.DAL.EntityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColortoCalendar : DbMigration
    {
        public override void Up()
        {
            AddColumn("Entity.Calendar", "Color", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("Entity.Calendar", "Color");
        }
    }
}
