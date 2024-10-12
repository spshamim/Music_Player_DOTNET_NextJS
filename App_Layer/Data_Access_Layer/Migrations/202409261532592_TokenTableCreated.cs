namespace Data_Access_Layer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TokenTableCreated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tokens",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Key = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        ExpiredAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tokens", "Id", "dbo.Users");
            DropIndex("dbo.Tokens", new[] { "Id" });
            DropTable("dbo.Tokens");
        }
    }
}
