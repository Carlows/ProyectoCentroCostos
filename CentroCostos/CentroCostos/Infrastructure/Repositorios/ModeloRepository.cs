using CentroCostos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentroCostos.Infrastructure.Repositorios
{
    public class ModeloRepository : BaseRepository<Modelo, int>, IModeloRepository
    {
        public ModeloRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public Modelo Find(string codigo)
        {
            return DbContext.Modelos.Where(m => m.Codigo.StartsWith(codigo)).Single();
        }
    }
}