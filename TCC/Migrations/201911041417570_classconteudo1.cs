namespace TCC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class classconteudo1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Conteudoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_Treinamento = c.Int(nullable: false),
                        Descricao = c.String(nullable: false),
                        Cod_Modulo = c.Int(nullable: false),
                        Link = c.String(nullable: false),
                        ConteudoExterno = c.Boolean(nullable: false),
                        Observacao = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Conteudoes");
        }
    }
}
