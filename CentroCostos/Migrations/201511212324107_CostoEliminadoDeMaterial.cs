namespace CentroCostos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CostoEliminadoDeMaterial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Material", "Id", "dbo.Costo");
            DropIndex("dbo.Material", new[] { "Id" });
            DropPrimaryKey("dbo.Material");
            AddColumn("dbo.Costo", "Material_Directo_Id", c => c.Int());
            AlterColumn("dbo.Material", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Material", "Id");
            CreateIndex("dbo.Costo", "Material_Directo_Id");
            AddForeignKey("dbo.Costo", "Material_Directo_Id", "dbo.Material", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Costo", "Material_Directo_Id", "dbo.Material");
            DropIndex("dbo.Costo", new[] { "Material_Directo_Id" });
            DropPrimaryKey("dbo.Material");
            AlterColumn("dbo.Material", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.Costo", "Material_Directo_Id");
            AddPrimaryKey("dbo.Material", "Id");
            CreateIndex("dbo.Material", "Id");
            AddForeignKey("dbo.Material", "Id", "dbo.Costo", "Id");
        }
    }
}
