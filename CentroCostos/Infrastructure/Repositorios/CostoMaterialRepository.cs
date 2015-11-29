using CentroCostos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroCostos.Infrastructure.Repositorios
{
    public class CostoMaterialRepository : BaseRepository<CostoMaterial, int>, ICostoMaterialRepository
    {
        public CostoMaterialRepository(ApplicationContext context) : base(context)
        {

        }
    }
}
