namespace AbpCompanyName.AbpProjectName.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChildTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EntityChilds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MyChildProperty1 = c.Int(nullable: false),
                        MyChildProperty2 = c.Int(nullable: false),
                        EntityMasterTableId = c.Int(nullable: false),
                        CreatedBy = c.Long(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EntityForFirstDB", t => t.EntityMasterTableId, cascadeDelete: true)
                .ForeignKey("dbo.AbpUsers", t => t.CreatedBy)
                .Index(t => t.EntityMasterTableId)
                .Index(t => t.CreatedBy);
            
            AddColumn("dbo.EntityForFirstDB", "CreatedBy", c => c.Long());
            CreateIndex("dbo.EntityForFirstDB", "CreatedBy");
            AddForeignKey("dbo.EntityForFirstDB", "CreatedBy", "dbo.AbpUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EntityChilds", "CreatedBy", "dbo.AbpUsers");
            DropForeignKey("dbo.EntityForFirstDB", "CreatedBy", "dbo.AbpUsers");
            DropForeignKey("dbo.EntityChilds", "EntityMasterTableId", "dbo.EntityForFirstDB");
            DropIndex("dbo.EntityForFirstDB", new[] { "CreatedBy" });
            DropIndex("dbo.EntityChilds", new[] { "CreatedBy" });
            DropIndex("dbo.EntityChilds", new[] { "EntityMasterTableId" });
            DropColumn("dbo.EntityForFirstDB", "CreatedBy");
            DropTable("dbo.EntityChilds");
        }
    }
}
