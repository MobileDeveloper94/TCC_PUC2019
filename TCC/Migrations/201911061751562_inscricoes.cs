namespace TCC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inscricoes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Inscricaos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_Usuario = c.String(nullable: false),
                        Id_Treinamento = c.Int(nullable: false),
                        DataInscricao = c.DateTime(nullable: false),
                        NotaTreinamento = c.Int(nullable: false),
                        AvaliacaoTreinamento = c.String(),
                        Aprovado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Inscricaos");
        }
    }
}
