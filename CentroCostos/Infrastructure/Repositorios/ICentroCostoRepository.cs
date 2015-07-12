using CentroCostos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroCostos.Infrastructure.Repositorios
{
    public interface ICentroCostoRepository : IRepository<CentroCosto, int>
    {
        CentroCosto Find(string codigo);
    }
}
