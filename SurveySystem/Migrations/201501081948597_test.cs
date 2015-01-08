namespace SurveySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Questions", "QuestionGroup_Id", "dbo.QuestionGroups");
            DropIndex("dbo.Questions", new[] { "QuestionGroup_Id" });
            RenameColumn(table: "dbo.Questions", name: "QuestionGroup_Id", newName: "QuestionGroupRefId");
            AlterColumn("dbo.Questions", "QuestionGroupRefId", c => c.Int(nullable: false));
            CreateIndex("dbo.Questions", "QuestionGroupRefId");
            AddForeignKey("dbo.Questions", "QuestionGroupRefId", "dbo.QuestionGroups", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "QuestionGroupRefId", "dbo.QuestionGroups");
            DropIndex("dbo.Questions", new[] { "QuestionGroupRefId" });
            AlterColumn("dbo.Questions", "QuestionGroupRefId", c => c.Int());
            RenameColumn(table: "dbo.Questions", name: "QuestionGroupRefId", newName: "QuestionGroup_Id");
            CreateIndex("dbo.Questions", "QuestionGroup_Id");
            AddForeignKey("dbo.Questions", "QuestionGroup_Id", "dbo.QuestionGroups", "Id");
        }
    }
}
