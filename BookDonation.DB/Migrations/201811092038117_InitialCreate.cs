namespace BookDonation.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActionName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        GenreId = c.Int(nullable: false),
                        AuthorId = c.Int(nullable: false),
                        Title = c.String(),
                        ISBN = c.String(),
                        Image = c.Binary(),
                        QtyAvailable = c.Int(nullable: false),
                        QtyReserved = c.String(),
                        Authors_Id = c.Int(),
                        Genres_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Authors", t => t.Authors_Id)
                .ForeignKey("dbo.Genres", t => t.Genres_Id)
                .Index(t => t.Authors_Id)
                .Index(t => t.Genres_Id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActionByUserId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        ActionId = c.String(),
                        Date = c.DateTime(nullable: false),
                        Actions_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Actions", t => t.Actions_Id)
                .Index(t => t.Actions_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "Actions_Id", "dbo.Actions");
            DropForeignKey("dbo.Books", "Genres_Id", "dbo.Genres");
            DropForeignKey("dbo.Books", "Authors_Id", "dbo.Authors");
            DropIndex("dbo.Transactions", new[] { "Actions_Id" });
            DropIndex("dbo.Books", new[] { "Genres_Id" });
            DropIndex("dbo.Books", new[] { "Authors_Id" });
            DropTable("dbo.Transactions");
            DropTable("dbo.Genres");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
            DropTable("dbo.Actions");
        }
    }
}
