namespace HRiVote.DAL.EntityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update6 : DbMigration
    {
        public override void Up()
        {
            DropColumn("Entity.Employee", "Projects");
        }
        
        public override void Down()
        {
            AddColumn("Entity.Employee", "Projects", c => c.String());
        }
    }
}
