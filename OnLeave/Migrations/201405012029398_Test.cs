namespace OnLeave.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Test", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Test");
        }
    }
}
