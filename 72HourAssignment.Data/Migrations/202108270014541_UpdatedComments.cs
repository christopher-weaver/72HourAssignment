namespace _72HourAssignment.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedComments : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reply", "Comment_Id", "dbo.Comment");
            DropForeignKey("dbo.Reply", "Comment_Id1", "dbo.Comment");
            DropIndex("dbo.Reply", new[] { "Comment_Id" });
            DropIndex("dbo.Reply", new[] { "Comment_Id1" });
            DropColumn("dbo.Reply", "Comment_Id");
            DropColumn("dbo.Reply", "Comment_Id1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reply", "Comment_Id1", c => c.Int());
            AddColumn("dbo.Reply", "Comment_Id", c => c.Int());
            CreateIndex("dbo.Reply", "Comment_Id1");
            CreateIndex("dbo.Reply", "Comment_Id");
            AddForeignKey("dbo.Reply", "Comment_Id1", "dbo.Comment", "Id");
            AddForeignKey("dbo.Reply", "Comment_Id", "dbo.Comment", "Id");
        }
    }
}
