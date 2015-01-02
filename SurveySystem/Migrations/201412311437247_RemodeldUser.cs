namespace SurveySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemodeldUser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Telephone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Telephone", c => c.String());
        }
    }
}
