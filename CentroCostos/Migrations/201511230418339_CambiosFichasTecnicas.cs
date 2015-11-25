namespace CentroCostos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CambiosFichasTecnicas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FichaTecnica",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MaterialesDepartamentoProduccion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Departamento_Id = c.Int(nullable: false),
                        FichaTecnica_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DepartamentoProduccion", t => t.Departamento_Id, cascadeDelete: true)
                .ForeignKey("dbo.FichaTecnica", t => t.FichaTecnica_Id)
                .Index(t => t.Departamento_Id)
                .Index(t => t.FichaTecnica_Id);
            
            CreateTable(
                "dbo.CostoMaterial",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Consumo_Par = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Costo_Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Material_Id = c.Int(),
                        MaterialesDepartamentoProduccion_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Material", t => t.Material_Id)
                .ForeignKey("dbo.MaterialesDepartamentoProduccion", t => t.MaterialesDepartamentoProduccion_Id)
                .Index(t => t.Material_Id)
                .Index(t => t.MaterialesDepartamentoProduccion_Id);
            
            AddColumn("dbo.Modelo", "Ficha_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Modelo", "Ficha_Id");
            AddForeignKey("dbo.Modelo", "Ficha_Id", "dbo.FichaTecnica", "Id", cascadeDelete: true);
            DropColumn("dbo.Material", "Consumo_Par");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Material", "Consumo_Par", c => c.Decimal(nullable: false, precision: 28, scale: 12));
            DropForeignKey("dbo.Modelo", "Ficha_Id", "dbo.FichaTecnica");
            DropForeignKey("dbo.MaterialesDepartamentoProduccion", "FichaTecnica_Id", "dbo.FichaTecnica");
            DropForeignKey("dbo.CostoMaterial", "MaterialesDepartamentoProduccion_Id", "dbo.MaterialesDepartamentoProduccion");
            DropForeignKey("dbo.CostoMaterial", "Material_Id", "dbo.Material");
            DropForeignKey("dbo.MaterialesDepartamentoProduccion", "Departamento_Id", "dbo.DepartamentoProduccion");
            DropIndex("dbo.CostoMaterial", new[] { "MaterialesDepartamentoProduccion_Id" });
            DropIndex("dbo.CostoMaterial", new[] { "Material_Id" });
            DropIndex("dbo.MaterialesDepartamentoProduccion", new[] { "FichaTecnica_Id" });
            DropIndex("dbo.MaterialesDepartamentoProduccion", new[] { "Departamento_Id" });
            DropIndex("dbo.Modelo", new[] { "Ficha_Id" });
            DropColumn("dbo.Modelo", "Ficha_Id");
            DropTable("dbo.CostoMaterial");
            DropTable("dbo.MaterialesDepartamentoProduccion");
            DropTable("dbo.FichaTecnica");
        }
    }
}
