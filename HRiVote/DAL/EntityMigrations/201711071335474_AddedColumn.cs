namespace HRiVote.DAL.EntityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("Entity.EmployeeFiles", "Type", c => c.String(nullable: false));
            AlterColumn("Entity.EmployeeFiles", "File", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("Entity.EmployeeFiles", "File", c => c.String(nullable: false));
            DropColumn("Entity.EmployeeFiles", "Type");
        }
    }
}
