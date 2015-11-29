namespace CentroCostos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MaterialesA2Digitos : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Material", "Costo_Unitario", c => c.Decimal(nullable: false, precision: 28, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Material", "Costo_Unitario", c => c.Decimal(nullable: false, precision: 28, scale: 12));
        }
    }
}
