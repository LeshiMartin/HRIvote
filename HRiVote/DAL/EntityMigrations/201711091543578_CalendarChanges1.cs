namespace HRiVote.DAL.EntityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CalendarChanges1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Entity.Calendar", "StartOfVacation", c => c.DateTime(nullable: false));
            AlterColumn("Entity.Calendar", "EndOfVacation", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("Entity.Calendar", "EndOfVacation", c => c.DateTime());
            AlterColumn("Entity.Calendar", "StartOfVacation", c => c.DateTime());
        }
    }
}
