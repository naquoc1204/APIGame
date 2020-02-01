namespace PokerGameAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewusercoin : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserCoins",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Coin = c.Double(nullable: false),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserCoins");
        }
    }
}
