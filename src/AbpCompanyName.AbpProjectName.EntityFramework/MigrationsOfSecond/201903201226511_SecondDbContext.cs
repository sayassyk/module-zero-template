namespace AbpCompanyName.AbpProjectName.MigrationsOfSecond
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondDbContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EntityForSecondDB",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MyProperty1 = c.Int(nullable: false),
                        MyProperty2 = c.Int(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EntityForSecondDB");
        }
    }
}
