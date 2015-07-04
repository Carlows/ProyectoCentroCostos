namespace CentroCostos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CentroCosto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo = c.String(),
                        Descripcion = c.String(),
                        Departamento_CentroCosto_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departamento", t => t.Departamento_CentroCosto_Id, cascadeDelete: true)
                .Index(t => t.Departamento_CentroCosto_Id);
            
            CreateTable(
                "dbo.Departamento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre_Departamento = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Costo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        esCostoDirecto = c.Boolean(nullable: false),
                        Descripcion = c.String(),
                        Comentario = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Material",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Codigo = c.String(),
                        Descripcion_Material = c.String(),
                        Unidad_Medida = c.String(),
                        Costo_Unitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Consumo_Par = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Costo", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.UnidadCosto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        Valor_Costo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Costo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Costo", t => t.Costo_Id)
                .Index(t => t.Costo_Id);
            
            CreateTable(
                "dbo.Linea",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre_Linea = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Modelo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo = c.String(),
                        Horma = c.String(),
                        Planta = c.String(),
                        Fecha_Creacion = c.DateTime(nullable: false),
                        Tipo_Suela = c.String(),
                        Numeracion_Menor = c.Byte(nullable: false),
                        Numeracion_Mayor = c.Byte(nullable: false),
                        URL_Imagen = c.String(),
                        Pieza = c.String(),
                        Color = c.String(),
                        Linea_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Linea", t => t.Linea_Id, cascadeDelete: true)
                .Index(t => t.Linea_Id);
            
            CreateTable(
                "dbo.Orden",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cantidad_Total = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        Modelo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Modelo", t => t.Modelo_Id)
                .Index(t => t.Modelo_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orden", "Modelo_Id", "dbo.Modelo");
            DropForeignKey("dbo.Modelo", "Linea_Id", "dbo.Linea");
            DropForeignKey("dbo.UnidadCosto", "Costo_Id", "dbo.Costo");
            DropForeignKey("dbo.Material", "Id", "dbo.Costo");
            DropForeignKey("dbo.CentroCosto", "Departamento_CentroCosto_Id", "dbo.Departamento");
            DropIndex("dbo.Orden", new[] { "Modelo_Id" });
            DropIndex("dbo.Modelo", new[] { "Linea_Id" });
            DropIndex("dbo.UnidadCosto", new[] { "Costo_Id" });
            DropIndex("dbo.Material", new[] { "Id" });
            DropIndex("dbo.CentroCosto", new[] { "Departamento_CentroCosto_Id" });
            DropTable("dbo.Orden");
            DropTable("dbo.Modelo");
            DropTable("dbo.Linea");
            DropTable("dbo.UnidadCosto");
            DropTable("dbo.Material");
            DropTable("dbo.Costo");
            DropTable("dbo.Departamento");
            DropTable("dbo.CentroCosto");
        }
    }
}
