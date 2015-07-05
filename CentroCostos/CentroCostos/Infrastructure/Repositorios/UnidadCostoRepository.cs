using CentroCostos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentroCostos.Infrastructure.Repositorios
{
    public class UnidadCostoRepository : BaseRepository<UnidadCosto, int>, IUnidadCostoRepository
    {
        public UnidadCostoRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}