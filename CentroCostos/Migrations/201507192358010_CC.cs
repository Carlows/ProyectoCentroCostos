namespace CentroCostos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CC : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.CentroCosto", name: "Departamento_CentroCosto_Id", newName: "Departamento_Id");
            RenameIndex(table: "dbo.CentroCosto", name: "IX_Departamento_CentroCosto_Id", newName: "IX_Departamento_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.CentroCosto", name: "IX_Departamento_Id", newName: "IX_Departamento_CentroCosto_Id");
            RenameColumn(table: "dbo.CentroCosto", name: "Departamento_Id", newName: "Departamento_CentroCosto_Id");
        }
    }
}
