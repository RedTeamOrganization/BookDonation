namespace BookDonation.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "QuantityAvailable", c => c.Int(nullable: false));
            AddColumn("dbo.Books", "QuantityReserved", c => c.String());
            DropColumn("dbo.Books", "QtyAvailable");
            DropColumn("dbo.Books", "QtyReserved");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "QtyReserved", c => c.String());
            AddColumn("dbo.Books", "QtyAvailable", c => c.Int(nullable: false));
            DropColumn("dbo.Books", "QuantityReserved");
            DropColumn("dbo.Books", "QuantityAvailable");
        }
    }
}
