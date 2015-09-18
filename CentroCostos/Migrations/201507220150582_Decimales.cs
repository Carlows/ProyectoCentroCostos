namespace CentroCostos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Decimales : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Material", "Costo_Unitario", c => c.Decimal(nullable: false, precision: 12, scale: 10));
            AlterColumn("dbo.Material", "Consumo_Par", c => c.Decimal(nullable: false, precision: 12, scale: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Material", "Consumo_Par", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Material", "Costo_Unitario", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
