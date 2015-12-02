namespace CentroCostos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MatDepartamentoProduccionRequiresFicha : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MaterialesDepartamentoProduccion", "FichaTecnica_Id", "dbo.FichaTecnica");
            DropIndex("dbo.MaterialesDepartamentoProduccion", new[] { "FichaTecnica_Id" });
            RenameColumn(table: "dbo.MaterialesDepartamentoProduccion", name: "FichaTecnica_Id", newName: "Ficha_Id");
            AlterColumn("dbo.MaterialesDepartamentoProduccion", "Ficha_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.MaterialesDepartamentoProduccion", "Ficha_Id");
            AddForeignKey("dbo.MaterialesDepartamentoProduccion", "Ficha_Id", "dbo.FichaTecnica", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MaterialesDepartamentoProduccion", "Ficha_Id", "dbo.FichaTecnica");
            DropIndex("dbo.MaterialesDepartamentoProduccion", new[] { "Ficha_Id" });
            AlterColumn("dbo.MaterialesDepartamentoProduccion", "Ficha_Id", c => c.Int());
            RenameColumn(table: "dbo.MaterialesDepartamentoProduccion", name: "Ficha_Id", newName: "FichaTecnica_Id");
            CreateIndex("dbo.MaterialesDepartamentoProduccion", "FichaTecnica_Id");
            AddForeignKey("dbo.MaterialesDepartamentoProduccion", "FichaTecnica_Id", "dbo.FichaTecnica", "Id");
        }
    }
}
