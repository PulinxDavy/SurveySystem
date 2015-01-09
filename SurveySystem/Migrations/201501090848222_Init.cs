namespace SurveySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Answer = c.String(),
                        QuestionString = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.QuestionGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
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
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Cellphone = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.QuestionGroupQuestions",
                c => new
                    {
                        QuestionGroup_Id = c.Int(nullable: false),
                        Question_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.QuestionGroup_Id, t.Question_Id })
                .ForeignKey("dbo.QuestionGroups", t => t.QuestionGroup_Id, cascadeDelete: true)
                .ForeignKey("dbo.Questions", t => t.Question_Id, cascadeDelete: true)
                .Index(t => t.QuestionGroup_Id)
                .Index(t => t.Question_Id);
            
            CreateTable(
                "dbo.SurveyQuestionGroups",
                c => new
                    {
                        Survey_Id = c.Int(nullable: false),
                        QuestionGroup_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Survey_Id, t.QuestionGroup_Id })
                .ForeignKey("dbo.Surveys", t => t.Survey_Id, cascadeDelete: true)
                .ForeignKey("dbo.QuestionGroups", t => t.QuestionGroup_Id, cascadeDelete: true)
                .Index(t => t.Survey_Id)
                .Index(t => t.QuestionGroup_Id);
            
            CreateTable(
                "dbo.SurveyQuestions",
                c => new
                    {
                        Survey_Id = c.Int(nullable: false),
                        Question_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Survey_Id, t.Question_Id })
                .ForeignKey("dbo.Surveys", t => t.Survey_Id, cascadeDelete: true)
                .ForeignKey("dbo.Questions", t => t.Question_Id, cascadeDelete: true)
                .Index(t => t.Survey_Id)
                .Index(t => t.Question_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.SurveyQuestions", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.SurveyQuestions", "Survey_Id", "dbo.Surveys");
            DropForeignKey("dbo.SurveyQuestionGroups", "QuestionGroup_Id", "dbo.QuestionGroups");
            DropForeignKey("dbo.SurveyQuestionGroups", "Survey_Id", "dbo.Surveys");
            DropForeignKey("dbo.QuestionGroupQuestions", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.QuestionGroupQuestions", "QuestionGroup_Id", "dbo.QuestionGroups");
            DropIndex("dbo.SurveyQuestions", new[] { "Question_Id" });
            DropIndex("dbo.SurveyQuestions", new[] { "Survey_Id" });
            DropIndex("dbo.SurveyQuestionGroups", new[] { "QuestionGroup_Id" });
            DropIndex("dbo.SurveyQuestionGroups", new[] { "Survey_Id" });
            DropIndex("dbo.QuestionGroupQuestions", new[] { "Question_Id" });
            DropIndex("dbo.QuestionGroupQuestions", new[] { "QuestionGroup_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropTable("dbo.SurveyQuestions");
            DropTable("dbo.SurveyQuestionGroups");
            DropTable("dbo.QuestionGroupQuestions");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Surveys");
            DropTable("dbo.QuestionGroups");
            DropTable("dbo.Questions");
        }
    }
}
