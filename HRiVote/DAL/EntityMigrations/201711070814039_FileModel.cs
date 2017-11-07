namespace HRiVote.DAL.EntityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FileModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Entity.FIleModel",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    ParentID = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
             .ForeignKey("Entity.FileModel", t => t.ParentID, cascadeDelete: false)
             .Index(i => i.ParentID);


        }
        
        public override void Down()
        {
            DropTable("Entity.FIleModel");
        }
    }
}
