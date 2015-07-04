namespace OnLeave.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveTest : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "Test");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Test", c => c.Int());
        }
    }
}
