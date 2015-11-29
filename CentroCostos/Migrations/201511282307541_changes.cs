namespace CentroCostos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CostoMaterial", "Costo_Total");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CostoMaterial", "Costo_Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
