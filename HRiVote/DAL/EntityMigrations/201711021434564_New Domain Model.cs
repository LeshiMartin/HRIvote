namespace HRiVote.DAL.EntityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewDomainModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Entity.EmployeeFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        File = c.String(nullable: false),
                        EmployeeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Entity.Employee", t => t.EmployeeID, cascadeDelete: true)
                .Index(t => t.EmployeeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Entity.EmployeeFiles", "EmployeeID", "Entity.Employee");
            DropIndex("Entity.EmployeeFiles", new[] { "EmployeeID" });
            DropTable("Entity.EmployeeFiles");
        }
    }
}
