namespace HRiVote.DAL.EntityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Entity.Employee", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("Entity.Employee", "Email", c => c.String());
        }
    }
}
