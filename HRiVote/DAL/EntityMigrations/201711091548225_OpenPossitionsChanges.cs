namespace HRiVote.DAL.EntityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OpenPossitionsChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Entity.OpenPosition", "Name", c => c.String(nullable: false));
            AlterColumn("Entity.OpenPosition", "Description", c => c.String(nullable: false));
            AlterColumn("Entity.OpenPosition", "StartOfJobOpenning", c => c.DateTime(nullable: false));
            AlterColumn("Entity.OpenPosition", "EndOfJobOpenning", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("Entity.OpenPosition", "EndOfJobOpenning", c => c.DateTime());
            AlterColumn("Entity.OpenPosition", "StartOfJobOpenning", c => c.DateTime());
            AlterColumn("Entity.OpenPosition", "Description", c => c.String());
            AlterColumn("Entity.OpenPosition", "Name", c => c.String());
        }
    }
}
