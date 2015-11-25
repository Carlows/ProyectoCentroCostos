namespace CentroCostos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoriaMaterials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreCategoria = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Material",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo = c.String(),
                        Descripcion_Material = c.String(),
                        Unidad_Medida = c.String(),
                        esMaterialDirecto = c.Boolean(nullable: false),
                        Costo_Unitario = c.Decimal(nullable: false, precision: 28, scale: 12),
                        Consumo_Par = c.Decimal(nullable: false, precision: 28, scale: 12),
                        Categoria_Material_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CategoriaMaterials", t => t.Categoria_Material_Id, cascadeDelete: true)
                .Index(t => t.Categoria_Material_Id);
            
            CreateTable(
                "dbo.CentroCosto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo = c.String(),
                        Descripcion = c.String(),
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
                        Departamento_Id = c.Int(),
                        Material_Directo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departamento", t => t.Departamento_Id)
                .ForeignKey("dbo.Material", t => t.Material_Directo_Id)
                .Index(t => t.Departamento_Id)
                .Index(t => t.Material_Directo_Id);
            
            CreateTable(
                "dbo.Departamento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre_Departamento = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        Fecha_Ultima_Modificacion = c.DateTime(nullable: false),
                        Tipo_Suela = c.String(),
                        Numeracion_Menor = c.Byte(nullable: false),
                        Numeracion_Mayor = c.Byte(nullable: false),
                        URL_Imagen = c.String(),
                        Pieza = c.Int(nullable: false),
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
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orden", "Modelo_Id", "dbo.Modelo");
            DropForeignKey("dbo.Modelo", "Linea_Id", "dbo.Linea");
            DropForeignKey("dbo.UnidadCosto", "Costo_Id", "dbo.Costo");
            DropForeignKey("dbo.Costo", "Material_Directo_Id", "dbo.Material");
            DropForeignKey("dbo.Costo", "Departamento_Id", "dbo.Departamento");
            DropForeignKey("dbo.Material", "Categoria_Material_Id", "dbo.CategoriaMaterials");
            DropIndex("dbo.Orden", new[] { "Modelo_Id" });
            DropIndex("dbo.Modelo", new[] { "Linea_Id" });
            DropIndex("dbo.UnidadCosto", new[] { "Costo_Id" });
            DropIndex("dbo.Costo", new[] { "Material_Directo_Id" });
            DropIndex("dbo.Costo", new[] { "Departamento_Id" });
            DropIndex("dbo.Material", new[] { "Categoria_Material_Id" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.Orden");
            DropTable("dbo.Modelo");
            DropTable("dbo.Linea");
            DropTable("dbo.UnidadCosto");
            DropTable("dbo.Departamento");
            DropTable("dbo.Costo");
            DropTable("dbo.CentroCosto");
            DropTable("dbo.Material");
            DropTable("dbo.CategoriaMaterials");
        }
    }
}
