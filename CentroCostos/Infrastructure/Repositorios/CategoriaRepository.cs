using CentroCostos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentroCostos.Infrastructure.Repositorios
{
    public class CategoriaRepository : BaseRepository<CategoriaMaterial, int>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}