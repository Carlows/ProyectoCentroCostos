using CentroCostos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentroCostos.Infrastructure.Repositorios
{
    public class CostoRepository : BaseRepository<Costo, int>, ICostoRepository
    {
        public CostoRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}