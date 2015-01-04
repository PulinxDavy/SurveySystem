namespace SurveySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class veranderingQuestionGroups : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuestionGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Survey_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Surveys", t => t.Survey_Id)
                .Index(t => t.Survey_Id);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionString = c.String(),
                        AnswerO = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        QuestionGroup_Id = c.Int(),
                        Survey_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuestionGroups", t => t.QuestionGroup_Id)
                .ForeignKey("dbo.Surveys", t => t.Survey_Id)
                .Index(t => t.QuestionGroup_Id)
                .Index(t => t.Survey_Id);
            
            CreateTable(
                "dbo.SurveyResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SurveyId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Surveys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Active = c.Boolean(nullable: false),
                        ActiveSince = c.DateTime(nullable: false),
                        Anonymous = c.Boolean(nullable: false),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "Survey_Id", "dbo.Surveys");
            DropForeignKey("dbo.QuestionGroups", "Survey_Id", "dbo.Surveys");
            DropForeignKey("dbo.Questions", "QuestionGroup_Id", "dbo.QuestionGroups");
            DropIndex("dbo.Questions", new[] { "Survey_Id" });
            DropIndex("dbo.Questions", new[] { "QuestionGroup_Id" });
            DropIndex("dbo.QuestionGroups", new[] { "Survey_Id" });
            DropTable("dbo.Surveys");
            DropTable("dbo.SurveyResults");
            DropTable("dbo.Questions");
            DropTable("dbo.QuestionGroups");
        }
    }
}
