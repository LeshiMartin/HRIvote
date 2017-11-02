namespace HRiVote.DAL.EntityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumninCalendar : DbMigration
    {
        public override void Up()
        {
            AddColumn("Entity.Calendar", "EmpName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("Entity.Calendar", "EmpName");
        }
    }
}
