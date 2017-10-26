namespace HRiVote.DAL.EntityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Entity.Skills",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Skill = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "Entity.SkillsEmployee",
                c => new
                    {
                        Skills_ID = c.Int(nullable: false),
                        Employee_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Skills_ID, t.Employee_ID })
                .ForeignKey("Entity.Skills", t => t.Skills_ID, cascadeDelete: true)
                .ForeignKey("Entity.Employee", t => t.Employee_ID, cascadeDelete: true)
                .Index(t => t.Skills_ID)
                .Index(t => t.Employee_ID);
            
            DropColumn("Entity.Employee", "Skills");
        }
        
        public override void Down()
        {
            AddColumn("Entity.Employee", "Skills", c => c.String());
            DropForeignKey("Entity.SkillsEmployee", "Employee_ID", "Entity.Employee");
            DropForeignKey("Entity.SkillsEmployee", "Skills_ID", "Entity.Skills");
            DropIndex("Entity.SkillsEmployee", new[] { "Employee_ID" });
            DropIndex("Entity.SkillsEmployee", new[] { "Skills_ID" });
            DropTable("Entity.SkillsEmployee");
            DropTable("Entity.Skills");
        }
    }
}
