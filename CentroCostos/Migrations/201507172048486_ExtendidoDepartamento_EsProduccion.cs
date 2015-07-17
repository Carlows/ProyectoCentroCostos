namespace CentroCostos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendidoDepartamento_EsProduccion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departamento", "esDeProduccion", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Departamento", "esDeProduccion");
        }
    }
}
