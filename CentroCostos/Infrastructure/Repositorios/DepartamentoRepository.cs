using CentroCostos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentroCostos.Infrastructure.Repositorios
{
    public class DepartamentoRepository : BaseRepository<Departamento, int>, IDepartamentoRepository
    {
        public DepartamentoRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public Departamento Find(string nombre)
        {
            return DbContext.Departamentos.Where(d => d.Nombre_Departamento.StartsWith(nombre)).Single();
        }

        public IEnumerable<Costo> GetCostosDirectos(int departamento)
        {
            return DbContext.Departamentos.Find(departamento)
                .Costos_Departamento.Where(c => c.esCostoDirecto == true)
                .ToList();
        }

        public IEnumerable<Costo> GetCostosIndirectos(int departamento)
        {
            return DbContext.Departamentos.Find(departamento)
                .Costos_Departamento.Where(c => c.esCostoDirecto == false)
                .ToList();
        }
    }
}