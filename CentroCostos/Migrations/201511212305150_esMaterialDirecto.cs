namespace CentroCostos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class esMaterialDirecto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CategoriaMaterials", "NombreCategoria", c => c.String());
            AddColumn("dbo.Material", "esMaterialDirecto", c => c.Boolean(nullable: false));
            DropColumn("dbo.CategoriaMaterials", "Categoria");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CategoriaMaterials", "Categoria", c => c.String());
            DropColumn("dbo.Material", "esMaterialDirecto");
            DropColumn("dbo.CategoriaMaterials", "NombreCategoria");
        }
    }
}
