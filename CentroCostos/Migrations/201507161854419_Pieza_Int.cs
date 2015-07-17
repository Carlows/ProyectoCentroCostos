namespace CentroCostos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Pieza_Int : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Modelo", "Pieza", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Modelo", "Pieza", c => c.String());
        }
    }
}
