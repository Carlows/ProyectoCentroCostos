using CentroCostos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentroCostos.Infrastructure.Repositorios
{
    public class DepartamentoRepository : BaseRepository<DepartamentoProduccion, int>, IDepartamentoRepository
    {
        public DepartamentoRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public DepartamentoProduccion Find(string nombre)
        {
            return DbContext.Departamentos.Where(d => d.Nombre_Departamento.StartsWith(nombre)).Single();
        }
    }
}