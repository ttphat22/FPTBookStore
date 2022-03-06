namespace FPTBOOK_1670_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstrun : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 50, unicode: false),
                        Fullname = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50, unicode: false),
                        Phone = c.String(nullable: false, maxLength: 13, unicode: false),
                        Address = c.String(nullable: false, maxLength: 200, unicode: false),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Username);
            
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        AuthorID = c.String(nullable: false, maxLength: 10, unicode: false),
                        AuthorName = c.String(nullable: false, maxLength: 50, unicode: false),
                        Description = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.AuthorID);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookID = c.String(nullable: false, maxLength: 10, unicode: false),
                        BookName = c.String(nullable: false, maxLength: 100),
                        CategoryID = c.String(nullable: false, maxLength: 10, unicode: false),
                        AuthorID = c.String(nullable: false, maxLength: 10, unicode: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        UrlImage = c.String(),
                        ShortDesc = c.String(nullable: false, maxLength: 200),
                        DetailDesc = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.BookID)
                .ForeignKey("dbo.Category", t => t.CategoryID)
                .ForeignKey("dbo.Authors", t => t.AuthorID)
                .Index(t => t.CategoryID)
                .Index(t => t.AuthorID);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryID = c.String(nullable: false, maxLength: 10, unicode: false),
                        CategoryName = c.String(nullable: false, maxLength: 50, unicode: false),
                        Description = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.CategoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "AuthorID", "dbo.Authors");
            DropForeignKey("dbo.Books", "CategoryID", "dbo.Category");
            DropIndex("dbo.Books", new[] { "AuthorID" });
            DropIndex("dbo.Books", new[] { "CategoryID" });
            DropTable("dbo.Category");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
            DropTable("dbo.Accounts");
        }
    }
}
