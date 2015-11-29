using CentroCostos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroCostos.Infrastructure.Repositorios
{
    public class MaterialesDepartamentoRepository : BaseRepository<MaterialesDepartamentoProduccion, int>, IMaterialesDepartamentoRepository
    {
        public MaterialesDepartamentoRepository(ApplicationContext dbContext) : base(dbContext) { }
    }
}
