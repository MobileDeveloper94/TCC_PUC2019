namespace TCC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixdatafimtreinamento : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Treinamentoes", "DataFim", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Treinamentoes", "DataFim", c => c.DateTime(nullable: false));
        }
    }
}
