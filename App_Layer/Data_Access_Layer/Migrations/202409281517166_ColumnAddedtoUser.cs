namespace Data_Access_Layer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnAddedtoUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ResetToken", c => c.String(maxLength: 8000, unicode: false, nullable: true));
            AddColumn("dbo.Users", "resetTokenExpires", c => c.DateTime(precision: 7, storeType: "datetime2", nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "resetTokenExpires");
            DropColumn("dbo.Users", "ResetToken");
        }
    }
}
