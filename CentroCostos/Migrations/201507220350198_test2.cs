namespace CentroCostos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Material", "Costo_Unitario", c => c.Decimal(nullable: false, precision: 28, scale: 12));
            AlterColumn("dbo.Material", "Consumo_Par", c => c.Decimal(nullable: false, precision: 28, scale: 12));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Material", "Consumo_Par", c => c.Decimal(nullable: false, precision: 12, scale: 12));
            AlterColumn("dbo.Material", "Costo_Unitario", c => c.Decimal(nullable: false, precision: 12, scale: 12));
        }
    }
}
