namespace TCC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_Avaliacao = c.Int(nullable: false),
                        Id_Inscricao = c.Int(nullable: false),
                        NumQuestoes = c.Int(nullable: false),
                        NumAcertos = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Notas");
        }
    }
}
