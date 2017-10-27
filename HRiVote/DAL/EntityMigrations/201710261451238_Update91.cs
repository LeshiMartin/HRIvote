namespace HRiVote.DAL.EntityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update91 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Entity.Skills",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Skill = c.String(),
                        EmployeeID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Entity.Employee", t => t.EmployeeID)
                .Index(t => t.EmployeeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Entity.Skills", "EmployeeID", "Entity.Employee");
            DropIndex("Entity.Skills", new[] { "EmployeeID" });
            DropTable("Entity.Skills");
        }
    }
}
