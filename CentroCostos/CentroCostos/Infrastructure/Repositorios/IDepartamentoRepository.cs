using CentroCostos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroCostos.Infrastructure.Repositorios
{
    public interface IDepartamentoRepository
    {
        Departamento Find(string nombre);
        IEnumerable<Costo> GetCostosDirectos(int departamento);
        IEnumerable<Costo> GetCostosIndirectos(int departamento);
    }
}
