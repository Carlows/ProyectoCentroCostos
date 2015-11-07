using CentroCostos.Infrastructure.Services;
using CentroCostos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CentroCostos.Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
            : base("DbCentroCostos")
        {
            //Database.SetInitializer<ApplicationContext>(new DbInitializer());
        }

        public DbSet<Linea> Lineas { get; set; }
        public DbSet<Modelo> Modelos { get; set; }
        public DbSet<CentroCosto> CentrosDeCostos { get; set; }
        public DbSet<Material> Materiales { get; set; }
        public DbSet<Costo> Costos { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Orden> OrdenesProducidas { get; set; }
        public DbSet<UnidadCosto> CostosGenerados { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<CategoriaMaterial> Categorias { get; set; }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Material>().Property(m => m.Costo_Unitario).HasPrecision(28, 12);
            modelBuilder.Entity<Material>().Property(m => m.Consumo_Par).HasPrecision(28, 12);

            base.OnModelCreating(modelBuilder);
        }
    }

    public class DbInitializer : DropCreateDatabaseAlways<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {
            UserService service = new UserService(context);

            service.CreateUser("admin", "admin12");

            base.Seed(context);
        }
    }
}