namespace ATM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CheckingAccounts", "joinDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Transactions", "transactionDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "transactionDate");
            DropColumn("dbo.CheckingAccounts", "joinDate");
        }
    }
}
