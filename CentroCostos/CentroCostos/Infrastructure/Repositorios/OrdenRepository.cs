using CentroCostos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentroCostos.Infrastructure.Repositorios
{
    public class OrdenRepository : BaseRepository<Orden, int>, IOrdenRepository 
    {
        public OrdenRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}