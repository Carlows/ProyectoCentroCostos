namespace CentroCostos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorreccionMaterialModelo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Material", "Categoria_Material_Id", "dbo.Material");
            CreateTable(
                "dbo.CategoriaMaterials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Categoria = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddForeignKey("dbo.Material", "Categoria_Material_Id", "dbo.CategoriaMaterials", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Material", "Categoria_Material_Id", "dbo.CategoriaMaterials");
            DropTable("dbo.CategoriaMaterials");
            AddForeignKey("dbo.Material", "Categoria_Material_Id", "dbo.Material", "Id");
        }
    }
}
