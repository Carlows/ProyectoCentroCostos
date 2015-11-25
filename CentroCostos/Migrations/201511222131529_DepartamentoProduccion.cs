namespace CentroCostos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DepartamentoProduccion : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Departamento", newName: "DepartamentoProduccion");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.DepartamentoProduccion", newName: "Departamento");
        }
    }
}
