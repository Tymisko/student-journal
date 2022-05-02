namespace Diary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReCreate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ratings", "Rating_Id", "dbo.Ratings");
            DropIndex("dbo.Ratings", new[] { "Rating_Id" });
            DropColumn("dbo.Ratings", "Rating_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ratings", "Rating_Id", c => c.Int());
            CreateIndex("dbo.Ratings", "Rating_Id");
            AddForeignKey("dbo.Ratings", "Rating_Id", "dbo.Ratings", "Id");
        }
    }
}
