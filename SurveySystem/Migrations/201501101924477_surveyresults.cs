namespace SurveySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class surveyresults : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SurveyResults");
        }
    }
}
