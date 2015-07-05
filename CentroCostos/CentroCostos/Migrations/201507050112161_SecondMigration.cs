namespace CentroCostos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Costo", "Departamento_Id", c => c.Int());
            CreateIndex("dbo.Costo", "Departamento_Id");
            AddForeignKey("dbo.Costo", "Departamento_Id", "dbo.Departamento", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Costo", "Departamento_Id", "dbo.Departamento");
            DropIndex("dbo.Costo", new[] { "Departamento_Id" });
            DropColumn("dbo.Costo", "Departamento_Id");
        }
    }
}
