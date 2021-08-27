namespace _72HourAssignment.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostReplyMergeMigration2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reply", "CreatedUtc", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reply", "CreatedUtc");
        }
    }
}
