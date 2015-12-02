using CentroCostos.Models;
using CentroCostos.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroCostos.Infrastructure.Repositorios
{
    public interface IDepartamentoRepository : IRepository<DepartamentoProduccion, int>
    {
        DepartamentoProduccion Find(string nombre);
        DepartamentoProduccion CreateDepartamento(DepartamentoViewModel model);
    }
}
