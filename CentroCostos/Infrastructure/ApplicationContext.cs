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
        public ApplicationContext() : base("DbCentroCostos")
        {
        }

        public DbSet<Linea> Lineas { get; set; }
        public DbSet<Modelo> Modelos { get; set; }
        public DbSet<CentroCosto> CentrosDeCostos { get; set; }
        public DbSet<Material> Materiales { get; set; }
        public DbSet<Costo> Costos { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Orden> OrdenesProducidas { get; set; }
        public DbSet<UnidadCosto> CostosGenerados { get; set; }
    }
}