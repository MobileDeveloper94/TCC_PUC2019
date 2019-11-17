namespace TCC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Inscricaos", "NotaTreinamento", c => c.Int());
            AlterColumn("dbo.Inscricaos", "Aprovado", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Inscricaos", "Aprovado", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Inscricaos", "NotaTreinamento", c => c.Int(nullable: false));
        }
    }
}
