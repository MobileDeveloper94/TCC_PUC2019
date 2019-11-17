namespace TCC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Visualizacoes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Visualizacaos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_Inscricao = c.Int(nullable: false),
                        Id_Conteudo = c.Int(nullable: false),
                        Visualizado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Visualizacaos");
        }
    }
}
