namespace CentroCostos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirdMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Material", "Categoria_Material_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Material", "Categoria_Material_Id");
            AddForeignKey("dbo.Material", "Categoria_Material_Id", "dbo.Material", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Material", "Categoria_Material_Id", "dbo.Material");
            DropIndex("dbo.Material", new[] { "Categoria_Material_Id" });
            DropColumn("dbo.Material", "Categoria_Material_Id");
        }
    }
}
