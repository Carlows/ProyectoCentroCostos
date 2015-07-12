namespace CentroCostos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modelo_Fecha_Modificacion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Modelo", "Fecha_Ultima_Modificacion", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Modelo", "Fecha_Ultima_Modificacion");
        }
    }
}
