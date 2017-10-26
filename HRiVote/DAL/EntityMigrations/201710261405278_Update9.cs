namespace HRiVote.DAL.EntityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("Entity.Employee", "EmploymentStatus", c => c.Boolean(nullable: false));
            AddColumn("Entity.Employee", "Skills", c => c.String());
            AddColumn("Entity.JobPosition", "Title", c => c.String());
            AddColumn("Entity.JobPosition", "Status", c => c.Boolean(nullable: false));
            DropColumn("Entity.JobPosition", "Name");
        }
        
        public override void Down()
        {
            AddColumn("Entity.JobPosition", "Name", c => c.String());
            DropColumn("Entity.JobPosition", "Status");
            DropColumn("Entity.JobPosition", "Title");
            DropColumn("Entity.Employee", "Skills");
            DropColumn("Entity.Employee", "EmploymentStatus");
        }
    }
}
