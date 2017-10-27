namespace HRiVote.DAL.EntityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SKillsUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("Entity.OpenPosition", "Status", c => c.Boolean(nullable: false));
            AddColumn("Entity.OpenPosition", "StartOfJobOpenning", c => c.DateTime());
            AddColumn("Entity.OpenPosition", "EndOfJobOpenning", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("Entity.OpenPosition", "EndOfJobOpenning");
            DropColumn("Entity.OpenPosition", "StartOfJobOpenning");
            DropColumn("Entity.OpenPosition", "Status");
        }
    }
}
