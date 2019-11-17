namespace TCC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class avaliacao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Avaliacaos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_Treinamento = c.Int(nullable: false),
                        Cod_Modulo = c.Int(nullable: false),
                        Descricao = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Questaos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_Avaliacao = c.Int(nullable: false),
                        Enunciado = c.String(nullable: false),
                        Alternativa1 = c.String(nullable: false),
                        Alternativa2 = c.String(nullable: false),
                        Alternativa3 = c.String(nullable: false),
                        Alternativa4 = c.String(nullable: false),
                        Alternativa5 = c.String(nullable: false),
                        Cod_AlternativaCorreta = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Questaos");
            DropTable("dbo.Avaliacaos");
        }
    }
}
