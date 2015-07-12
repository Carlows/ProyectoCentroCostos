using CentroCostos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentroCostos.Infrastructure.Repositorios
{
    public class CentroCostoRepository : BaseRepository<CentroCosto, int>, ICentroCostoRepository
    {
        public CentroCostoRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public CentroCosto Find(string codigo)
        {
            return DbContext.CentrosDeCostos.Where(c => c.Codigo.StartsWith(codigo)).Single();
        }
    }
}