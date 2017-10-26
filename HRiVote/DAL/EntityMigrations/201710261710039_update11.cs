namespace HRiVote.DAL.EntityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("Entity.Skills", "status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Entity.Skills", "status");
        }
    }
}
