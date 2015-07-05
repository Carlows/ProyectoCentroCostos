using CentroCostos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentroCostos.Infrastructure.Repositorios
{
    public class MaterialRepository : BaseRepository<Material, int>, IMaterialRepository
    {
        public MaterialRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}